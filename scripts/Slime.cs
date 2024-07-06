using Godot;
using System;

public partial class Slime : Node2D
{
	// Called when the node enters the scene tree for the first time.

	private const float Speed = 60.0f;
	private double _direction = 1;
	private RayCast2D _rayCast2DRight;
	private RayCast2D _rayCast2DLeft;
	private AnimatedSprite2D _animatedSprite2D;
	
	public override void _Ready()
	{
		_rayCast2DLeft = GetNode<RayCast2D>("RayCast2D_Left");
		_rayCast2DRight = GetNode<RayCast2D>("RayCast2D_Right");
		_animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (_rayCast2DRight.IsColliding())
		{
			_direction = -1;
			_animatedSprite2D.FlipH = false;
		}

		if (_rayCast2DLeft.IsColliding())
		{
			_direction = 1;
			_animatedSprite2D.FlipH = true;
		}

		var newPosition = Position;
		newPosition.X += (float)(_direction * Speed * delta);
		Position = newPosition;
	}
}
