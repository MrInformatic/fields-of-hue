using Godot;
using System;

public class BuilingButton : Control
{
    [Export]
    public NodePath buttonPath;

    [Export]
    public NodePath textureRectPath;

    public Button button;
    public TextureRect textureRect;

    public override void _Ready()
    {
        button = GetNode<Button>(buttonPath);
        textureRect = GetNode<TextureRect>(textureRectPath);
    }
}
