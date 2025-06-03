using Godot;

public partial class Game : Node2D
{

	[Export] private Node2D _scene;

	[Export] private PackedScene[] _tiles;

	AudioStreamPlayer songPlayer;

	const int _TextureSize = 256;
	const float _TileScaleSize = 0.078125f;
	const float _TileOffsetSize = _TextureSize * _TileScaleSize;
	const int RowsInColumn = 3;
	const int MaxColumns = 15;
	private float _currentColumnProgress = 0;
	private int _playerColumnProgress = 0;
	private int _Speed = 2;
	private int _MaxSpeed = 10;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Started!");
		songPlayer = GetNode<AudioStreamPlayer>("Conductor");
		// songPlayer.Play();

		_scene.GlobalPosition = new Vector2(0, 0);

		// Initialize the level with Empty Columns
		for (int i = -4; i <= MaxColumns; i++)
		{
			InstantiateColumn(i);
		}
	}

	public override void _PhysicsProcess(double delta)
	{

		float progress = _TileOffsetSize * _Speed * (float)delta;
		foreach (Node n in _scene.GetChildren())
			((Node2D)n).Translate(Vector2.Left * progress);

		_currentColumnProgress += progress;
		if (_currentColumnProgress >= _TileOffsetSize)
		{
			_playerColumnProgress += 1;
			_currentColumnProgress = 0;
			InstantiateColumn(MaxColumns);
			_scene.GetChild(0).QueueFree();
		}

		base._PhysicsProcess(delta);

	}

	private void InstantiateColumn(int screenOffset)
	{
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
