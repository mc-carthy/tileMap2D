using UnityEngine;
using System.Collections.Generic;

public class TileDataMap {

	protected class DataRoom {
		public int left; 
		public int bottom; 
		public int width; 
		public int height;

		public bool isConnected = false;

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
	private int roomNum = 10;

	private List<DataRoom> rooms;

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
		for (int i = 0; i < 4; i++) {
			int rsx = Random.Range (4, 14);
			int rsy = Random.Range (4, 10);

			DataRoom r = new DataRoom ();

			r.left = Random.Range (0, size_x - rsx);
			r.bottom = Random.Range (0, size_y - rsy);
			r.width = rsx;
			r.height = rsy;


			//Debug.Log (!DoesRoomCollide(r));


			if (!DoesRoomCollide (r)) {
				rooms.Add (r);
			} else {
				i--;
			}
		}

		foreach (DataRoom room in rooms) {
			BuildRoom (room);
		}

		for (int i = 0; i < rooms.Count; i++) {
			if (!rooms[i].isConnected) {
				int j = Random.Range (1, rooms.Count);
				BuildCorridor (rooms[i], rooms [(i + j) % rooms.Count]);
			}
		}

		BuildWalls ();
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

		r1.isConnected = true;
		r2.isConnected = true;
	}

	private void BuildWalls () {
		for (int x = 0; x < size_x; x++) {
			for (int y = 0; y < size_y; y++) {
				if (map_data[x, y] == 3 && HasAdjacentFloor (x, y)) {
					map_data [x, y] = 2;
				}
			}
		}
	}

	private bool HasAdjacentFloor (int x, int y) {

		if (x > 0 && map_data [x - 1, y] == 1) {
			return true;
		}
		if (x < size_x - 1 && map_data [x + 1, y] == 1) {
			return true;
		}

		if (y > 0 && map_data [x, y - 1] == 1) {
			return true;
		}
		if (y < size_y - 1 && map_data [x, y + 1] == 1) {
			return true;
		}

		return false;
	}
}
