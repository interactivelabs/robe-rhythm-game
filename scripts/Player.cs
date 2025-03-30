using Godot;

public partial class Player : CharacterBody2D
{
	public const float _Speed = 100.0f;

	private int _currentRow = 0;

	// TODO: find out how to share this between the two scripts
	const int _TextureSize = 256;
	const float _TileScaleSize = 0.078125f;
	const float _TileOffsetSize = _TextureSize * _TileScaleSize;


	public override void _PhysicsProcess(double delta)
	{

		base._PhysicsProcess(delta);

		if (Input.IsActionJustPressed("ui_up") && _currentRow < 2)
		{
			_currentRow += 1;
		}
		else if (Input.IsActionJustPressed("ui_down") && _currentRow > 0)
		{
			_currentRow -= 1;
		}

		var TargetPosition = Vector2.Up * _currentRow * _TileOffsetSize;
		var direction = Position.DirectionTo(TargetPosition).Normalized();
		var velocity = direction * _Speed;

		if (Position.DistanceTo(TargetPosition) > 1)
		{
			Velocity = velocity;
		}
		else
		{
			Velocity = Vector2.Zero;
		}

		MoveAndSlide();
	}
}

