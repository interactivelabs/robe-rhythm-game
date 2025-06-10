using Godot;

namespace Scripts;

public delegate void ScoreEventHandler(int value);

public partial class Coin : LeftMovingNode
{
    public event ScoreEventHandler OnScore;

    [Export] public int Value { get; set; } = 1;

    private void _on_body_entered(Node2D body)
    {
        if (body is not CharacterBody2D) return;
        OnScore?.Invoke(Value);
        QueueFree();
    }
}
