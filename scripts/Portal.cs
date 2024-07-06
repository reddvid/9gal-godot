using Godot;
using System;

public partial class Portal : Area2D
{
    private Label _labelLevelComplete;
    private Node2D _player;
    private Timer _timer;

    public override void _Ready()
    {
        _labelLevelComplete = GetNode<Label>("Label");
        _timer = GetNode<Timer>("Timer");
        
        _labelLevelComplete.Visible = false;
        _labelLevelComplete.Hide();
    }
    
    private void OnBodyEntered(Node2D body)
    {
        GD.Print("Level Complete");
        
        body.Hide();

        body.SetProcessInput(false);
        body.SetPhysicsProcess(false);

        _labelLevelComplete.Show();
        
        _timer.Start();
    }

    private void OnTimerTimeout()
    {
        var levelName = GetTree().CurrentScene.Name;
        GetTree().ChangeSceneToFile("res://scenes/level2.tscn");
    }
}
