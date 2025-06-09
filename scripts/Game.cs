namespace Scripts;

using Godot;

public partial class Game : Node2D
{

	[Export] public Node2D TerrainScene;

	[Export] public PackedScene[] Terrains;

	private AudioStreamPlayer _songPlayer;

	private double _bpm = 152;
	private float _speed = 50.0f;
	private float _nextColumnOffset = 0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Started!");

		_songPlayer = GetNode<AudioStreamPlayer>("Conductor");
		// songPlayer.Play();

		// _Speed = 60f / _bpm;
		_speed = GameSettings.DefaultSpeed;
		// _NextColumnOffset = GameSettings.RowSize;

		TerrainScene.GlobalPosition = new Vector2(0, 0);

		// Initialize the terrain
		InitializeTerrain();
	}

	public override void _PhysicsProcess(double delta)
	{
		var progress = _speed * (float)delta;
		_nextColumnOffset -= progress;
		if (_nextColumnOffset <= 0)
		{
			_nextColumnOffset = GameSettings.RowSize; // Reset the offset for the next column
			AddTerrainColumn(GameSettings.MaxColumns);
		}
		base._PhysicsProcess(delta);
	}

	private void InitializeTerrain()
	{
		for (var i = 0; i < GameSettings.MaxColumns; i++)
		{
			AddTerrainColumn(i);
		}
	}

	private void AddTerrainColumn(int column)
	{
		for (var j = -1; j < GameSettings.RowsInColumn - 1; j++)
		{
			AddTerrainBlock(column, j);
		}
	}

	private void AddTerrainBlock(int column, int row)
	{
		var terrain = Terrains[0];
		var terrainInstance = terrain.Instantiate<Node2D>();
		terrainInstance.GlobalPosition = new Vector2(column * GameSettings.RowSize, row * GameSettings.RowSize);
		terrainInstance.Scale = new Vector2(GameSettings.TileScaleSize, GameSettings.TileScaleSize);
		TerrainScene.AddChild(terrainInstance);
	}

}
