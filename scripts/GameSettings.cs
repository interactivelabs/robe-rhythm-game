using Godot;

public partial class GameSettings
{
    public const int TextureSize = 16;
    public const float TileScaleSize = 1f;
    public const float RowSize = TextureSize * TileScaleSize; // 1 pixel offset add gaps

    public const int RowsInColumn = 3;
    public const int MaxColumns = 20;

    public const float DefaultSpeed = 50.0f;
}
