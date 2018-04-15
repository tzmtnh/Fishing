using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour {

	public static InputManager inst;

	public float horizontal;
	public bool click;

	Quaternion _initAttitudeInv = Quaternion.identity;

	void Awake () {
		Assert.IsNull(inst);
		inst = this;
	}

	IEnumerator Start() {
		if (Application.platform == RuntimePlatform.Android) {
			Input.gyro.enabled = true;
			Screen.sleepTimeout = SleepTimeout.NeverSleep;

			yield return null;
			_initAttitudeInv = Quaternion.Inverse(Input.gyro.attitude);
		}
	}

	float _angle;
	Vector3 _angles;
	void Update () {
		horizontal = 0;
		click = false;

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0) {
				Touch touch = Input.touches[0];
				if (touch.phase == TouchPhase.Began) {
					click = true;
				}
			}

			Quaternion attitude = _initAttitudeInv * Input.gyro.attitude;
			_angles = attitude.eulerAngles;
			_angle = attitude.eulerAngles.z - 180f;
			_angle = (180f - Mathf.Abs(_angle)) * Mathf.Sign(_angle);
			const float thresh = 10;
			if (Mathf.Abs(_angle) > thresh) {
				horizontal = _angle / 20f;
			}

		} else {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				horizontal = -1;
			}

			if (Input.GetKey(KeyCode.RightArrow)) {
				horizontal = 1;
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				click = true;
			}
		}
	}

	void OnGUI() {
		GUIStyle style = new GUIStyle();
		style.fontSize = 40;
		GUI.Label(new Rect(100, 100, 200, 100), "" + _angles, style);
		GUI.Label(new Rect(100, 200, 200, 100), "" + _angle, style);
	}
}
