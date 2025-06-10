using Godot;

namespace Scripts;

public partial class Game : Node2D
{
	private AudioStreamPlayer _songPlayer;

	private double _bpm = 152;
	private float _speed = 50.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Game Started!");
		_songPlayer = GetNode<AudioStreamPlayer>("Conductor");
		// songPlayer.Play();

		// _Speed = 60f / _bpm;
		_speed = GameSettings.DefaultSpeed;
		// _NextColumnOffset = GameSettings.RowSize;
	}

}
