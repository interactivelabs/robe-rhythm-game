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
        if (Position.X < GameSettings.RowSize * 4 * -1)
        {
            QueueFree();
        }

        base._PhysicsProcess(delta);
    }

    void _on_body_entered()
    {
        // This method will be called when a body enters the coin's area
        GD.Print("A body has entered the coin's area!");

        EmitSignal(SignalName.ScoreUpdate, Value);

        QueueFree();

        // You can add logic here to handle what happens when a body enters the coin's area
        // For example, you might want to increase the player's score or play a sound effect
    }
}


