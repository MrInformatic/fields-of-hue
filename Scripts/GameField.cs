using Godot;
using System;
using System.Collections.Generic;

public class GameField : Node2D
{
	private PackedScene cellSpritePrefab;
	private Dictionary<CellPosition, Cell> cells;

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

			Cell cell = new Cell(color, sprite, cellPosition);
		}
	}

	public void BuildBuilding(Building building)
	{

	}
}
