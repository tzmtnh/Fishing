using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class GameManager : MonoBehaviour
{

	public struct ScoreEntry {
		public string name;
		public int count;
		public int price;
		public int order;
		public Sprite sprite;
	}

	public enum GameState { StartMenu, Fishing, Ninja, EndGame, Leaderboard }

    public static GameManager inst;
    public event Action<GameState, GameState> onGameStateChanged;

    public GameState state;
    public int score;

    public TextMeshPro positiveScore;
    public TextMeshPro negativeScore;


	[NonSerialized]
	public Dictionary<string, ScoreEntry> scoreEntries = new Dictionary<string, ScoreEntry>(32);

    public void changeState(GameState newState, bool forceUpdate = false)
    {
        if (newState == state && forceUpdate == false) return;
        GameState oldState = state;
        state = newState;

		//Debug.LogFormat("Game state changed from {0} to {1}", oldState, newState);

		if (onGameStateChanged != null) {
			onGameStateChanged(oldState, newState);
		}

        switch (state)
        {
            case GameState.Fishing:
                score = 0;
                AudioManager.inst.playFishingMusic();
                break;
            case GameState.Ninja:
                AudioManager.inst.playNinjaMusic();
                break;
            case GameState.EndGame:
                AudioManager.inst.playThemeMusic();
                break;
            default:
                break;
        }

    }
	public void addScore(Entity entity, int scoreToAdd) {
		score += scoreToAdd;

		ScoreEntry scoreEntry;
		if (scoreEntries.ContainsKey(entity.name)) {
			scoreEntry = scoreEntries[entity.name];
		} else {
			scoreEntry = new ScoreEntry();
			scoreEntry.name = entity.name;
			scoreEntry.price = scoreToAdd;
			scoreEntry.sprite = entity.sprite;
			scoreEntry.order = entity.isGarbage ? entity.price : entity.price * 1000;
			scoreEntries.Add(entity.name, scoreEntry);
		}

		scoreEntry.count++;
		scoreEntries[entity.name] = scoreEntry;

		StartCoroutine(animateScore(scoreToAdd, entity.transform.position));
	}


    public void addScore(int scoreToAdd, Vector3 pos)
    {
        score += scoreToAdd;
        StartCoroutine(animateScore(scoreToAdd, pos));
    }

    IEnumerator animateScore(int scoreToAdd, Vector3 pos)
    {
        TextMeshPro scoreText = Instantiate(scoreToAdd > 0 ? positiveScore : negativeScore);
        scoreText.transform.position = pos;
        scoreText.text = "$" + (scoreToAdd > 0 ? "+" : "") + scoreToAdd;

        const float duration = 1;
        const float speed = 1.5f;

        float timer = 0;
        while (timer < duration)
        {
            float t = timer / duration;
            float dt = Time.deltaTime;

            scoreText.alpha = 1f - t;
            scoreText.transform.position += Vector3.up * (dt * speed);

            timer += dt;
            yield return null;
        }
    }

    void Awake()
    {
        Assert.IsNull(inst);
        inst = this;
        changeState(GameState.StartMenu, true);
    }

	private void Start()
	{
        AudioManager.inst.playThemeMusic();
	}
}
