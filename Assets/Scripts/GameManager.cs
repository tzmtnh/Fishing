using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	public enum GameState { StartMenu, Fishing, Ninja, EndGame, Leaderboard }

	public static GameManager inst;
	public static event Action<GameState, GameState> onGameStateChanged;

	public GameState state;

	public void changeState(GameState newState) {
		if (newState == state) return;
		GameState oldState = state;
		state = newState;
		onGameStateChanged(oldState, newState);
		Debug.LogFormat("changing state from {0} to {1}", oldState, newState);
	}

	void Awake() {
		Assert.IsNull(inst);
		inst = this;
	}
}
