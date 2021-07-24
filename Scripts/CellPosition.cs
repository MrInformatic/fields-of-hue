using Godot;

public class CellPosition
{
	public int x;
	public int y;

	public CellPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public Vector2 getWorldPosition(int x, int y)
	{
		return new Vector2(x, y);
	}

	public override int GetHashCode()
	{
		return (x, y).GetHashCode();
	}

	public override bool Equals(object obj)
	{
		if (obj is CellPosition)
		{
			CellPosition cellPosition = (CellPosition)obj;
			return x == cellPosition.x && y == cellPosition.y;
		}
		else
		{
			return false;
		}
	}
}
