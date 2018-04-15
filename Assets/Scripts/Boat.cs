using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour {
	
	public float frequency = 1;
	public float amplitude = 3f;

	Transform _transform;
	float _phase = 0;

	void Awake() {
		_transform = transform;
	}

	void Update () {
		_phase += Time.deltaTime * frequency;
		float angle = Mathf.Sin(_phase) * amplitude;
		_transform.rotation = Quaternion.Euler(0, 0, angle);
	}
}
