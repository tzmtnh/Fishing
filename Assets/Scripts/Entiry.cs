using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entiry : MonoBehaviour {

	public int price = 10;
	public AnimationCurve probabilityByDepth = new AnimationCurve();

	protected Rigidbody2D _rigidbody;
	protected SpriteRenderer _sprite;

	bool _attached = false;

	public void attachTo(Rigidbody2D rb) {
		if (_attached) return;
		_attached = true;

		HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
		joint.connectedBody = rb;
	}

	void Awake() {
		_sprite = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
	}

}
