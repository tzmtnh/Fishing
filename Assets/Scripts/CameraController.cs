using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float hookBoarderY = -1;

	Vector3 _pos;

	void updateFishing() {
		float deltaY = Hook.inst.transform.position.y - _pos.y;
		if (Mathf.Abs(deltaY) > hookBoarderY) {
			_pos.y += deltaY - hookBoarderY;
			transform.position = _pos;
		}
	}

	void Start () {
		_pos = transform.position;
	}
	
	void Update () {
		switch (GameManager.inst.state) {
			case GameManager.GameState.StartMenu:
				break;
			case GameManager.GameState.Fishing:
				updateFishing();
				break;
			case GameManager.GameState.Ninja:
				break;
			case GameManager.GameState.EndGame:
				break;
			case GameManager.GameState.Leaderboard:
				break;
			default:
				Debug.LogError("Unhandled Game State");
				break;
		}
	}
}
