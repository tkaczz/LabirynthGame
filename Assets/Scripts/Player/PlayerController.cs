using UnityEngine;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -10f;
    private Vector3 velocity;
    private float velocity2;
    private CharacterController characterController = null;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        //musimy skądś pobrac informacje, jak gracz chce sie przesunąć
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        //inny sposób na to samo
        /* Vector3 move = (transform.right * x + transform.forward * z) * Time.deltaTime * speed;
        move.y = gravity * Time.deltaTime;
        characterController.Move(move);
        */
    }
}