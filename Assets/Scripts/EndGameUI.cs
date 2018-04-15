using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour {

	public Text scoreText;
	public Image entityImage;
	public Text entityName;
	public Text entityCount;
	public Text entityPrice;

	IEnumerator playAnimation() {
		var entries = GameManager.inst.scoreEntries;
		List<GameManager.ScoreEntry> scoreEntries = new List<GameManager.ScoreEntry>(entries.Count);
		foreach (var item in entries) {
			scoreEntries.Add(item.Value);
		}
		scoreEntries.Sort((a, b) => (a.order - b.order));

		int lastScore = 0;
		WaitForSeconds shortWait = new WaitForSeconds(0.06f);
		WaitForSeconds longWait = new WaitForSeconds(0.5f);

		foreach (GameManager.ScoreEntry entry in scoreEntries) {
			entityName.text = entry.name;
			entityPrice.text = entry.price + "$";
			entityCount.text = entry.count.ToString();
			entityImage.sprite = entry.sprite;

			Vector3 scale = entityImage.rectTransform.localScale;
			float w = entry.sprite.rect.width;
			float h = entry.sprite.rect.height;
			if (w > h) {
				scale.y = h / w;
			} else {
				scale.x = w / h;
			}
			entityImage.rectTransform.localScale = scale;

			int i = 0;
			int step = 1;
			int n = Mathf.Abs(entry.price) * entry.count;
			int sign = entry.price < 0 ? -1 : 1;
			while (i < n) {
				i += step;
				step++;
				int newScore = lastScore + Mathf.Min(i, n) * sign;
				scoreText.text = newScore + "$";
				yield return shortWait;
			}

			lastScore += n * sign;
			yield return longWait;
		}
	}

	void OnEnable() {
		StartCoroutine(playAnimation());
	}

	private void OnDisable() {
		StopAllCoroutines();
	}
}
