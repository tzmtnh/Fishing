using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fish : Entity {

    static string TRAPPED_TURTLE_SPRITE = "Trapped_Turtle";

    public Sprite FREE_VERSION;

    bool isTurtle = false;
	public float speed = 1;

	float _dir = 1;

    SpriteRenderer _spriteRenderer;
	void Start() {
		_dir = Random.value > 0.5f ? 1 : -1;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer.sprite.texture.name == TRAPPED_TURTLE_SPRITE) {
            isTurtle = true;
        }
	}

	void FixedUpdate() {
        if (_state == State.InWater) {
			_rigidbody.AddForce(new Vector2(_dir * speed, 0));
		}
	}

	protected override void Update() {
		base.Update();
		if (_state != State.InWater) return;
		_sprite.flipX = _dir > 0;
	}

    protected override void onCollisionEnter2D(Collision2D collision) {
		if (_state == State.InWater &&
            collision.collider.CompareTag("Side")) {
			_dir = -_dir;
        } else if (_state == State.Flying && 
                   collision.collider.CompareTag("NinjaHook")) {
            NinjaHook ninjaHook = collision.collider.transform.GetComponent<NinjaHook>();
            Debug.Log("Ninja fish collision!");
            if (isTurtle && ninjaHook.currentVelocity < ninjaHook.gentleCutThreshold)
            {
                Debug.Log("Hook velocity that released me (Turtle)" + ninjaHook.currentVelocity);
                int score = 2 * price;
                GameManager.inst.addScore(score, transform.position);
                //TrashSpawner.inst.trashObjList.Remove(this);
                _spriteRenderer.sprite = FREE_VERSION;
                isTurtle = false; 
            }
        } 
	}

}
