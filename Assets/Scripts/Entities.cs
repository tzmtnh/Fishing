using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Entities : MonoBehaviour {

	public static Entities inst;

	public int numEntities = 100;
	public float densityMultiplier = 1;
	public Vector2 depthRange = new Vector2(-1, -80);

	public Entity[] entityPrefabs;

	float getRandomValueByDistribution(AnimationCurve distributionCurve) {
		const int subdivitions = 100;
		float total = 0;
		for (int i = 0; i < subdivitions; i++) {
			float t = i / (subdivitions - 1f);
			total += distributionCurve.Evaluate(t);
		}

		float r = Random.value;
		float current = 0;
		for (int i = 0; i < subdivitions; i++) {
			float t = i / (subdivitions - 1f);
			current += distributionCurve.Evaluate(t);
			if (current / total >= r) {
				return t;
			}
		}
		return 1;
	}

	void generateEntities() {
		foreach (Entity prefab in entityPrefabs) {
			int n = Mathf.CeilToInt(prefab.population * densityMultiplier);
			for (int i = 0; i < n; i++) {
				float r = getRandomValueByDistribution(prefab.probabilityByDepth);
				float y = Mathf.Lerp(depthRange.x, depthRange.y, r);
				float w = prefab.isGarbage ? 4f : 1f;
				float x = Random.Range(-1f, 1f) * w;

				Entity entity = Instantiate(prefab, transform);
				entity.name = prefab.name;
				entity.transform.position = new Vector3(x, y, 0);
			}
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
