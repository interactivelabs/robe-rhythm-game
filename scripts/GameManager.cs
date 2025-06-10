using Godot;

namespace Scripts;

public partial class GameManager : Node2D
{
    [Export] public Node2D ObstaclesScene;
    [Export] public Node2D PickupsScene;
    [Export] public PackedScene[] Obstacles;
    [Export] public PackedScene[] Pickups;

    private Player _player;
    private Timer _spawnerTimer;
    private GameUi _gameUi;

    private int _score;
    private float _speed = 50.0f;
    private float _nextColumnOffset;

    private bool _canAddObstacles = true;
    private int _canAddPickups;

    public override void _Ready()
    {
        GD.Print("Game Manager Started!");

        var middle = GetViewportRect().Size.Y / 2;
        ObstaclesScene.GlobalPosition = new Vector2(0, middle);
        PickupsScene.GlobalPosition = new Vector2(0, middle);

        _gameUi = GetNode<GameUi>("../CanvasGameUI/GameUI");
        _spawnerTimer = GetNode<Timer>("SpawnerTimer");
        _spawnerTimer.Start();

        _speed = GameSettings.DefaultSpeed;
        _score = 0;

        _player = GetNode<Player>("../Player");
        _player.OnPlayerStateChange += OnPlayerStateChange;
    }

    public override void _PhysicsProcess(double delta)
    {
        var obstacleRow = GameSettings.GetRandomNumber(-1, GameSettings.RowsInColumn - 1);
        var progress = _speed * (float)delta;
        _nextColumnOffset -= progress;
        if (_nextColumnOffset <= 0 && Obstacles.Length > 0 && _canAddObstacles)
        {
            AddObstacle(obstacleRow);
            _canAddObstacles = false;
        }

        if (_nextColumnOffset <= 0 && Obstacles.Length > 0 && _canAddPickups <= 0)
        {
            var pickupRow = GameSettings.GetRandomNumber(-1, GameSettings.RowsInColumn - 1, obstacleRow);
            AddPickup(pickupRow);
            _canAddPickups = GameSettings.GetRandomNumber(1, 3);
        }

        if (_nextColumnOffset <= 0)
            _nextColumnOffset = GameSettings.Instance.RowSize; // Reset the offset for the next column

        base._PhysicsProcess(delta);
    }

    private void _on_spawner_timer_timeout()
    {
        _canAddObstacles = true;
        _canAddPickups -= 1;
    }

    private void AddObstacle(int obstacleRow)
    {
        var obstacleScale = GameSettings.Instance.TileScaleSize * 1.4f;
        var obstacle = Obstacles[GD.RandRange(0, Obstacles.Length - 1)];
        var obstacleInstance = obstacle.Instantiate<EnemySlimeGreen>();
        obstacleInstance.GlobalPosition = new Vector2(0, 0);
        obstacleInstance.GlobalPosition = new Vector2(GameSettings.MaxColumns * GameSettings.Instance.RowSize,
            obstacleRow * GameSettings.Instance.RowSize);
        obstacleInstance.Scale = new Vector2(obstacleScale, obstacleScale);
        obstacleInstance.DamageValue = GameSettings.GetRandomNumber(10, 20);
        obstacleInstance.OnDamage += OnPlayerDamage;
        ObstaclesScene.CallDeferred(Node.MethodName.AddChild, obstacleInstance);
    }

    private void OnPlayerDamage(int value)
    {
        _player.DamageTaken(value);
        _gameUi.UpdateHealthLabel(_player.Health);
    }

    private void OnPlayerStateChange(PlayerState playerState)
    {
        if (playerState == PlayerState.Dead)
        {
            GD.Print("Game Over");
            // TODO: Add game over and restart UI
        }
    }

    private void AddPickup(int pickupRow)
    {
        var pickupScale = GameSettings.Instance.TileScaleSize * 1.5f;
        var pickup = Pickups[GD.RandRange(0, Pickups.Length - 1)];
        var pickupInstance = pickup.Instantiate<Coin>();
        pickupInstance.GlobalPosition = new Vector2(0, 0);
        pickupInstance.GlobalPosition = new Vector2(GameSettings.MaxColumns * GameSettings.Instance.RowSize,
            pickupRow * GameSettings.Instance.RowSize);
        pickupInstance.Scale = new Vector2(pickupScale, pickupScale);
        pickupInstance.OnScore += OnScoreUpdate;
        PickupsScene.CallDeferred(Node.MethodName.AddChild, pickupInstance);
        _canAddPickups = GameSettings.GetRandomNumber(1, 3);
    }

    private void OnScoreUpdate(int value)
    {
        _score += value;
        _gameUi.UpdateScoreLabel(_score);
    }
}
