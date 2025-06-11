using Godot;

namespace Scripts;

public partial class LeftMovingNode : Node2D
{
    [Export] public VisibleOnScreenNotifier2D VisibleOnScreen { get; set; }

    private float _speed;

    public override void _Ready()
    {
        GameSettings.Instance.OnSpeedChanged += speed => _speed = speed;
        _speed = GameSettings.Instance.Speed;

        VisibleOnScreen.ScreenExited += QueueFree;
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        Position -= new Vector2(_speed * (float)delta, 0);
        base._PhysicsProcess(delta);
    }
}
