using UnityEngine;
using System.Collections.Generic;

public class TileDataMap {

	protected class DataRoom {
		public int left; 
		public int bottom; 
		public int width; 
		public int height;

		public int right {
			get {
				return left + width - 1;
			}
		}

		public int top {
			get {
				return bottom + height - 1;
			}
		}

		public int centerX {
			get {
				return left + (width / 2);
			}
		}

		public int centerY {
			get {
				return bottom + (height / 2);
			}
		}

		public bool CollidesWith (DataRoom other) {
			if (left > other.right) {
				return false;
			}

			if (bottom > other.top) {
				return false;
			}

			if (right < other.left) {
				return false;
			}

			if (top < other.bottom) {
				return false;
			}

			return true;
		}
	}

	private int size_x;
	private int size_y;
	private int[,] map_data;

	private List<DataRoom> rooms;

	// 0 - Unkown
	// 1 - Floor
	// 2 - Wall
	// 3 - Stone

	[SerializeField]
	private int roomNum = 10;

	public TileDataMap(int size_x, int size_y) {
		this.size_x = size_x;
		this.size_y = size_y;

		map_data = new int [size_x, size_y];


		for (int x = 0; x < size_x; x++) {
			for (int y = 0; y < size_y; y++) {
				map_data [x, y] = 3;
			}
		}

		rooms = new List<DataRoom> ();

		for (int i = 0; i < roomNum; i++) {
			int rsx = Random.Range (4, 8);
			int rsy = Random.Range (4, 8);

			DataRoom r = new DataRoom ();
			r.left = Random.Range (0, size_x - rsx);
			r.bottom = Random.Range (0, size_y - rsy);
			r.width = rsx;
			r.height = rsy;

			if (!DoesRoomCollide (r)) {
				rooms.Add (r);
			} else {
				i--;
			}

			foreach (DataRoom r2 in rooms) {
				BuildRoom (r2);
			}
		}
		BuildCorridor (rooms [0], rooms [1]);

	}

	public int GetTileAt(int x, int y) {
		return map_data [x, y];
	}

	private void BuildRoom (DataRoom r) {

		for (int x = 0; x < r.width; x++) {
			for (int y = 0; y < r.height; y++) {
				if (x == 0 || x == r.width - 1 || y == 0 || y == r.height - 1) {
					map_data [r.left + x, r.bottom + y] = 2;
				} else {
					map_data [r.left + x, r.bottom + y] = 1;
				}
			}
		}

	}

	private bool DoesRoomCollide (DataRoom r) {
		foreach (DataRoom r2 in rooms) {
			if (r.CollidesWith (r2)) {
				return true;
			}
		}
		return false;
	}

	private void BuildCorridor (DataRoom r1, DataRoom r2) {
		int x = r1.centerX;
		int y = r1.centerY;

		while (x != r2.centerX) {
			map_data [x, y] = 1;
			x += x < r2.centerX ? 1 : -1;
		}

		while (y != r2.centerY) {
			map_data [x, y] = 1;
			y += y < r2.centerY ? 1 : -1;
		}

		/*
		while (x != r2.centerX || y != r2.centerY) {
			map_data [x, y] = 1;
			if (Mathf.Abs (x - r2.centerX) > Mathf.Abs (y - r2.centerY)) {
				x += x < r2.centerX ? 1 : -1;
			} else {
				y += y < r2.centerY ? 1 : -1;
			}
		}
		*/
	}
}
