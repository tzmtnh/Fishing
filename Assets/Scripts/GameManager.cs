using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	public enum GameState { StartMenu, Fishing, Ninja, EndGame, Leaderboard }

	public static GameManager inst;

	public GameState state;

	private void Awake() {
		Assert.IsNull(inst);
		inst = this;
	}
}
