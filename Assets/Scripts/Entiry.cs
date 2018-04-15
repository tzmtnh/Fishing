using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entiry : MonoBehaviour {

	public int price = 10;
	public AnimationCurve probabilityByDepth = new AnimationCurve();

	protected Rigidbody2D _rigidbody;
	protected SpriteRenderer _sprite;
	protected Collider2D _collider;

	protected bool _attached = false;
	float _initMass;

	public void attachTo(Rigidbody2D rb, Collider2D c) {
		if (_attached) return;
		_attached = true;
		_rigidbody.mass = 0;

		HingeJoint2D joint = gameObject.AddComponent<HingeJoint2D>();
		joint.connectedBody = rb;
		_rigidbody.velocity = new Vector2();
		_rigidbody.angularVelocity = 0;

		Physics2D.IgnoreCollision(_collider, c, true);
	}

	void Awake() {
		_collider = GetComponent<Collider2D>();
		_sprite = GetComponent<SpriteRenderer>();
		_rigidbody = GetComponent<Rigidbody2D>();
		_initMass = _rigidbody.mass;

		LayerMask layer = LayerMask.NameToLayer("Entities");
		Physics2D.IgnoreLayerCollision(layer, layer, true);
	}

}
