namespace Scripts;

using Godot;

public partial class Player : CharacterBody2D
{
	private const float PlayerSpeed = 2.0f;

	private enum PlayerRow
	{
		Top = -1,
		Middle = 0,
		Bottom = 1
	}

	private PlayerRow _currentRow = PlayerRow.Middle;
	private PlayerRow _targetRow = PlayerRow.Middle;

	public int Health { get; set; } = 100;

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

		if (Health <= 0)
		{
			GD.Print("PLayer Dead");
		}

		var targetY = (int)_targetRow * GameSettings.RowSize;
		Position = new Vector2(0, targetY);
		Velocity = new Vector2(0, Mathf.Lerp(0, targetY, PlayerSpeed * (float)delta));

		MoveAndSlide();
	}
}
