using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

    public GameObject trashPrefab;
    public Transform[] spawnPoints;
    public float minDelay = .1f;
    public float maxDelay = 1f;


	// Use this for initialization
	void Start () {
        StartCoroutine(spawnTrash());
	}

    IEnumerator spawnTrash() {
        while (true) {
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];
            GameObject spawnedTrash = Instantiate(trashPrefab, spawnPoint.position, spawnPoint.rotation);
            Destroy(spawnedTrash, 5f);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
