using UnityEngine;

public class WinPlace : MonoBehaviour {
	private float alfa = 0;

	private float Resizer() {
		float value = Mathf.Sin(alfa);
		alfa += (1.5f * Time.deltaTime);
		return value + 2f;
	}

	private void Update() {
		float scale = Resizer();
		transform.localScale = new Vector3(scale, transform.localScale.y, scale);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			GameManager.gameManager.WinGame();
		}
	}
}