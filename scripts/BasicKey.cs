using Godot;
using System;

public partial class BasicKey : Area2D
{
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    private void BodyEntered(Node2D body)
    {
        if (body is Player)
        {
            GD.Print("You Got a Basic Key");

            _animationPlayer.Play("pickup");

            (body as Player).Collect("Basic Key");
        }
    }
}