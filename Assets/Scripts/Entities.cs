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
		List<Vector2> usedRadiusesAndHeights = new List<Vector2>(128);

		foreach (Entity prefab in entityPrefabs) {
			int n = Mathf.CeilToInt(prefab.population * densityMultiplier);
			CapsuleCollider2D collider = prefab.GetComponent<CapsuleCollider2D>();
			float r = collider.size.y * prefab.transform.localScale.y / 2f;

			for (int i = 0; i < n; i++) {
				for (int j = 0; j < 100; j++) {
					float rand = getRandomValueByDistribution(prefab.probabilityByDepth);
					float y = Mathf.Lerp(depthRange.x, depthRange.y, rand);

					bool valid = true;
					foreach (Vector2 rh in usedRadiusesAndHeights) {
						float usedR = rh.x;
						float usedY = rh.y;
						if (Mathf.Abs(y - usedY) < r + usedR) {
							valid = false;
							break;
						}
					}
					if (valid == false) continue;
					usedRadiusesAndHeights.Add(new Vector2(r, y));

					float w = prefab.isGarbage ? 4f : 1f;
					float x = Random.Range(-1f, 1f) * w;

					Entity entity = Instantiate(prefab, transform);
					entity.name = prefab.name;
					entity.transform.position = new Vector3(x, y, 0);
					break;
				}
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
