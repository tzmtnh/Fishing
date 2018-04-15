using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {

	public float speed = 1;
	public float boarder = 5;

	Vector3 _pos;
	Transform _transform;

	void Awake() {
		_transform = transform;
		_pos = _transform.position;
	}

	void Update () {
		_pos.x += speed * Time.deltaTime;
		if (_pos.x > boarder) {
			_pos.x = -boarder;
		} else if (_pos.x < -boarder) {
			_pos.x = boarder;
		}
		_transform.position = _pos;
	}
}
