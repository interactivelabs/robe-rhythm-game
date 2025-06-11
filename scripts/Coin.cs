using Godot;

namespace Scripts;

public delegate void ScoreEventHandler(int value);

public partial class Coin : LeftMovingNode
{
    public event ScoreEventHandler OnScore;

    [Export] public AudioStreamPlayer2D PickupSound;
    [Export] public int Value { get; set; } = 1;

    public override void _Ready()
    {
        PickupSound.Finished += FinishedPickup;
        base._Ready();
    }

    private void _on_body_entered(Node2D body)
    {
        if (body is not CharacterBody2D) return;
        PickupSound.Play();
        OnScore?.Invoke(Value);
    }

    private void FinishedPickup()
    {
        QueueFree();
    }
}
