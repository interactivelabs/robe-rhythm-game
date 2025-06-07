namespace Scripts;

using Godot;

public partial class EnemySlimeGreen : CharacterBody2D
{
    [Export]
    public float Speed { get; set; }

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
        if (Position.X < -80)
        {
            QueueFree();
        }

        base._PhysicsProcess(delta);
    }
}
