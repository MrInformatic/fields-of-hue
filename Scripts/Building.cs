using Godot;
using System;
using System.Collections.Generic;

public class Building : Node2D
{
    [Export]
    public Texture patternTexture;

    public GameField gameField;
    public CellPosition cellPosition;
    public int cellWidth;
    public int cellHeight;

    public override void _Ready()
    {
        var pattern = patternTexture.GetData();

        var width = pattern.GetWidth();
        var height = pattern.GetHeight();

        var halfWidth = width / 2;
        var halfHeight = height / 2;

        pattern.Lock();
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                Color pixel = pattern.GetPixel(x, y);

                if (pixel.a > 0.0)
                {
                    var offsetCellPosition = new CellPosition(
                        cellPosition.x + x - halfWidth,
                        cellPosition.y + y - halfHeight
                    );

                    gameField.AddColor(offsetCellPosition, pixel);
                }
            }
        }
        pattern.Unlock();

        Position = new Vector2(
            cellPosition.x * cellWidth,
            cellPosition.y * cellHeight
        );

        GD.Print(Position);
    }
}
