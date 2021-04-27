using UnityEngine;

public class Doors : MonoBehaviour {
	[SerializeField] private Transform closePosition;
	[SerializeField] private Transform openPosition;
	[SerializeField] private Transform door;

	private bool open = false;
	private float speed = 1f;

	public void OpenClose() {
		open = !open;
	}

	private void Start() {
		door.position = closePosition.position;
	}

	private void Update() {
		if (open && Vector3.Distance(door.position, openPosition.position) > 0.001f) {
			door.position = Vector3.MoveTowards(
				door.position,
				openPosition.position,
				Time.deltaTime * speed
			);
		}
		if (!open && Vector3.Distance(door.position, closePosition.position) > 0.001f) {
			door.position = Vector3.MoveTowards(
				door.position,
				closePosition.position,
				Time.deltaTime * speed
			);
		}
	}
}