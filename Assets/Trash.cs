using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {
    public float startForce = 10f;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
        rb.AddTorque(Random.Range(-200f, 200f));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.tag == "NinjaHook") {
            Debug.Log("Print we hit trash!");
            Destroy(gameObject);
        }
	}

	// Update is called once per frame
	void Update () {
        if (rb.position.y < 2) {
            Destroy(gameObject);
        }
	}
}
