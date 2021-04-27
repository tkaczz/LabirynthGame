using UnityEngine;

public class Lock : MonoBehaviour {
	public Doors[] doors;
	public Keys myColor;
	private bool iCanOpen;
	private bool locked = false;
	private Animator keyAnimator;

	public void UseKey() {
		foreach (var door in doors) {
			door.OpenClose();
		}
	}

	public bool CheckTheKey() {
		if (GameManager.gameManager.redKeys > 0 && myColor == Keys.Red) {
			GameManager.gameManager.redKeys--;
			locked = true;
		}
		else if (GameManager.gameManager.greenKeys > 0 && myColor == Keys.Green) {
			GameManager.gameManager.greenKeys--;
			locked = true;
		}
		else if (GameManager.gameManager.goldKeys > 0 && myColor == Keys.Gold) {
			GameManager.gameManager.goldKeys--;
			locked = true;
		}
		else {
			Debug.Log("Nie masz klucza!");
			locked = false;
		}

		return locked;
	}

	private void Start() {
		keyAnimator = GetComponent<Animator>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			iCanOpen = true;
			Debug.Log("You can use lock");
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			iCanOpen = false;
			Debug.Log("You can not use lock");
		}
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.E) && iCanOpen && !locked) {
			keyAnimator.SetBool("useKey", CheckTheKey());
		}
	}
}