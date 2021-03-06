using UnityEngine;

public class PortalTeleport : MonoBehaviour {
    /// <summary>
    /// Przetrzymuje pozycję gracza
    /// </summary>
    [Tooltip("Przetrzymuje pozycję gracza")]
    [SerializeField] private Transform player;

    /// <summary>
    /// Pozycja i obrót naszego celu
    /// </summary>
    [Tooltip("Pozycja i obrót naszego celu")]
    [SerializeField] private Transform receiver;

    private bool playerIsOverlaping;

    private void FixedUpdate() {
        if (playerIsOverlaping) {
            //różnica między pozycją gracza a transform
            //w scenariuszach kiedy gracz wchodzi np. bardziej po lewej/prawej stronie
            Vector3 portalToPlayer = player.position - transform.position;

            //sprawdzamy czy gracz jest wystarczająco blisko
            //jeśli będą w przeciwnych kierunkach to będzie ujemne
            //sprawdzamy czy gracz podchodzi od przodu
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            if (dotProduct < 0f) {
                //wyliczenie różnicy obrotu, tak by postać po wyjściu była ustawiona pod odpowiedniym kątem
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                //różnicy między pozycją gracza (w momencie wejścia)
                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionOffset;

                playerIsOverlaping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerIsOverlaping = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerIsOverlaping = false;
        }
    }
}