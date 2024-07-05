using Godot;
using System;

public partial class Keelzone : Area2D
{
    private Timer _timer;
    
    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
    }

    private void OnBodyEntered(Node2D body)
    {
        GD.Print("Dieaded");
        
        _timer.Start();
    }

    private void OnTimerTimeout()
    {
        GetTree().ReloadCurrentScene();
    }
}
