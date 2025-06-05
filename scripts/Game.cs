using Godot;

public partial class Game : Node2D
{

	[Export] private Node2D _terrainScene;

	[Export] private PackedScene[] _terrain;

	AudioStreamPlayer songPlayer;

	private double _bpm = 152;
	private float _Speed = 50.0f;

	private float _NextColumnOffset = 0f;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Started!");

		songPlayer = GetNode<AudioStreamPlayer>("Conductor");
		// songPlayer.Play();

		// _Speed = 60f / _bpm;
		_Speed = GameSettings.DefaultSpeed;
		// _NextColumnOffset = GameSettings.RowSize;

		_terrainScene.GlobalPosition = new Vector2(0, 0);

		// Initialize the terrain
		initializeTerrain();
	}

	public override void _PhysicsProcess(double delta)
	{
		var progress = _Speed * (float)delta;
		_NextColumnOffset -= progress;
		if (_NextColumnOffset <= 0)
		{
			_NextColumnOffset = GameSettings.RowSize; // Reset the offset for the next column
			for (int j = -1; j < GameSettings.RowsInColumn - 1; j++)
			{
				AddTerrainBlock(GameSettings.MaxColumns, j);
			}
		}
		base._PhysicsProcess(delta);
	}

	private void initializeTerrain()
	{
		for (int i = 0; i < GameSettings.MaxColumns; i++)
		{
			for (int j = -1; j < GameSettings.RowsInColumn - 1; j++)
			{
				AddTerrainBlock(i, j);
			}
		}
	}

	private void AddTerrainBlock(int column, int row)
	{
		var terrain = _terrain[0];
		var terrainInstance = terrain.Instantiate<Node2D>();
		terrainInstance.GlobalPosition = new Vector2(column * GameSettings.RowSize, row * GameSettings.RowSize);
		terrainInstance.Scale = new Vector2(GameSettings.TileScaleSize, GameSettings.TileScaleSize);
		_terrainScene.AddChild(terrainInstance);
	}

}
