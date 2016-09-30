using UnityEngine;
using System.Collections;

public class TileMouseOver : MonoBehaviour {

	private Renderer ren;
	private Color origCol;

	private void Start () {
		ren = GetComponent<Renderer> ();
		origCol = ren.material.color;
	}

	private void OnMouseOver () {
		ren.material.color = Color.cyan;
	}

	private void OnMouseExit () {
		ren.material.color = origCol;
	}
}
