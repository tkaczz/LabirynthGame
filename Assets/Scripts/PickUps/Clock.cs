using UnityEngine;

public class Clock : PickUp {
    /// <summary>
    /// Po nim sprawdzamy czy dodajemy, odejmujemy czas
    /// </summary>
    [SerializeField] private bool addTime;

    [SerializeField] private int time = 5;

    public override void Pick() {
        int sign;

        if (addTime) {
            sign = 1;
        }
        else {
            sign = -1;
        }
        GameManager.gameManager.AddTime(time * sign);
        base.Pick();
    }
}