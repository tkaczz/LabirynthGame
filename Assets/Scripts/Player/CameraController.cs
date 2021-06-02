using UnityEngine;

public class CameraController : MonoBehaviour {
	[Tooltip("Czułość myszy")]
	[SerializeField] private float mouseSensivity = 100f;

	[SerializeField] private bool debug = false;

	private Transform playerBody = null;
	private Transform cameraTransform = null;

	private float xRotation = 0f;

	//...a w Awake szukający obiektów
	private void Awake() {
		playerBody = transform.parent;
		cameraTransform = GetComponent<Transform>();
	}

	//dobrym zwyczajem jest żeby w Start() umieszczać kod "konfigurujący"...
	private void Start() {
		//tutaj sprawdzamy czy NIE włączyliśmy flagę debug, i czy poniższe NIE uruchamia się w edytorze
		if (debug && Application.isEditor) {
			Cursor.lockState = CursorLockMode.None;
		}
		else {
			Cursor.lockState = CursorLockMode.Locked;
		}
	}

	private void Update() {
		//czego głównie potrzebujemy żeby poruszyć kamerę?
		//dlaczego * Time.deltaTime?
		//bez mouseSensivity mouseX, mouseY byłyby bardzo małe, więc do czego ostatecznie by to doprowadziło?
		float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime;

		//wyliczenie różnicy
		xRotation -= mouseY;

		xRotation = Mathf.Clamp(xRotation, -90f, 80f);

		cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		playerBody.Rotate(Vector3.up * mouseX);
	}
}