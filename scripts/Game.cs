using Godot;

public partial class Game : Node2D
{

	[Export] private Node2D _scene;

	[Export] private PackedScene[] _tiles;

	AudioStreamPlayer songPlayer;

	const int _TextureSize = 256;
	const float _TileScaleSize = 0.07f;
	const float _TileOffsetSize = _TextureSize * _TileScaleSize + 1;
	const int RowsInColumn = 3;

	private int _currentTailTile;
	private float _currentGroupProgress;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Started!");
		songPlayer = GetNode<AudioStreamPlayer>("Conductor");
		// songPlayer.Play();

		_currentTailTile = 15;
		_currentGroupProgress = 0;

		// Initialize the level with Empty Columns
		for (int i = -4; i < _currentTailTile; i++)
		{
			InstantiateColumn(i);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		float progress = _TileOffsetSize * (float)delta;
		foreach (Node n in _scene.GetChildren())
			((Node2D)n).Translate(Vector2.Left * progress);

		_currentGroupProgress += progress;
		if (_currentGroupProgress > _TileOffsetSize)
		{
			_currentTailTile += 1;
			_currentGroupProgress = 0;

			InstantiateColumn(_currentTailTile);
		}
	}

	private void InstantiateColumn(int screenOffset)
	{
		GD.Print("Game Started: ", screenOffset);
		Node2D column = GetTilesColumn(RowsInColumn);
		column.GlobalPosition = Vector2.Right * screenOffset * _TileOffsetSize;

		_scene.AddChild(column);
	}

	private Sprite2D GetTileInstance(float position = 0)
	{
		Sprite2D tile = _tiles[0].Instantiate<Sprite2D>();
		tile.Scale = new Vector2(0.07f, 0.07f);
		tile.Position = Vector2.Up * position * _TileOffsetSize;
		return tile;
	}

	private Node2D GetTilesColumn(int rowsInColumn)
	{
		var column = new Node2D();
		for (int i = 0; i < rowsInColumn; i++)
		{
			Sprite2D tile = GetTileInstance(i);
			column.AddChild(tile);
		}
		return column;
	}
}
