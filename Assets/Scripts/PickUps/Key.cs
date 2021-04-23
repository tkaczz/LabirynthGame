using UnityEngine;

public enum Keys {
    Green,
    Red,
    Gold
}

public class Key : PickUp {
    [SerializeField] private Keys keyType = Keys.Gold;

    public override void Pick() {
        GameManager.gameManager.AddKey(keyType);
        base.Pick();
    }
}