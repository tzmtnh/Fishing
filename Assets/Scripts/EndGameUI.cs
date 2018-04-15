using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {

	public Text scoreText;

	void OnEnable() {
		if (scoreText != null) {
			scoreText.text = GameManager.inst.score + "$";
		}
	}
}
