using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

    public Entity trashPrefab;
    public Transform[] spawnPoints;
    public int maxSpawns = 20;
    public float startForce = 10f;
    public List<Entity> trashObjList;

    public static TrashSpawner trashSpawnerInstance = null;

    Rigidbody2D rb;

	private void Awake()
	{
        trashSpawnerInstance = this;
        GameManager.onGameStateChanged += _start;
	}

	// Use this for initialization
	void Start () {
        trashObjList = new List<Entity>();
	}

    void _start(GameManager.GameState old, GameManager.GameState current) {
        if (current.Equals(GameManager.GameState.Ninja))

        for (int i = 0; i < maxSpawns; i++) {
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];
            Quaternion tmp = spawnPoint.rotation;
            tmp.z += Random.Range(-0.025f, 0.025f);
            spawnPoint.rotation = tmp;
            Entity spawnedTrash = Instantiate(trashPrefab, spawnPoint.position, spawnPoint.rotation);
                //rb = spawnedTrash.GetComponent<Rigidbody2D>();
                //rb.AddForce(transform.up * startForce, ForceMode2D.Impulse);
                //rb.AddTorque(Random.Range(-200f, 200f));
                //spawnedTrash._attached = false;
                //spawnedTrash._flying = true;
            spawnedTrash.detachAndLaunch();
            trashObjList.Add(spawnedTrash);
            //Destroy(spawnedTrash, 5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
