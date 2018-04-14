using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Rob : MonoBehaviour {

	public static Rob inst;

	public float maxSpeed = 1;
	public float reactionSpeed = 1;

	public Rigidbody2D hook;
	public LineRenderer rope;

	private void Awake() {
		Assert.IsNull(inst);
		inst = this;

		rope.positionCount = 2;
		rope.SetPosition(0, hook.transform.position);
		rope.SetPosition(1, hook.transform.position);
	}

	private void FixedUpdate() {
		if (Input.GetKey(KeyCode.LeftArrow)) {
			hook.AddForce(new Vector2(-reactionSpeed, 0));
		} else if (Input.GetKey(KeyCode.RightArrow)) {
			hook.AddForce(new Vector2(reactionSpeed, 0));
		}

		if (hook.velocity.magnitude > maxSpeed) {
			hook.velocity = hook.velocity.normalized * maxSpeed;
		}
	}

	private void Update() {
		rope.SetPosition(1, hook.transform.position);
	}
}
