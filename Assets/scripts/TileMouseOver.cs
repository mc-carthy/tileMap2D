using UnityEngine;
using System.Collections;

public class TileMouseOver : MonoBehaviour {

	private Camera mainCam;
	private Collider col;
	private Renderer ren;
	private Color origCol;
	[SerializeField]
	private Color highlightCol = Color.cyan;

	private void Start () {
		ren = GetComponent<Renderer> ();
		origCol = ren.material.color;
		mainCam = Camera.main;
		col = GetComponent<Collider> ();
	}

	private void Update () {

		Ray ray = mainCam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (col.Raycast (ray, out hit, 100)) {
			ren.material.color = highlightCol;
		} else {
			ren.material.color = origCol;
		}
	}
}
