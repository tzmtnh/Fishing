using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour {

	public static UIManager inst;

	public GameObject startMenu;
	public GameObject endGameMenu;

	GameObject[] _menues;

	GameObject getMenuByState(GameManager.GameState state) {
		switch (state) {
			case GameManager.GameState.StartMenu:	return startMenu;
			case GameManager.GameState.Fishing:		return null;
			case GameManager.GameState.Ninja:		return null;
			case GameManager.GameState.EndGame:		return endGameMenu;
			case GameManager.GameState.Leaderboard: return null;
			default:								return null;
		}
	}

	void onGameStateChanged(GameManager.GameState oldState, GameManager.GameState newState) {
		GameObject newMenu = getMenuByState(newState);
		foreach (GameObject menu in _menues) {
			menu.SetActive(newMenu == menu);
		}
	}

	public void onStartButtonPressed() {
		GameManager.inst.changeState(GameManager.GameState.Fishing);
	}

	void Awake () {
		Assert.IsNull(inst);
		inst = this;

		GameManager.onGameStateChanged += onGameStateChanged;

		_menues = new GameObject[2];
		_menues[0] = startMenu;
		_menues[1] = endGameMenu;
	}
}
