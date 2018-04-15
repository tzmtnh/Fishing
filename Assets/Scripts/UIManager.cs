using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour {

	public static UIManager inst;

	public GameObject startMenu;

	GameObject[] _menues;

	GameObject getMenuByState(GameManager.GameState state) {
		switch (state) {
			case GameManager.GameState.StartMenu: return startMenu;
			case GameManager.GameState.Fishing:
			case GameManager.GameState.Ninja:
			case GameManager.GameState.EndGame:
			case GameManager.GameState.Leaderboard:
			default: return null;
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

		_menues = new GameObject[1];
		_menues[0] = startMenu;
	}
}
