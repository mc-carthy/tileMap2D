using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class TileMap : MonoBehaviour {

	private MeshFilter mFil;
	private MeshRenderer mRen;
	private MeshCollider mCol;

	// Number of tiles
	[SerializeField]
	private int size_x = 100;
	[SerializeField]
	private int size_z = 50;

	public float tileSize = 1;

	private void Start () {
		mFil = GetComponent<MeshFilter> ();
		mRen = GetComponent<MeshRenderer> ();
		mCol = GetComponent<MeshCollider> ();

		BuildMesh ();
	}

	public void BuildMesh () {

		int numTiles = size_x * size_z;
		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVertices = vsize_x * vsize_z;

		int numTriangles = numTiles * 2;

		// Generate mesh data
		Vector3[] vertices = new Vector3[numVertices];
		Vector3[] normals = new Vector3[numVertices];
		Vector2[] uv = new Vector2[numVertices];

		int[] triangles = new int[numTriangles * 3];

		int x, z;

		for (z = 0; z < vsize_z; z++) {
			for (x = 0; x < vsize_x; x++) {

				vertices [z * vsize_x + x] = new Vector3 (x * tileSize, 0, z * tileSize);
				normals [z * vsize_x + x] = Vector3.up;
				uv [z * vsize_x + x] = new Vector2 ((float)x / vsize_x, (float)z / vsize_z);
			}
		}

		for (z = 0; z < size_z; z++) {
			for (x = 0; x < size_x; x++) {

				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;

				triangles[triOffset + 0] = z * vsize_x + x + 		   0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 1;

				triangles[triOffset + 3] = z * vsize_x + x + 		   0;
				triangles[triOffset + 4] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 5] = z * vsize_x + x + 		   1;

			}
		}

		// Create new mesh and populate it with the created data
		Mesh mesh = new Mesh ();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		mFil.mesh = mesh;
		mCol.sharedMesh = mesh;
	}
}
