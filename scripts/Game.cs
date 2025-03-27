using Godot;

public partial class Game : Node2D
{

	[Export] private Node2D _scene;

	[Export] private PackedScene[] _tiles;

	private float _levelSpeed = 1f;

	AudioStreamPlayer songPlayer;



	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Started!");
		songPlayer = GetNode<AudioStreamPlayer>("Conductor");
		// songPlayer.Play();

		InstantiateColumn();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void InstantiateTile(float position = 0)
	{
		GD.Print("POSITION: ", position);
		Node2D tile = _tiles[GD.RandRange(0, 2)].Instantiate<Node2D>();
		_scene.AddChild(tile);
		tile.Scale = new Vector2(0.07f, 0.07f);
		tile.Position = Vector2.Up * position * 20;
	}

	private void InstantiateColumn()
	{
		for (int i = -1; i < 2; i++)
		{
			InstantiateTile(i);
		}
	}
}
