using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Entities : MonoBehaviour {

	public static Entities inst;

	public int numEntities = 100;
	public Vector2 depthRange = new Vector2(-1, -80);

	public Entity[] entityPrefabs;

	void generateEntities() {
		for (int i = 0; i < numEntities; i++) {
			float depthParam = i / (numEntities - 1f);
			float y = Mathf.Lerp(depthRange.x, depthRange.y, depthParam);
			float x = Random.Range(-3f, 3f);

			float totalWeight = 0;
			foreach (Entity ep in entityPrefabs) {
				totalWeight += ep.probabilityByDepth.Evaluate(depthParam);
			}

			float r = Random.value;
			Entity prefab = null;
			float currentWeight = 0f;
			foreach (Entity ep in entityPrefabs) {
				float w = ep.probabilityByDepth.Evaluate(depthParam);
				currentWeight += w;
				if (currentWeight / totalWeight >= r) {
					prefab = ep;
					break;
				}
			}

			if (prefab == null) continue;
			Entity entity = Instantiate(prefab, transform);
			entity.transform.position = new Vector3(x, y, 0);
		}
	}

	void Awake () {
		Assert.IsNull(inst);
		inst = this;
		
		generateEntities();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine(new Vector3(-6, depthRange.x, 0), new Vector3(6, depthRange.x, 0));
		Gizmos.DrawLine(new Vector3(-6, depthRange.y, 0), new Vector3(6, depthRange.y, 0));
	}
}
