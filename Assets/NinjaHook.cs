using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaHook : MonoBehaviour {

    public GameObject ninjaHookTrailPrefab;
    GameObject currentTrail;

    public float minCutVelocity = 0.001f;
    Vector2 previousPosition;

    public bool isCutting = false; 

    CircleCollider2D circleCollider2D;

    Rigidbody2D rb;

    Camera cam;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main;

        circleCollider2D = GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.inst.state != (GameManager.GameState.Ninja)) {
            return;
        }
            
        if (Input.GetMouseButtonDown(0)) {
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
    }

    void stopCutting() {
        isCutting = false;
        currentTrail.transform.SetParent(null);
        Destroy(currentTrail, 2f);
        circleCollider2D.enabled = false;
    }

    void updateCut() {
        rb.position = getPosition();
        float velocity = (rb.position - previousPosition).magnitude * Time.deltaTime;

        if (velocity > minCutVelocity) {
            circleCollider2D.enabled = true;
        } else {
            circleCollider2D.enabled = false;
        }
        previousPosition = rb.position;
        //Debug.Log("pos = " + rb.position);
    }
    Vector2 getPosition() {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
