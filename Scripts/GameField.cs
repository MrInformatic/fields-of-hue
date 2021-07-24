using Godot;
using System;
using System.Collections.Generic;

public class GameField : Node2D
{
    [Export]
    public NodePath buildingPalletPath;

    [Export]
    public int cellWidth;

    [Export]
    public int cellHeight;

    [Export]
    public PackedScene cellSpritePrefab;

    public BuildingPallet buildingPallet;

    private Dictionary<CellPosition, Cell> cells;
    private Dictionary<CellPosition, Building> buildings;

    public Cell GetCell(CellPosition cellPosition)
    {
        return cells[cellPosition];
    }

    public void AddColor(CellPosition cellPosition, Color color)
    {
        if (cells.ContainsKey(cellPosition))
        {
            cells[cellPosition].addColor(color);
        }
        else
        {
            var sprite = (Sprite)cellSpritePrefab.Instance();
            AddChild(sprite);

            Cell cell = new Cell(color, sprite, cellPosition, cellWidth, cellHeight);
        }
    }

    public void BuildBuilding(Building building)
    {

    }

    public override void _Ready()
    {
        cells = new Dictionary<CellPosition, Cell>();
        buildings = new Dictionary<CellPosition, Building>();
        buildingPallet = GetNode<BuildingPallet>(buildingPalletPath);
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
                    var cellPosition = new CellPosition(
                        (int)Math.Round(inputEventMouseButton.GlobalPosition.x / (float)cellWidth),
                        (int)Math.Round(inputEventMouseButton.GlobalPosition.y / (float)cellHeight)
                    );

                    var building = (Building)buildingPallet.GetSelectedBuilding().Instance();
                    building.gameField = this;
                    building.cellPosition = cellPosition;
                    building.cellWidth = cellWidth;
                    building.cellHeight = cellHeight;
                    AddChild(building);
                }
            }

        }

        base._UnhandledInput(inputEvent);
    }
}
