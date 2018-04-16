using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class NinjaHook : MonoBehaviour {

	public static NinjaHook inst;

	public GameObject ninjaHookTrailPrefab;
    GameObject currentTrail;

    public float minCutVelocity = .008f;
    public float gentleCutThreshold = .005f;

	[System.NonSerialized] public float speed = 0.0f;
	[System.NonSerialized] public Vector2 velocity;
    Vector2 previousPosition;

    public bool isCutting = false; 

    CircleCollider2D circleCollider2D;

    Rigidbody2D rb;

    Camera cam;

	void Awake() {
		Assert.IsNull(inst);
		inst = this;
	}

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        circleCollider2D = GetComponent<CircleCollider2D>();
	}
	
	void Update () {
        if (GameManager.inst.state != (GameManager.GameState.Ninja)) {
            return;
        }
            
        if (!isCutting && Input.GetMouseButtonDown(0)) {
            startCutting();
        } else if (Input.GetMouseButtonUp(0)) {
            stopCutting();
        }

        if (isCutting) {
            updateCut();
        }
		
	}

    void startCutting()
    {
        isCutting = true;
        transform.position = getPosition();
        currentTrail = Instantiate(ninjaHookTrailPrefab, transform);
        currentTrail.transform.localPosition = new Vector3();
        previousPosition = rb.position;
        speed = 0.0f;
        circleCollider2D.enabled = true;
    }

    void stopCutting() {
        isCutting = false;
        currentTrail.transform.SetParent(null);
        Destroy(currentTrail, 2f);
        speed = 0.0f;
        circleCollider2D.enabled = false;
    }

    void updateCut() {
        rb.position = getPosition();
		velocity = (rb.position - previousPosition) / Time.deltaTime;
		speed = (rb.position - previousPosition).magnitude * Time.deltaTime;

        //if (velocity > minCutVelocity) {
        //    circleCollider2D.enabled = true;
        //} else {
        //    circleCollider2D.enabled = false;
        //}
        previousPosition = rb.position;
        //Debug.Log("pos = " + rb.position);
    }
    Vector2 getPosition() {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
