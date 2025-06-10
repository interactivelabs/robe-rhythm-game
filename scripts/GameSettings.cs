using Godot;
using System;

namespace Scripts;

public partial class GameSettings : Node
{
    public static GameSettings Instance { get; private set; }

    private const int TextureSize = 48;
    private const int ScreenChunks = 6;
    public const int RowsInColumn = 3;
    public const int MaxColumns = 12;
    public const float DefaultSpeed = 200.0f;

    public float TileScaleSize { get; private set; } = 0.5f;
    public float RowSize { get; private set; }

    public override void _Ready()
    {
        GD.Print("Game Settings Started!");

        Instance = this;
        RowSize = TextureSize * TileScaleSize;
    }

    public void ReCalculateScale(Rect2 viewportSize)
    {
        var verticalChunkSize = viewportSize.Size.Y / ScreenChunks;
        TileScaleSize = verticalChunkSize / TextureSize;
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
