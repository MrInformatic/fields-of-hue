using Godot;
using System;
using System.Collections.Generic;

public class GameField : Node2D
{
    public CellPosition[] neighbours = new CellPosition[] {
        new CellPosition(1, 0),
        new CellPosition(1, 1),
        new CellPosition(0, 1),
        new CellPosition(-1, 1),
    };

    [Export]
    public NodePath buildingPalletPath;

    [Export]
    public int cellWidth;

    [Export]
    public int cellHeight;

    [Export]
    public PackedScene cellSpritePrefab;

    [Export]
    public double hueScoreSlopeMod;

    [Export]
    public double hueScoreXMod;

    [Export]
    public double hueScoreYMod;

    [Export]
    public double satScoreSlopeMod;

    [Export]
    public double satScoreXMod;

    [Export]
    public double satScoreYMod;

    [Export]
    public double brightScoreSlopeMod;

    [Export]
    public double brightScoreXMod;

    [Export]
    public double brightScoreYMod;

    [Export]
    public NodePath ScoreLabelPath;

    public Label scoreLabel;

    public BuildingPallet buildingPallet;

    private Dictionary<CellPosition, Cell> cells;
    private Dictionary<CellPosition, Building> buildings;

    public Cell GetCell(CellPosition cellPosition)
    {
        return cells[cellPosition];
    }

    public void AddColor(CellPosition cellPosition, Color color)
    {
        GD.Print(cells.ContainsKey(cellPosition));
        if (cells.ContainsKey(cellPosition))
        {
            cells[cellPosition].addColor(color);
        }
        else
        {
            var sprite = (Sprite)cellSpritePrefab.Instance();
            AddChild(sprite);

            Cell cell = new Cell(color, sprite, cellPosition, cellWidth, cellHeight);

            cells.Add(cellPosition, cell);
        }
    }

    public double calculateScore()
    {
        var score = 0.0;

        foreach (var entry in cells)
        {
            var cellPosition = entry.Key;
            var cell = entry.Value;
            var cellColor = cell.GetColor();

            foreach (var neighbour in neighbours)
            {
                var otherCellPosition = new CellPosition(
                    cellPosition.x + neighbour.x,
                    cellPosition.y + neighbour.y
                );

                if (cells.ContainsKey(otherCellPosition))
                {
                    var otherCell = cells[otherCellPosition];
                    var otherCellColor = otherCell.GetColor();

                    score += (-hueScoreSlopeMod * Math.Pow((cellColor.h * 360.0 - otherCellColor.h * 360.0) - hueScoreXMod, 2.0)) / hueScoreXMod + hueScoreYMod;
                    score += satScoreSlopeMod * Math.Pow((cellColor.s - otherCellColor.s) - satScoreXMod, 2.0) + satScoreYMod;
                    score += brightScoreSlopeMod * Math.Pow((cellColor.v - otherCellColor.v) - brightScoreXMod, 2.0) + brightScoreYMod;
                }
            }
        }

        return score;
    }

    public override void _Ready()
    {
        cells = new Dictionary<CellPosition, Cell>();
        buildings = new Dictionary<CellPosition, Building>();
        buildingPallet = GetNode<BuildingPallet>(buildingPalletPath);
        scoreLabel = GetNode<Label>(ScoreLabelPath);
        updateScore();
    }

    public override void _UnhandledInput(InputEvent inputEvent)
    {
        if (inputEvent is InputEventMouseButton)
        {
            var inputEventMouseButton = (InputEventMouseButton)inputEvent;
            if ((ButtonList)inputEventMouseButton.ButtonIndex == ButtonList.Left && inputEventMouseButton.Pressed)
            {
                if (buildingPallet.GetSelectedBuilding() != null)
                {
                    Vector2 position = GetGlobalMousePosition();

                    var cellPosition = new CellPosition(
                        (int)Math.Round(position.x / (float)cellWidth),
                        (int)Math.Round(position.y / (float)cellHeight)
                    );

                    if (!buildings.ContainsKey(cellPosition))
                    {
                        var building = (Building)buildingPallet.GetSelectedBuilding().Instance();
                        building.gameField = this;
                        building.cellPosition = cellPosition;
                        building.cellWidth = cellWidth;
                        building.cellHeight = cellHeight;
                        AddChild(building);

                        buildings.Add(cellPosition, building);

                        updateScore();
                    }
                }
            }

        }

        base._UnhandledInput(inputEvent);
    }

    public void updateScore()
    {
        scoreLabel.Text = calculateScore().ToString();
    }
}
