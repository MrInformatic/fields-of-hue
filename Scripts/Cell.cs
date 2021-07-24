using Godot;
using System;
using System.Collections.Generic;

public class Cell
{
    private List<Color> colors;
    private Sprite sprite;
    private CellPosition position;

    public Cell(Color color, Sprite sprite, CellPosition position)
    {
        this.colors = new List<Color>();
        this.sprite = sprite;
        this.position = position;

        updateColor();
        updatePosition();
    }

    public void addColor(Color color)
    {
        colors.Add(color);

        updateColor();
    }

    public Color GetColor()
    {
        var h = 0.0f;
        var s = 0.0f;
        var v = 0.0f;

        foreach (var color in colors)
        {
            h += color.h;
            s += color.s;
            v += color.v;
        }

        h /= colors.Count;

        return Color.FromHsv(h, s, v);
    }

    public void updateColor()
    {
        this.sprite.Modulate = GetColor();
    }

    public void updatePosition()
    {
        this.sprite.Position = new Vector2(position.x, position.y);
    }
}