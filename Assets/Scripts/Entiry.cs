using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Entity : MonoBehaviour {

	protected enum State { InWater, Attached, Flying }

	public int population = 10;
	public int price = 10;
    public float startForce = 1600f;
	public AnimationCurve probabilityByDepth = new AnimationCurve();

	public bool isGarbage { get { return GetType() == typeof(Garbage); } }
	public bool isFish { get { return GetType() == typeof(Fish); } }

	public Sprite sprite { get { return _sprite.sprite; } }

	protected Rigidbody2D _rigidbody;
	protected SpriteRenderer _sprite;
	protected Collider2D _collider;
    protected WheelJoint2D _joint;
	protected State _state = State.InWater;
	float _initMass;

	public virtual void attachTo(Rigidbody2D rb, Collider2D c) {
        if (_state != State.InWater) return;
        _state = State.Attached;
        // sfx
        if (isGarbage) {
            AudioManager.inst.playSound("Hooking_Garbage");
        } else {
            AudioManager.inst.playSound("Hooking_Fish");   
        }
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

		_state = State.Flying;
        AudioManager.inst.playSound("Leaving_Water");
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

		LayerMask layer = LayerMask.NameToLayer("Entities");
		Physics2D.IgnoreLayerCollision(layer, layer, true);
	}

	protected virtual void Update()
	{

        if (_state == State.Flying && _rigidbody.position.y < 0.6) {
            if (isGarbage) {
                AudioManager.inst.playSound("Garbage_Enter_Water");
            } else {
                AudioManager.inst.playSound("Fish_Enter_Water");
            }
			TrashSpawner.inst.trashObjList.Remove(this);
			Destroy(gameObject);
        }
	}

	void slice() {
		foreach (Transform child in transform) {
			Destroy(child.gameObject);
		}

		Vector2 velocity = NinjaHook.inst.velocity;
		float angle = Vector2.Angle(velocity, Vector2.up);
		angle = angle - _rigidbody.rotation;

		_sprite.material.SetFloat("_Sliced", 1);
		_sprite.material.SetFloat("_Angle", angle);

		GameObject go = Instantiate(gameObject);
		go.name = gameObject.name + " Slice";
		Destroy(go.GetComponent<Entity>());

		SpriteRenderer other = go.GetComponent<SpriteRenderer>();
		other.material.SetFloat("_Angle", angle + 180);

		Collider2D otherCollider = go.GetComponent<Collider2D>();
		Physics2D.IgnoreCollision(_collider, otherCollider, true);

		StartCoroutine(sliceCo(other));
	}

	IEnumerator sliceCo(SpriteRenderer part) {
		const float duration = 1;
		float timer = 0;
		Color c = Color.white;

		while (timer < duration) {
			float t = timer / duration;

			c.a = 1f - t;
			part.color = c;
			_sprite.color = c;

			timer += Time.deltaTime;
			yield return null;
		}

		Destroy(part.gameObject);
		Destroy(gameObject);
	}

	protected virtual void onCollisionEnter2D(Collision2D collision) { }

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.CompareTag("NinjaHook")) {
            if (NinjaHook.inst.speed >= NinjaHook.inst.minCutVelocity)
            {
                //Debug.Log("Hook velocity that killed me " + ninjaHook.currentVelocity);
                if (isGarbage) {
                    AudioManager.inst.playSound("Garbage_Slice");
                } else {
                    AudioManager.inst.playSound("Fish_Slice");
                }
                int score = price * (isGarbage ? 1 : -1);
                GameManager.inst.addScore(this, score);
                TrashSpawner.inst.trashObjList.Remove(this);

				slice();
                return;
            }
		}

		onCollisionEnter2D(collision);
	}
}
