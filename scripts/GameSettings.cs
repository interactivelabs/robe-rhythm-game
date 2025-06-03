using Godot;

public partial class GameSettings
{
    public const int TextureSize = 256;
    public const float TileScaleSize = 0.07f;
    public const float RowSize = TextureSize * TileScaleSize + 2.0f; // 1 pixel offset add gaps

    public const int RowsInColumn = 3;
    public const int MaxColumns = 15;

    public const float DefaultSpeed = 50.0f;
}
