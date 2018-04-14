using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float minHookY = -1;

	Vector3 _pos;

	void Start () {
		_pos = transform.position;
	}
	
	void Update () {
		float deltaY = Rob.inst.hook.transform.position.y - _pos.y;
		if (deltaY < minHookY) {
			_pos.y += deltaY - minHookY;
			transform.position = _pos;
		}
	}
}
