using Godot;

namespace Scripts;

public partial class LeftMovingNode : Node2D
{
    [Export] public float Speed { get; set; }

    [Export] public VisibleOnScreenNotifier2D VisibleOnScreen { get; set; }

    public override void _Ready()
    {
        if (Speed == 0) Speed = GameSettings.DefaultSpeed;
        VisibleOnScreen.ScreenExited += QueueFree;
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        Position -= new Vector2(Speed * (float)delta, 0);
        base._PhysicsProcess(delta);
    }
}
