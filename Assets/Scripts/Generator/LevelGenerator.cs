using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
	public Texture2D map;
	public List<ColorToPrefab> colorMappings;
	public float offset = 5f;

	public Material material01;
	public Material material02;

	//unity operuje wg. innych współrzędnych dlatego "z" a nie "y"
	public void GenerateTile(int x, int z) {
		Color pixelColor = map.GetPixel(x, z);

		if (pixelColor.a == 0) {
			return;
		}

		foreach (var colorMapping in colorMappings) {
			if (colorMapping.color == pixelColor) {
				Vector3 position = new Vector3(x, 0, z) * offset;
				Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
			}
		}
	}

	public void ColorTheChildren() {
		foreach (var child in transform.GetComponentsInChildren<Transform>()) {
			if (child.tag == "Wall") {
				if (Random.Range(1, 100) % 3 == 0) {
					child.gameObject.GetComponent<Renderer>().material = material02;
				}
				else {
					child.gameObject.GetComponent<Renderer>().material = material01;
				}
			}
		}
	}

	public void GenerateLabirynth() {
		for (int x = 0; x < map.width; x++) {
			for (int z = 0; z < map.height; z++) {
				GenerateTile(x, z);
			}
		}

		ColorTheChildren();
	}
}