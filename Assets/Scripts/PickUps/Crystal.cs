using UnityEngine;

public class Crystal : PickUp {
    [SerializeField] private int points = 5;

    public override void Pick() {
        GameManager.gameManager.AddPoints(points);
        base.Pick(); //kasujemy obiekt
    }
}