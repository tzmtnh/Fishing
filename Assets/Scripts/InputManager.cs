using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class InputManager : MonoBehaviour {

	public static InputManager inst;

	public bool left;
	public bool right;
	public bool click;

	Quaternion _initAttitude = Quaternion.identity;

	void Awake () {
		Assert.IsNull(inst);
		inst = this;
	}

	IEnumerator Start() {
		if (Application.platform == RuntimePlatform.Android) {
			Input.gyro.enabled = true;

			yield return null;
			_initAttitude = Input.gyro.attitude;
		}
	}

	float _angle;
	void Update () {
		left = false;
		right = false;
		click = false;

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0) {
				Touch touch = Input.touches[0];
				if (touch.phase == TouchPhase.Began) {
					click = true;
				}
			}

			Quaternion attitude = Input.gyro.attitude * _initAttitude;
			Vector3 up1 = _initAttitude * Vector3.forward;
			Vector3 forward = _initAttitude * Vector3.up;
			Vector3 up2 = Vector3.ProjectOnPlane(attitude * Vector3.forward, forward).normalized;
			_angle = Vector3.Angle(up1, up2);

			//_angle = attitude.eulerAngles.y - 180f;
			//_angle = (180f - Mathf.Abs(_angle)) * Mathf.Sign(_angle);
			const float thresh = 10;
			if (Mathf.Abs(_angle) > thresh) {
				left = _angle < 0;
				right = _angle > 0;
			}

		} else {
			if (Input.GetKey(KeyCode.LeftArrow)) {
				left = true;
			}

			if (Input.GetKey(KeyCode.LeftArrow)) {
				right = true;
			}

			if (Input.GetKeyDown(KeyCode.Space)) {
				click = true;
			}
		}
	}

	void OnGUI() {
		GUI.Label(new Rect(100, 100, 200, 100), "" + _angle);
	}
}
