using Godot;
using System;

public partial class Conductor : AudioStreamPlayer
{

	[Export] private double _bpm = 152;
	[Export] private int _measures = 4;

	[Export] private Timer _startTimer;

	private double _songPosition = 0.0;
	private int _songPositionInBeats = 1;
	private double _secPerBeat = 60;
	private double _lastReportedBeat = 0;
	private int _beatsBeforeStart = 0;
	private double _measure = 1;

	private int _closest = 0;
	private double _timeOffBeat = 0;

	[Signal]
	public delegate void BeatEventHandler(float position);

	[Signal]
	public delegate void MeasureEventHandler(float position);


	public Conductor()
	{
		_secPerBeat /= _bpm;
	}


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_secPerBeat = 60 / _bpm;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (Playing)
		{
			_songPosition = GetPlaybackPosition() + AudioServer.GetTimeSinceLastMix();
			_songPosition -= AudioServer.GetOutputLatency();
			_songPositionInBeats = (int)Math.Floor(_songPosition / _secPerBeat) + _beatsBeforeStart;
			ReportBeat();
		}
	}

	private void ReportBeat()
	{
		if (_lastReportedBeat < _songPositionInBeats)
		{
			if (_measure > _measures)
			{
				_measure = 1;
			}

			EmitSignal(nameof(Beat), _songPositionInBeats);
			EmitSignal(nameof(Measure), _measure);
			_lastReportedBeat = _songPositionInBeats;
			_measure++;
		}
	}

	public void _on_start_timer_timeout()
	{
		_songPositionInBeats += 1;
		if (_songPositionInBeats < _beatsBeforeStart - 1)
		{
			_startTimer.Start();
		}
		else if (_songPositionInBeats == _beatsBeforeStart - 1)
		{
			_startTimer.WaitTime -= AudioServer.GetTimeToNextMix() + AudioServer.GetOutputLatency();
			_startTimer.Start();
		}
		else
		{
			Play();
			_startTimer.Stop();
		}

		ReportBeat();
	}
}
