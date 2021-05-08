using UnityEngine;

public class Spikes : MonoBehaviour {
	[SerializeField] private float force;

	//jeśli wejdziemy w trigger, to jest pierwszy obiekt od rodzica
	private void OnTriggerStay(Collider other) {
		//...i jeśli to gracz
		if (other.tag == "Player") {
			//...to chcemy go odrzucić
			//to właśnie nam daje InverseTransformDirection
			//przeciwieństwo TransformDirection tzn. jeśli z lokalnych współrzędnych,
			//chcemy przenieść np. Vector3 na globalne współrzędne, tzn. takich dla obiektów z samej góry
			//https://docs.unity3d.com/ScriptReference/Transform.InverseTransformDirection.html
			Vector3 recoil =
				transform.parent.InverseTransformDirection(transform.right) * Random.Range(-0.1f, 0.1f) + //w osi x
				transform.parent.InverseTransformDirection(transform.forward) * -1f; //w osi z

			//po wyznaczeniu kierunku manipulujemy CharacterController z obiektu gracza
			other.GetComponent<CharacterController>().Move(recoil * Time.deltaTime * force);
		}
	}
}