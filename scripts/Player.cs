using Godot;

public partial class Player : CharacterBody2D
{
	public const float _Speed = 100.0f;

	// TODO: find out how to share this between the two scripts
	const int _TextureSize = 256;
	const float _TileScaleSize = 0.078125f;
	const float _RowSize = _TextureSize * _TileScaleSize;

	enum PlayerRow
	{
		Top = 1,
		Middle = 0,
		Bottom = -1
	}

	PlayerRow _currentRow = PlayerRow.Middle;
	PlayerRow _targetRow = PlayerRow.Middle;


	public override void _PhysicsProcess(double delta)
	{

		base._PhysicsProcess(delta);

		if (Input.IsActionJustPressed("ui_up") && _currentRow < PlayerRow.Top)
		{
			_currentRow += 1;
		}
		else if (Input.IsActionJustPressed("ui_down") && _currentRow > PlayerRow.Bottom)
		{
			_currentRow -= 1;
		}

		float targetY = (int)_targetRow * _RowSize;
		Position = Position.Lerp(new Vector2(Position.X, targetY), _Speed * (float)delta);

		// var TargetPosition = Vector2.Up * _currentRow * _RowSize;
		// var direction = Position.DirectionTo(TargetPosition).Normalized();
		// var velocity = direction * _Speed;

		// if (Position.DistanceTo(TargetPosition) > 1)
		// {
		// 	Velocity = velocity;
		// }
		// else
		// {
		// 	Velocity = Vector2.Zero;
		// }

		MoveAndSlide();
	}
}

