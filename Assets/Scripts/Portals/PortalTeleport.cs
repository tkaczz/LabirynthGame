using UnityEngine;

public class PortalTeleport : MonoBehaviour {
    /// <summary>
    /// Przetrzymuje pozycję gracza
    /// </summary>
    [Tooltip("Przetrzymuje pozycję gracza")]
    public Transform player;

    /// <summary>
    /// Pozycja i obrót naszego celu
    /// </summary>
    [Tooltip("Pozycja i obrót naszego celu")]
    public Transform receiver;

    /// <summary>
    /// Flaga oznaczająca czy gracz jest w interakcji z protalem
    /// </summary>
    private bool playerIsOverlaping;

    //dlaczego FixedUpdate?
    //wpływamy na ruch CharacterController czyli fizykę naszej gry
    //żeby działała prawidłowo to powinno się to robić właśnie w FixedUpdate
    private void FixedUpdate() {
        //prosta sprawa, jeśli gracz nie jest w interakcji to nie idziemy dalej
        if (playerIsOverlaping) {
            //różnica między pozycją gracza a transform portalu do którego wchodzimy
            //w scenariuszach kiedy gracz wchodzi np. bardziej po lewej/prawej stronie
            Vector3 portalToPlayer = player.position - transform.position;

            //sprawdzamy czy gracz podchodzi od przodu
            //jest to tzw. iloczyn skalarny
            //w grach służy m.in. do tego
            //że możemy sprawdzić czy coś jest w zasięgu wzroku postaci (lub z tyłu)
            //lub w naszym przypadku czy wchodzimy z dobrego kierunku do portalu
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //jeśli wchodzimy od przodu
            if (dotProduct < 0f) {
                //wyliczenie różnicy obrotu, tak by postać po wyjściu była ustawiona pod odpowiednim kątem
                //liczymy to też dlatego że portal z którego wychodzimy też może być obrócony
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180; //bez tego postać się tak jakby zatrzaśnie
                player.Rotate(Vector3.up, rotationDiff); //ostatecznie obracamy postać

                //różnicy między pozycją gracza (w momencie wejścia)
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;

                //ustawiamy pozycję gracza na miejsce w którym jest portal z którego wychodzimy
                //+ przesunięcie naszej względem tego do którego wchodziliśmy (positionOffset)
                player.position = receiver.position + positionOffset;

                playerIsOverlaping = false;
            }
            //else {
            //    Debug.Log("Wchodzimy z innej strony");
            //}
        }
    }

    //Jako że nasz box collider jest typu trigger
    //to musimy obłużyć interakcję z nim w funkcjach
    //OnTriggerEnter, OnTriggerExit
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerIsOverlaping = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            playerIsOverlaping = false;
        }
    }
}