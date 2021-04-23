using UnityEngine;

public class FreezePickUp : PickUp {
    [SerializeField] private int freezeAmount = 5;

    public override void Pick() {
        GameManager.gameManager.FreezeTime(freezeAmount);
        base.Pick();
    }
}