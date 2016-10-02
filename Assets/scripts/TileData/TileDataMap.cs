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

	List<DataRoom> rooms;

	// 0 - Unkown
	// 1 - Floor
	// 2 - Wall
	// 3 - Stone

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

		for (int i = 0; i < 10; i++) {
			int rsx = Random.Range (4, 8);
			int rsy = Random.Range (4, 8);

			DataRoom r = new DataRoom ();
			r.left = Random.Range (0, size_x - rsx);
			r.bottom = Random.Range (0, size_y - rsy);
			r.width = rsx;
			r.height = rsy;

			if (!DoesRoomCollide (r)) {
				rooms.Add (r);

				MakeRoom (r);
			} else {
				i--;
			}
		}

	}

	public int GetTileAt(int x, int y) {
		return map_data [x, y];
	}

	private void MakeRoom (DataRoom r) {

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
}
