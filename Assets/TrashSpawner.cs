using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

    public Entity trashPrefab;
    public int maxSpawns = 15;
    public float startForce = 10f;
    public List<Entity> trashObjList;

    public static TrashSpawner inst = null;

    Rigidbody2D rb;

	private void Awake()
	{
        inst = this;
        GameManager.onGameStateChanged += _start;
	}

	void Start () {
        trashObjList = new List<Entity>();
	}

    void _start(GameManager.GameState old, GameManager.GameState current) {
        if (!current.Equals(GameManager.GameState.Ninja)) {
            return;
        }

        foreach (Entity attachedEntity in Hook.inst.attachedEntities) {
            Transform spawnPoint = Hook.inst.rigid.transform;
            Quaternion tmp = spawnPoint.rotation;
            tmp.z += Random.Range(-0.025f, 0.025f);
            spawnPoint.rotation = tmp;
            attachedEntity.detachAndLaunch();
            trashObjList.Add(attachedEntity);
            //Destroy(spawnedTrash, 5f);
        }
    }

	void showEndGameMenu() {
		GameManager.inst.changeState(GameManager.GameState.EndGame);
	}

	bool _gameEnded = false;
	void Update () {
		if (_gameEnded) return;
		if (GameManager.inst.state == GameManager.GameState.Ninja && trashObjList.Count == 0) {
			_gameEnded = true;
			Invoke("showEndGameMenu", 1);
		}
	}
}
