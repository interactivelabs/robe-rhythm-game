using Godot;

public partial class Player : CharacterBody2D
{
	public const float _PlayerSpeed = 2.0f;

	enum PlayerRow
	{
		Top = -1,
		Middle = 0,
		Bottom = 1
	}

	PlayerRow _currentRow = PlayerRow.Middle;
	PlayerRow _targetRow = PlayerRow.Middle;


	public override void _PhysicsProcess(double delta)
	{

		base._PhysicsProcess(delta);

		if (Input.IsActionJustPressed("ui_up") && _targetRow > PlayerRow.Top)
		{
			_targetRow -= 1;
		}
		else if (Input.IsActionJustPressed("ui_down") && _targetRow < PlayerRow.Bottom)
		{
			_targetRow += 1;
		}

		float targetY = (int)_targetRow * GameSettings.RowSize;
		Position = new Vector2(0, targetY);
		Velocity = new Vector2(0, Mathf.Lerp(0, targetY, _PlayerSpeed * (float)delta));

		MoveAndSlide();
	}
}
