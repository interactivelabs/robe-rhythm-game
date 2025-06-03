using Godot;

public partial class GameManager : Node2D
{

	[Export] private Node2D _obstaclesScene;

	[Export] private PackedScene[] _obstacles;
	[Export] private PackedScene[] _pickups;

	Timer _spawnerTimer;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Manager Started!");
		_spawnerTimer = GetNode<Timer>("SpawnerTimer");
		_spawnerTimer.Start();
	}

	public void _on_spawner_timer_timeout()
	{
		GD.Print("Spawner Timer Timeout!");
		if (_obstacles.Length > 0)
		{
			var obstacle = _obstacles[GD.RandRange(0, _obstacles.Length - 1)];
			var obstacleInstance = obstacle.Instantiate<Node2D>();
			obstacleInstance.GlobalPosition = new Vector2(0, 0);
			// This is -2 because we start at -1 for the top row
			var row = GD.RandRange(-1, GameSettings.RowsInColumn - 2);
			obstacleInstance.GlobalPosition = new Vector2(GameSettings.MaxColumns * GameSettings.RowSize, row * GameSettings.RowSize);
			_obstaclesScene.AddChild(obstacleInstance);

		}
	}
}
