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
                    }
                }
            }

        }

        base._UnhandledInput(inputEvent);
    }
}
