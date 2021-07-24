using Godot;
using System;
using System.Collections.Generic;

public class Building : Node2D
{
    [Export]
    public Image pattern;

    public GameField gameField;
    public CellPosition cellPosition;

    public override void _Ready()
    {
        var width = pattern.GetWidth();
        var height = pattern.GetHeight();

        var halfWidth = width / 2;
        var halfHeight = height / 2;

        for (var x = 0; x <= width; x++)
        {
            for (var y = 0; y <= height; y++)
            {
                var offsetCellPosition = new CellPosition(
                    cellPosition.x + x - halfWidth,
                    cellPosition.y + y - halfHeight
                );

                gameField.AddColor(offsetCellPosition, pattern.GetPixel(x, y));
            }
        }
    }
}