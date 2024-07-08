using Godot;
using System;

public partial class UI : CanvasLayer
{
    private Label _promptLabel;

    private string _prompt;

    private string Prompt
    {
        get => _prompt;
        set
        {
            _prompt = value;
            UpdatePromptText();
        }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _promptLabel = GetNode<Label>("Control/MarginContainer/VBoxContainer/HBoxContainer/Prompt");

        UpdatePromptText();
    }

    private void UpdatePromptText()
    {
        GD.Print($"Updating: {_prompt}");

        _promptLabel.Text = _prompt;
    }

    public void OnCollected(string collectable)
    {
        GD.Print("Collected");

        _prompt = $"You picked up a {collectable}!";
        UpdatePromptText();
    }

 

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}