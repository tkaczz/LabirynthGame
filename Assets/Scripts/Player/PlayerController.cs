using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static int StatycznaZmienna = 1;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -10f;
    private Vector3 velocity;
    private CharacterController characterController = null;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity);

        //inny sposób na to samo
        /* Vector3 move = (transform.right * x + transform.forward * z) * Time.deltaTime * speed;
        move.y = gravity * Time.deltaTime;
        characterController.Move(move);
        */

        int liczba = InnaKlasa.JakasZmienna;
    }
}

public class InnaKlasa {
    public static int JakasZmienna;
}