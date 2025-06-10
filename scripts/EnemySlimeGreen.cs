using Godot;

namespace Scripts;

public delegate void DamageEventHandler(int value);

public partial class EnemySlimeGreen : LeftMovingNode
{
    public event DamageEventHandler OnDamage;

    [Export] public int Value { get; set; } = 1;

    private void _on_body_entered(Node2D body)
    {
        if (body is not CharacterBody2D) return;
        GD.Print("Player damaged");
        OnDamage?.Invoke(Value);
    }
}
