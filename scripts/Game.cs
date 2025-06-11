using Godot;

namespace Scripts;

public partial class Game : Node2D
{
    public override void _Ready()
    {
        GD.Print("Game Started!");
        base._Ready();
    }
}
