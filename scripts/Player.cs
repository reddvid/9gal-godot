using Godot;
using System;

public partial class Player : CharacterBody2D
{
    public const float Speed = 120.0f;
    public const float JumpVelocity = -300.0f;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private AnimatedSprite2D _playerAnimSprite;

    public override void _Ready()
    {
        _playerAnimSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
            velocity.Y = JumpVelocity;

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("move_left", "move_right", "ui_up", "ui_down");

        // Direct the player
        if (direction > Vector2.Zero)
        {
            _playerAnimSprite.FlipH = false;
        }
        else if (direction < Vector2.Zero)
        {
            _playerAnimSprite.FlipH = true;
        }

        // Play animations
        if (IsOnFloor())
        {
            _playerAnimSprite.Play(direction == Vector2.Zero ? "idle" : "run");
        }
        else
        {
            _playerAnimSprite.Play(velocity.Y < 0 ? "jump" : "fall");
        }

        // Apply movement
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}