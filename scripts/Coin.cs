namespace Scripts;

using Godot;

public partial class Coin : Area2D
{

    [Signal]
    public delegate void ScoreUpdateEventHandler(int score);

    [Export]
    public float Speed { get; set; }

    [Export]
    public int Value { get; set; } = 1;

    public override void _Ready()
    {
        if (Speed == 0)
        {
            Speed = GameSettings.DefaultSpeed;
        }
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        Position -= new Vector2(Speed * (float)delta, 0);

        // TODO: check if the texture is off-screen
        // If the texture is off-screen, remove it from the scene
        if (Position.X < GameSettings.RowSize * 2 * -1)
        {
            QueueFree();
        }

        base._PhysicsProcess(delta);
    }

    public void _on_body_entered(Node2D body)
    {
        EmitSignal(SignalName.ScoreUpdate, Value);

        QueueFree();
    }
}
