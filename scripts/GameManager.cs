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

}
