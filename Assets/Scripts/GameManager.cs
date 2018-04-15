using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using TMPro;

public class GameManager : MonoBehaviour {

	public enum GameState { StartMenu, Fishing, Ninja, EndGame, Leaderboard }

	public static GameManager inst;
	public static event Action<GameState, GameState> onGameStateChanged;

	public GameState state;
	public int score;

	public TextMeshPro positiveScore;
	public TextMeshPro negativeScore;

	public void changeState(GameState newState, bool forceUpdate = false) {
		if (newState == state && forceUpdate == false) return;
		GameState oldState = state;
		state = newState;

		if (state == GameState.Fishing) {
			score = 0;
		}

		if (onGameStateChanged != null) {
			onGameStateChanged(oldState, newState);
		}
	}

	public void addScore(int scoreToAdd, Vector3 pos) {
		score += scoreToAdd;
		StartCoroutine(animateScore(scoreToAdd, pos));
	}

	IEnumerator animateScore(int scoreToAdd, Vector3 pos) {
		TextMeshPro scoreText = Instantiate(scoreToAdd > 0 ? positiveScore : negativeScore);
		scoreText.transform.position = pos;
		scoreText.text = (scoreToAdd > 0 ? "+" : "") + scoreToAdd + "$";

		const float duration = 1;
		const float speed = 1.5f;

		float timer = 0;
		while(timer < duration) {
			float t = timer / duration;
			float dt = Time.deltaTime;

			scoreText.alpha = 1f - t;
			scoreText.transform.position += Vector3.up * (dt * speed);

			timer += dt;
			yield return null;
		}
	}

	void Awake() {
		Assert.IsNull(inst);
		inst = this;
		changeState(GameState.StartMenu, true);
	}
}
