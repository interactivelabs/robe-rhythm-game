using Godot;
using System;

public partial class Texture : Sprite2D
{

	[Export]
	public float speed { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		GlobalPosition += new Vector2(speed * (float)delta, 0);

		if (Position.Y > GetViewportRect().Size.X)
		{
			QueueFree();
		}

		base._PhysicsProcess(delta);
	}
}
