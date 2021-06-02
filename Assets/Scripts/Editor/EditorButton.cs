using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelGenerator))]
public class EditorButton : Editor {

	public override void OnInspectorGUI() {
		DrawDefaultInspector();

		var generator = (LevelGenerator)target;

		if (GUILayout.Button("Create labirynth")) {
			generator.GenerateLabirynth();
		}

		if (GUILayout.Button("Remove labirynth")) {
			generator.RemoveLabirynth();
		}
	}
}