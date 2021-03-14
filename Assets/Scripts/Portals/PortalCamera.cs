using UnityEngine;

public class PortalCamera : MonoBehaviour {
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    private float myAngle;

    private void Update() {
        //przesuwamy naszą kamerę w oparciu o odległość od drugiego portalu
        Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        transform.position = portal.position + playerOffsetFromPortal;

        //podobnie robimy z rotacją
        //tym razem tylko między portalami
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        if (myAngle == 90 || myAngle == 270) {
            angularDifferenceBetweenPortalRotations -= 90;
        }

        //wyliczamy kąt obrotu kamery ze wcześniejszym uwzględnieniem rotacji portali
        //w oparciu o Vector3.up czyli kąt w y
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);

        //obrót kamery w y
        Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;

        //może się zdarzyć że kamery w portalach będą inaczej ustawione
        if (myAngle == 90 || myAngle == 270) {
            newCameraDirection = new Vector3(newCameraDirection.z * -1, newCameraDirection.y * 1, newCameraDirection.x * 1);
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
        else {
            newCameraDirection = new Vector3(newCameraDirection.x * -1, newCameraDirection.y * 1, newCameraDirection.z * -1);
            transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
        }
    }
}