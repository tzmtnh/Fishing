using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : Entity {

	public float speed = 1;

	float _dir = 1;

	void Start() {
		_dir = Random.value > 0.5f ? 1 : -1;
	}

	void FixedUpdate() {
        if (_attached == false && _flying == false) {
			_rigidbody.AddForce(new Vector2(_dir * speed, 0));
		}
	}

	protected override void Update() {
		base.Update();
		if (_attached) return;
		_sprite.flipX = _dir > 0;
	}

    protected override void onCollisionEnter2D(Collision2D collision) {
		if (GameManager.inst.state != GameManager.GameState.Fishing) return;
        if (collision.collider.CompareTag("Side")) {
			_dir = -_dir;
        }
	}

}
