using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour {

	public int price = 10;
	public AnimationCurve probabilityByDepth = new AnimationCurve();

	bool _isGarbage;
	public bool isGarbage { get { return _isGarbage; } }

	bool _isFish;
	public bool isFish { get { return _isFish; } }

	protected Rigidbody2D _rigidbody;
	protected SpriteRenderer _sprite;
	protected Collider2D _collider;

	protected bool _attached = false;
	float _initMass;

	public void attachTo(Rigidbody2D rb, Collider2D c) {
		if (_attached) return;
		_attached = true;
		_rigidbody.mass = 0;

		WheelJoint2D joint = gameObject.AddComponent<WheelJoint2D>();
		joint.connectedBody = rb;
		joint.anchor = new Vector2(0, 0.1f);

		_rigidbody.velocity = new Vector2();
		_rigidbody.gravityScale = 1;

		Physics2D.IgnoreCollision(_collider, c, true);
	}

	void Awake() {
		_collider = GetComponent<Collider2D>();
		_sprite = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_initMass = _rigidbody.mass;

		Type type = GetType();
		_isGarbage = type == typeof(Garbage);
		_isFish = type == typeof(Fish);

		LayerMask layer = LayerMask.NameToLayer("Entities");
		Physics2D.IgnoreLayerCollision(layer, layer, true);
	}

}
