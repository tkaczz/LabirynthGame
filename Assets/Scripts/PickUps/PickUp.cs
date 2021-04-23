using UnityEngine;

/// <summary>
/// Ten skrypt będziemy umieszczać na kazdym obiekcie który będziemy mogli podnieść
///<para>Umożliwi to też nadpisywanie/dołożenie funkcjonalności dla funkcji Pick</para>
/// </summary>
public class PickUp : MonoBehaviour {

    public virtual void Pick() {
        Destroy(this.gameObject);
    }

    private void Update() {
        Rotate();
    }

    private void Rotate() {
        transform.Rotate(0, 0, 1);
    }
}