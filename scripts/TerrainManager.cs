using Godot;

namespace Scripts;

public partial class TerrainManager : Node2D
{
    [Export] public Node2D TerrainScene;
    [Export] public PackedScene[] Terrains;

    private float _speed;
    private float _nextColumnOffset;

    public override void _Ready()
    {
        GD.Print("TerrainManager started");
        GameSettings.Instance.ReCalculateScale(GetViewportRect());

        GameSettings.Instance.OnSpeedChanged += speed => _speed = speed;
        _speed = GameSettings.Instance.Speed;

        var middle = GetViewportRect().Size.Y / 2;
        TerrainScene.GlobalPosition = new Vector2(0, middle);

        // Initialize the terrain
        InitializeTerrain();
    }

    public override void _PhysicsProcess(double delta)
    {
        var progress = _speed * (float)delta;
        _nextColumnOffset -= progress;
        if (_nextColumnOffset <= 0)
        {
            _nextColumnOffset = GameSettings.Instance.RowSize; // Reset the offset for the next column
            AddTerrainColumn(GameSettings.MaxColumns);
        }

        base._PhysicsProcess(delta);
    }

    private void InitializeTerrain()
    {
        for (var i = 0; i < GameSettings.MaxColumns; i++) AddTerrainColumn(i);
    }

    private void AddTerrainColumn(int column)
    {
        for (var j = -1; j < GameSettings.RowsInColumn - 1; j++) AddTerrainBlock(column, j);
    }

    private void AddTerrainBlock(int column, int row)
    {
        var terrain = Terrains[0];
        var terrainInstance = terrain.Instantiate<Node2D>();

        terrainInstance.GlobalPosition =
            new Vector2(column * GameSettings.Instance.RowSize, row * GameSettings.Instance.RowSize);
        terrainInstance.Scale = new Vector2(GameSettings.Instance.TileScaleSize, GameSettings.Instance.TileScaleSize);
        TerrainScene.AddChild(terrainInstance);
    }
}
