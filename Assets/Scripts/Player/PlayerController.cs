using UnityEngine;

public class PlayerController : MonoBehaviour {
    public static int StatycznaZmienna = 1;

    [SerializeField] private Transform groundCheck = null;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;

    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -10f;
    private Vector3 velocity;
    private CharacterController characterController = null;

    private void Awake() {
        characterController = GetComponent<CharacterController>();
    }

    private void Update() {
        //pobiera dane z klawiatury
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //porusza nasza postacia
        if (Physics.Raycast(
            groundCheck.position,
            transform.TransformDirection(Vector3.down),
            out RaycastHit hit,
            0.4f,
            groundMask)) {
            string terrainType = hit.collider.gameObject.tag;

            switch (terrainType) {
                default:
                    speed = 12f;
                    break;
                case "Low":
                    speed = 3f;
                    break;
                case "High":
                    speed = 20f;
                    break;
            }
        }
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        //obsluguje nasza grawitacje
        isGrounded = Physics.CheckSphere(
            groundCheck.position,
            0.4f,
            groundMask
        );
        velocity.y += gravity * Time.deltaTime;

        //czy spadamy
        if (!isGrounded && velocity.y < 0) {
            velocity.y = -2;
        }
        //czy jesteśmy na ziemii
        else {
            velocity.y = 0;
        }

        characterController.Move(velocity);
    }

    //to jest nasz kod wchodzący w interakcję z obiektami mającymi tag "PickUp"
    //jak dołożymy inne rodzaje pickup-ów to nie będziemy musieli tego później zmieniać
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "PickUp") {
            other.gameObject.GetComponent<PickUp>().Pick();
        }
    }
}