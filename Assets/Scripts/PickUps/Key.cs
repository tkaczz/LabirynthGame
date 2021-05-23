using UnityEngine;

public enum Keys {
	Green,
	Red,
	Gold
}

public class Key : PickUp {
	[SerializeField] private Material red;
	[SerializeField] private Material green;
	[SerializeField] private Material gold;

	[SerializeField] private Keys keyType = Keys.Gold;

	public override void Pick() {
		GameManager.gameManager.AddKey(keyType);
		base.Pick();
	}

	private void SetMyColor() {
		switch (keyType) {
			case Keys.Green:
				GetComponent<Renderer>().material = green;
				break;
			case Keys.Red:
				GetComponent<Renderer>().material = red;
				break;
			case Keys.Gold:
				GetComponent<Renderer>().material = gold;
				break;
		}
	}

	private void Start() {
		SetMyColor();
	}
}