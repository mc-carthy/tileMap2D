using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {

	private MeshFilter mFil;
	private MeshRenderer mRen;
	private MeshCollider mCol;

	private void Start () {
		mFil = GetComponent<MeshFilter> ();
		mRen = GetComponent<MeshRenderer> ();
		mCol = GetComponent<MeshCollider> ();

		BuildMesh ();
	}

	private void BuildMesh () {

		// Generate mesh data
		Vector3[] vertices = new Vector3[4];
		int[] triangles = new int[6];
		Vector3[] normals = new Vector3[4];

		vertices [0] = new Vector3 (0, 0, 0);
		vertices [1] = new Vector3 (1, 0, 0);
		vertices [2] = new Vector3 (0, 0, -1);
		vertices [3] = new Vector3 (1, 0, -1);

		triangles [0] = 0;
		triangles [1] = 3;
		triangles [2] = 2;

		triangles [3] = 0;
		triangles [4] = 1;
		triangles [5] = 3;

		normals [0] = new Vector3 (0, 0, 0);


		// Create new mesh and populate it with the created data
		Mesh mesh = new Mesh ();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;

		mFil.mesh = mesh;
	}
}
