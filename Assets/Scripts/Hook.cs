using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Hook : MonoBehaviour {

	enum State { Idle, GoingDown, GoingUp }

	public static Hook inst;

	public float reactionSpeed = 20f;

    public float recenteringSpeedModifier = 6f;

	public LineRenderer rope;

	State _state = State.Idle;
	Collider2D _collider;

	Rigidbody2D _rigidbody;
	public Rigidbody2D rigid {
		get { return _rigidbody; }
	}

	[System.NonSerialized]
	public List<Entity> attachedEntities = new List<Entity>(32);

	void updateLeftRight() {
		float horizontal = InputManager.inst.horizontal;
		if (Mathf.Approximately(horizontal, 0)) return;
		_rigidbody.AddForce(new Vector2(horizontal * reactionSpeed, 0));
	}

	void Awake() {
		Assert.IsNull(inst);
		inst = this;

		_rigidbody = GetComponent<Rigidbody2D>();
		_collider = GetComponent<Collider2D>();

		rope.SetPosition(0, _rigidbody.transform.position);
	}

	void FixedUpdate() {
		switch (_state) {
			case State.Idle:
				_rigidbody.gravityScale = 0;
				if (InputManager.inst.click) {
					_state = State.GoingDown;
				}
				break;

			case State.GoingDown:
				_rigidbody.gravityScale = 1;
				updateLeftRight();
				break;

			case State.GoingUp:
				_rigidbody.gravityScale = -1;
				updateLeftRight();
				break;

			default:
				Debug.LogError("Unhandled Rob State");
				break;
		}
        // pendulum motion of hook towards center.
        Vector2 pos = _rigidbody.position;
        pos.x = 0.43f;
        _rigidbody.AddForce((pos - _rigidbody.position) * recenteringSpeedModifier /
                            (Mathf.Abs(pos.y) + 0.15f));
		_rigidbody.angularVelocity = 0;
		_rigidbody.rotation = 0;
	}

	void Update() {
		rope.SetPosition(0, _rigidbody.transform.position);
	}

	void OnCollisionEnter2D(Collision2D collision) {
        if (GameManager.inst.state != (GameManager.GameState.Fishing))
        {
            return;
        }
		if (collision.collider.CompareTag("Entiry")) {
			Entity entiry = collision.collider.GetComponent<Entity>();
			Assert.IsNotNull(entiry);
			entiry.attachTo(_rigidbody, _collider);
			attachedEntities.Add(entiry);

			if (_state == State.GoingDown) {
				_state = State.GoingUp;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (_state == State.GoingDown && collision.CompareTag("Ground")) {
			_state = State.GoingUp;
		} else if (_state == State.GoingUp && collision.CompareTag("SeaLevel")) {
			_state = State.Idle;
			GameManager.inst.changeState(GameManager.GameState.Ninja);
		}
	}
}
