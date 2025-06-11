using Godot;
using System;

namespace Scripts;

public partial class Conductor : AudioStreamPlayer
{
    [Export] public double Bpm = 149;
    [Export] public int Measures = 4;

    private double _songPosition;
    private int _songPositionInBeats = 1;
    private double _secPerBeat = 60;
    private double _lastReportedBeat;
    private int _beatsBeforeStart;
    private double _measure = 1;

    [Signal]
    public delegate void BeatEventHandler(float position);

    [Signal]
    public delegate void MeasureEventHandler(float position);


    public Conductor()
    {
        _secPerBeat /= Bpm;
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GD.Print("Conductor Started!");
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!Playing) return;
        _songPosition = GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
        _songPosition -= AudioServer.GetOutputLatency();
        _songPositionInBeats = (int)Math.Floor(_songPosition / _secPerBeat) + _beatsBeforeStart;
        ReportBeat();
    }

    private void ReportBeat()
    {
        if (!(_lastReportedBeat < _songPositionInBeats)) return;
        if (_measure > Measures) _measure = 1;

        EmitSignal(nameof(Beat), _songPositionInBeats);
        EmitSignal(nameof(Measure), _measure);
        _lastReportedBeat = _songPositionInBeats;
        _measure++;
    }

}
