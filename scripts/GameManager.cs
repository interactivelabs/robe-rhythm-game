using Godot;

public partial class GameManager : Node2D
{

	[Export] private Node2D _obstaclesScene;

	[Export] private Node2D _pickupsScene;

	[Export] private PackedScene[] _obstacles;
	[Export] private PackedScene[] _pickups;

	Timer _spawnerTimer;

	private float _Speed = 50.0f;
	private float _NextColumnOffset = 0f;

	private bool _canAddObstacles = true;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Manager Started!");
		_spawnerTimer = GetNode<Timer>("SpawnerTimer");
		_spawnerTimer.Start();

		_Speed = GameSettings.DefaultSpeed;
		// _NextColumnOffset = GameSettings.RowSize;
	}

	public override void _PhysicsProcess(double delta)
	{
		var progress = _Speed * (float)delta;
		_NextColumnOffset -= progress;
		if (_NextColumnOffset <= 0 && _obstacles.Length > 0 && _canAddObstacles)
		{
			var obstacle = _obstacles[GD.RandRange(0, _obstacles.Length - 1)];
			var obstacleInstance = obstacle.Instantiate<Node2D>();
			obstacleInstance.GlobalPosition = new Vector2(0, 0);
			// This is -2 because we start at -1 for the top row
			var obstacleRow = GD.RandRange(-1, GameSettings.RowsInColumn - 2);
			obstacleInstance.GlobalPosition = new Vector2(GameSettings.MaxColumns * GameSettings.RowSize, obstacleRow * GameSettings.RowSize);
			_obstaclesScene.AddChild(obstacleInstance);
			// _canAddObstacles = false;

			// TODO: Add a pickup after every 3 obstacles

			var pickup = _pickups[GD.RandRange(0, _pickups.Length - 1)];
			var pickupInstance = pickup.Instantiate<Node2D>();
			pickupInstance.GlobalPosition = new Vector2(0, 0);
			// This is -2 because we start at -1 for the top row

			var pickupRow = obstacleRow;
			while (pickupRow == obstacleRow)
			{
				pickupRow = GD.RandRange(-1, GameSettings.RowsInColumn - 2);
			}

			pickupInstance.GlobalPosition = new Vector2(GameSettings.MaxColumns * GameSettings.RowSize, pickupRow * GameSettings.RowSize);
			_pickupsScene.AddChild(pickupInstance);
			_canAddObstacles = false;
		}

		if (_NextColumnOffset <= 0)
		{
			_NextColumnOffset = GameSettings.RowSize; // Reset the offset for the next column
		}

		base._PhysicsProcess(delta);
	}

	public void _on_spawner_timer_timeout()
	{
		_canAddObstacles = true;
	}

}
