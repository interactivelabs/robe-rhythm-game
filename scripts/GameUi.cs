using Godot;

namespace Scripts;

public partial class GameUi : Control
{
    [Export] public Label ScoreValueLabel;
    [Export] public Label HealthValueLabel;

    public void UpdateScoreLabel(int value)
    {
        ScoreValueLabel.Text = value.ToString();
    }

    public void UpdateHealthLabel(int value)
    {
        HealthValueLabel.Text = value.ToString();
    }
}