using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour {

	private Camera mainCam;
	private Collider col;
	private Renderer ren;
	private TileMap tileMap;
	private Color origCol;
	[SerializeField]
	private Color highlightCol = Color.cyan;
	[SerializeField]
	private Transform selectionCube;
	private Vector3 currentTileCoord;

	private void Start () {
		tileMap = GetComponent<TileMap> ();
		ren = GetComponent<Renderer> ();
		origCol = ren.material.color;
		mainCam = Camera.main;
		col = GetComponent<Collider> ();
	}

	private void Update () {

		Ray ray = mainCam.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;

		if (col.Raycast (ray, out hit, 100)) {
			int x = Mathf.FloorToInt (hit.point.x / tileMap.tileSize);
			int z = Mathf.FloorToInt (hit.point.z / tileMap.tileSize);

			currentTileCoord.x = x;
			currentTileCoord.z = z;

			selectionCube.transform.position = currentTileCoord;
		} else {
			ren.material.color = origCol;
		}
	}
}
