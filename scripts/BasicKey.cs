using Godot;
using System;

public partial class BasicKey : Area2D
{
    public override void _Ready()
    {
        
    }

    private void BodyEntered(Node2D body)
    {
        QueueFree();
        
        GD.Print("You Got a Basic Key");

        body.GetNode<Label>("Label").Text = "You got a basic key!";
        body.GetNode<Label>("Label").Show();
    }
}
