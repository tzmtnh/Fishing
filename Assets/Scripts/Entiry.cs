using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour {

	public int price = 10;
    public float startForce = 1600f;
	public AnimationCurve probabilityByDepth = new AnimationCurve();

	bool _isGarbage;
	public bool isGarbage { get { return _isGarbage; } }

	bool _isFish;
	public bool isFish { get { return _isFish; } }

	protected Rigidbody2D _rigidbody;
	protected SpriteRenderer _sprite;
	protected Collider2D _collider;
    protected WheelJoint2D _joint;
	protected bool _attached = false;
    protected bool _flying = false;
	float _initMass;

	public virtual void attachTo(Rigidbody2D rb, Collider2D c) {
		if (_attached) return;
		_attached = true;
		_rigidbody.mass = 0;
		_rigidbody.angularVelocity = 0;

		_joint = gameObject.AddComponent<WheelJoint2D>();
		_joint.connectedBody = rb;
		_joint.anchor = new Vector2(0, 0.1f);

		_rigidbody.velocity = new Vector2();
		_rigidbody.gravityScale = 1;

		Physics2D.IgnoreCollision(_collider, c, true);
	}

    public void detachAndLaunch() {
        _rigidbody.mass = _initMass;
        _joint.enabled = false;
        _rigidbody.drag = 0.25f;
        _rigidbody.gravityScale = 0.25f;
        Invoke("delayedLayerCollisions", UnityEngine.Random.Range(1.0f, 2.0f));
        _rigidbody.AddForce(transform.up *
                            UnityEngine.Random.Range(0.7f, 1.3f) * startForce * _initMass,
                            ForceMode2D.Impulse);
        _rigidbody.AddTorque(UnityEngine.Random.Range(-200f, 200f));

        _attached = false;
        _flying = true;

    }

    void delayedLayerCollisions() {
        LayerMask layer = LayerMask.NameToLayer("Entities");
        Physics2D.IgnoreLayerCollision(layer, layer, false);  
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

	protected virtual void Update()
	{
		if (_flying && _rigidbody.position.y < 0.5) {
			TrashSpawner.inst.trashObjList.Remove(this);
			Destroy(gameObject);
        }
	}

	protected virtual void onCollisionEnter2D(Collision2D collision) { }

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("NinjaHook")) {
			int score = price * (isGarbage ? 1 : -1);
			GameManager.inst.addScore(score, transform.position);
			TrashSpawner.inst.trashObjList.Remove(this);
			Destroy(gameObject);
			return;
		}

		onCollisionEnter2D(collision);
	}
}
