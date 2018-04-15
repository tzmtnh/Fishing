using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

    public GameObject trashPrefab;
    public Transform[] spawnPoints;
    public float minDelay = .01f;
    public float maxDelay = .1f;
    public int maxSpawns = 20;
    public List<GameObject> trashObjList;

    public static TrashSpawner trashSpawnerInstance = null;

	private void Awake()
	{
        trashSpawnerInstance = this;
        GameManager.onGameStateChanged += _start;
	}

	// Use this for initialization
	void Start () {
        trashObjList = new List<GameObject>();
	}

    void _start(GameManager.GameState old, GameManager.GameState current) {
        if (current.Equals(GameManager.GameState.Ninja))
        StartCoroutine(spawnTrash()); 
    }
    IEnumerator spawnTrash() {
        for (int i = 0; i < maxSpawns; i++) {
            float delay = 0.05f;//Random.Range(minDelay, maxDelay);
            Debug.Log(delay);
            yield return new WaitForSeconds(delay);
            //yield return true;    
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];
            Quaternion tmp = spawnPoint.rotation;
            tmp.z += Random.Range(-0.025f, 0.025f);
            spawnPoint.rotation = tmp;
            GameObject spawnedTrash = Instantiate(trashPrefab, spawnPoint.position, spawnPoint.rotation);
            trashObjList.Add(spawnedTrash);
            //Destroy(spawnedTrash, 5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
