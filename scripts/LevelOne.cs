using Godot;
using System;
using Range = Godot.Range;

public partial class LevelOne : Node2D
{
    [Export] private Player _player;
    [Export] private UI _ui;

    private AnimationPlayer _plat1;
    private AnimationPlayer _plat2;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _plat1 = GetNode<AnimationPlayer>("SinglePlatform/AnimationPlayer");
        _plat2 = GetNode<AnimationPlayer>("SinglePlatform2/AnimationPlayer");

        var rng = new RandomNumberGenerator();
        rng.Randomize();

        _plat2.Seek(rng.RandiRange(0, 4), true);
        _plat1.Seek(rng.RandiRange(0, 8), true);

        _player.Collected += Collect;
        _player.PortalEntered += ChangeLevel;
    }

    private void ChangeLevel(string nextmap)
    {
        
    }

    private void Collect(string collectableName)
    {
        if (!string.IsNullOrEmpty(collectableName))
            _ui.OnCollected(collectableName);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}