using Godot;
using System;

namespace Scripts;

public partial class GameSettings : Node
{
    public static GameSettings Instance { get; private set; }

    private const int TextureSize = 48;

    public float TileScaleSize = 0.5f;
    public float RowSize;

    public const int RowsInColumn = 3;
    public const int MaxColumns = 10;

    public const float DefaultSpeed = 50.0f;

    public override void _Ready()
    {
        Instance = this;

        RowSize = TextureSize * TileScaleSize;
    }

    public static int GetRandomNumber(int min, int max, int? exclude = null)
    {
        var random = new Random();
        var value = random.Next(min, max);
        if (exclude == null || value != exclude) return value;
        while (value == exclude) value = random.Next(min, max);
        return value;
    }
}
