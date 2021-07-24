using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
    [Export]
    public float speed;

    public override void _Ready()
    {

    }

    public override void _Process(float delta)
    {
        var offset = new Vector2();

        if (Input.IsActionPressed("ui_up"))
        {
            offset.y -= delta * speed;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            offset.y += delta * speed;
        }
        if (Input.IsActionPressed("ui_left"))
        {
            offset.x -= delta * speed;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            offset.x += delta * speed;
        }

        Position = Position + offset;
    }
}
