using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float hookBoarderY = -1;
    public float smoothSpeed = 0.1f;
    public Vector3 offset;
    public Vector3 target;

	Vector3 _pos;
    Vector3 _initial_pos;

	void updateFishing() {
		float deltaY = Hook.inst.transform.position.y - _pos.y;
		if (Mathf.Abs(deltaY) > hookBoarderY) {
			_pos.y += deltaY - hookBoarderY;
            //transform.position = _pos;
            target = _pos;
		}
	}

    void updateNinja() {
        // find lowest object
        float min_y = 1000;
        bool all_null = true;
        TrashSpawner trashSpawner = TrashSpawner.inst;
        foreach (Entity go in trashSpawner.trashObjList)
        {
            if (go == null)
            {
                continue;
            }
            all_null = false;
            float curr_y = go.GetComponent<Rigidbody2D>().position.y;
            if (curr_y < min_y)
            {
                min_y = curr_y;
            }
        }
        if (min_y < 1000f && min_y > 4f)
        {
            Vector3 pos = transform.position;
            _pos.y = min_y + 1.5f;
            //transform.position = _pos;
            target = _pos;
        } else if (all_null) {
            target = _initial_pos;
        }
    }

	void Start () {
		_pos = transform.position;
        target = _pos;
        _initial_pos = _pos;
	}

	Vector3 _velocity;
	void Update () {
		switch (GameManager.inst.state) {
			case GameManager.GameState.StartMenu:
				break;
			case GameManager.GameState.Fishing:
				updateFishing();
				break;
			case GameManager.GameState.Ninja:
                updateNinja();
				break;
			case GameManager.GameState.EndGame:
				break;
			case GameManager.GameState.Leaderboard:
				break;
			default:
				Debug.LogError("Unhandled Game State");
				break;
		}

		Vector3 desiredPosition = target + offset;
		Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothSpeed);
		transform.position = smoothedPosition;
	}
}
