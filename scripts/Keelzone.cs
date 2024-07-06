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

        var animatedSprite = body.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        animatedSprite.Play("unalive_no_weapon");
        
        Engine.TimeScale = 0.5f;
        _timer.Start();
    }

    private void OnTimerTimeout()
    {
        Engine.TimeScale = 1.0f;
        GetTree().ReloadCurrentScene();
    }
}
