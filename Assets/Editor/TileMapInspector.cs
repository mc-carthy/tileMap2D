using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(TileGraphicsMap))]
public class TileMapInspector : Editor {

	public override void OnInspectorGUI () {
		base.OnInspectorGUI();

		if (GUILayout.Button("Regenerate")) {
			TileGraphicsMap tileMap = (TileGraphicsMap)target;
			tileMap.BuildMesh ();
		}
	}
}
