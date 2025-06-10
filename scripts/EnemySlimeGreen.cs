using Godot;

namespace Scripts;

public delegate void DamageEventHandler(int damageValue);

public partial class EnemySlimeGreen : LeftMovingNode
{
    public event DamageEventHandler OnDamage;

    [Export] public int DamageValue { get; set; } = 1;

    private void _on_body_entered(Node2D body)
    {
        if (body is not CharacterBody2D) return;
        OnDamage?.Invoke(DamageValue);
    }
}