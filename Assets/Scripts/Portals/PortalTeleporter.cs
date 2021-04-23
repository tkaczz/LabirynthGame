using UnityEngine;

public class PortalTeleporter : MonoBehaviour {
    public Transform player;
    public Transform receiver;
    private bool playerIsOverlapping = false;

    private void FixedUpdate() {
        if (playerIsOverlapping) {
            Vector3 portalToPlayer = player.position - transform.position;

            //dlaczego transform.up
            //bo obróciliśmy nasz ColliderPlane o -90
            //i wchodzimy od tyłu
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //wchodzimy od tyłu
            if (dotProduct < -0.01f) {
                //dlaczego minus? bo sprawdzamy od tyłu
                //skrypt przypielismy do ColliderPlane ktory odwrocilismy
                //a potrzebny nam jest rotacja między np. PortalA a PortalB
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);

                //ale postać gracza musi być obrócona
                //sprawdzcie efekt przy np. 180 - 1
                //i ustańcie na środku
                //jeśli wchodzi na wprost wtedy rotationDiff = 0
                rotationDiff += 180;

                //w końcu obracamy postać w okół osy y
                //oś y -> 0,1,0 (Vector3.up)
                player.Rotate(Vector3.up, rotationDiff);

                //teraz trzeba nanieść naszą rotację na gracza
                //interesuje nas tylko rotacja w osi y
                Vector3 positionRotationOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = receiver.position + positionRotationOffset;
                playerIsOverlapping = false;
            }
            else if (dotProduct > 0.01) {
                Debug.Log("Wchodzimy od przodu");
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            playerIsOverlapping = false;
        }
    }
}