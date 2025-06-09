using System;

namespace Scripts;

using Godot;

public partial class GameSettings : Node
{
    private const int TextureSize = 48;
    public const float TileScaleSize = 0.5f;
    public const float RowSize = TextureSize * TileScaleSize;

    public const int RowsInColumn = 3;
    public const int MaxColumns = 10;

    public const float DefaultSpeed = 50.0f;

    public static int GetRandomNumber(int min, int max, int? exclude = null)
    {
        var random = new Random();
        var value = random.Next(min, max);
        if (exclude == null || value != exclude) return value;
        while (value == exclude) value = random.Next(min, max);
        return value;
    }
}
