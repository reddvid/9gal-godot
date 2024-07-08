using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void CollectedEventHandler(string collectableName);

    [Signal]
    public delegate void PortalEnteredEventHandler(string nextMap);

    public const float Speed = 120.0f;
    public const float JumpVelocity = -300.0f;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private AnimatedSprite2D _playerAnimSprite;
    private CollisionShape2D _playerCollision;

    public override void _Ready()
    {
        _playerAnimSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _playerCollision = GetNode<CollisionShape2D>("CollisionShape2D");
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
            // Going to the right
            _playerAnimSprite.FlipH = false;
        }
        else if (direction < Vector2.Zero)
        {
            // Going to the left
            _playerAnimSprite.FlipH = true;
        }

        // Play animations
        if (IsOnFloor())
        {
            if (_playerAnimSprite.Animation == "unalive_no_weapon") return;

            _playerAnimSprite.Play(direction == Vector2.Zero ? "idle" : "run");
            _playerAnimSprite.Position = _playerAnimSprite.FlipH switch
            {
                true when direction == Vector2.Zero =>
                    // Looking to the Left Idle
                    new Vector2(-10, 2),
                true when direction != Vector2.Zero =>
                    // Looking to the Left Running
                    new Vector2(-18, 2),
                false when direction == Vector2.Zero =>
                    // Looking to the Right Idle
                    new Vector2(-2, 2),
                false when direction != Vector2.Zero =>
                    // Looking to the Right Running
                    new Vector2(6, 2),
                _ => _playerAnimSprite.Position
            };
        }
        else
        {
            _playerAnimSprite.Play(velocity.Y < 0 ? "jump" : "fall");

            if (!_playerAnimSprite.FlipH)
            {
                _playerAnimSprite.Position = new Vector2(-4, 2);
            }
            else
            {
                _playerAnimSprite.Position = new Vector2(-10, 2);
            }
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

    public void Collect(string collectable)
    {
        GD.Print("Emitting");
        
        EmitSignal(SignalName.Collected, collectable);
    }

    public void ChangeLevel(string mapName)
    {
        GD.Print("Change Level");

        EmitSignal(SignalName.PortalEntered, "c1m2.tscn");
    }

}