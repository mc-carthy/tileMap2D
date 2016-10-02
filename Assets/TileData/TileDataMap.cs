public class TileDataMap {

	private TileDataTile[] _tiles;
	private int _width;
	private int _height;

	public TileDataMap(int width, int height) {
		_width = width;
		_height = height;

		_tiles = new TileDataTile[_width * _height];
	}

	public TileDataTile GetTile (int x, int y) {
		if (x < 0 || x >= _width || y < 0 || y >= _height) {
			return null;
		}
		return _tiles [y * _width + x];
	}
}
