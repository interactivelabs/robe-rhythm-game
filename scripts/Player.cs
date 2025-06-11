using System.Collections.Generic;
using Godot;

namespace Scripts;

public enum PlayerState
{
    Running,
    Hit,
    Dead
}

public delegate void PlayerStateChangeEventHandler(PlayerState playerState);

public partial class Player : CharacterBody2D
{
    public event PlayerStateChangeEventHandler OnPlayerStateChange;

    [Export] private AnimatedSprite2D _playerAnimations;

    private readonly Dictionary<PlayerState, double> _stateTimeouts = new()
    {
        { PlayerState.Running, 0 },
        { PlayerState.Hit, .3 },
        { PlayerState.Dead, 0 }
    };

    private PlayerState _currentState;
    private double _currentStateTimeout;

    private enum PlayerRow
    {
        Top = -1,
        Middle = 0,
        Bottom = 1
    }

    private PlayerRow _currentRow = PlayerRow.Middle;
    private PlayerRow _targetRow = PlayerRow.Middle;
    private float _rowChangeSpeed = 2.0f;

    private float _middle;
    private float _left;

    public int Health { get; private set; } = 100;

    public override void _Ready()
    {
        GD.Print("Player Started!");

        var playerScale = GameSettings.Instance.TileScaleSize * 2f;
        Scale = new Vector2(playerScale, playerScale);
        _middle = GetViewportRect().Size.Y / 2;
        _left = GameSettings.Instance.RowSize * 1;
        GlobalPosition = new Vector2(_left, _middle);
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);

        if (Input.IsActionJustPressed("ui_up") && _targetRow > PlayerRow.Top)
            _targetRow -= 1;
        else if (Input.IsActionJustPressed("ui_down") && _targetRow < PlayerRow.Bottom) _targetRow += 1;

        var targetY = (int)_targetRow * GameSettings.Instance.RowSize + _middle;
        Position = new Vector2(_left, targetY);
        Velocity = new Vector2(0, Mathf.Lerp(0, targetY, _rowChangeSpeed * (float)delta));

        MoveAndSlide();
    }

    private void StateChange(PlayerState playerState)
    {
        GD.Print("Player State Changed");
        _currentState = playerState;
        _currentStateTimeout = _stateTimeouts[_currentState];
        switch (_currentState)
        {
            case PlayerState.Hit:
                GD.Print("Player Hit");
                _playerAnimations.Play("hit");
                break;
            case PlayerState.Dead:
                GD.Print("Player Dead");
                _playerAnimations.Play("dead");
                break;
            case PlayerState.Running:
            default:
                _playerAnimations.Play("default");
                GD.Print("Player Running");
                break;
        }

        OnPlayerStateChange?.Invoke(_currentState);
    }

    public override void _Process(double delta)
    {
        switch (_currentState)
        {
            case PlayerState.Hit:
                _currentStateTimeout -= delta;
                if (_currentStateTimeout <= 0) StateChange(PlayerState.Running);
                break;
        }

        base._Process(delta);
    }

    public void DamageTaken(int value)
    {
        Health -= value;
        StateChange(Health <= 0 ? PlayerState.Dead : PlayerState.Hit);
    }
}
