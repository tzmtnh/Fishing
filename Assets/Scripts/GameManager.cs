using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

	public enum GameState { StartMenu, InGame }

	public static GameManager inst;

	private void Awake() {
		Assert.IsNull(inst);
		inst = this;
	}
}
