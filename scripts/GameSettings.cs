using Godot;

public partial class GameSettings
{
    private const int TextureSize = 48;
    public const float TileScaleSize = 0.5f;
    public const float RowSize = TextureSize * TileScaleSize;

    public const int RowsInColumn = 3;
    public const int MaxColumns = 20;

    public const float DefaultSpeed = 50.0f;
}
