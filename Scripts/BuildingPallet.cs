using Godot;
using System;
using System.Linq;

public class BuildingPallet : Control
{
    [Export]
    public PackedScene buildingButtonPrefab;

    [Export]
    public NodePath containerPath;

    [Export]
    public PackedScene[] buildings;

    [Export]
    public Texture[] buildingTextures;

    private Node container;
    private PackedScene selectedBuilding;

    public override void _Ready()
    {
        container = GetNode(containerPath);

        foreach (var (building, buildingTexture) in buildings.Zip(buildingTextures, (building, buildingTexture) => (building, buildingTexture)))
        {
            var buildingButton = (BuilingButton)buildingButtonPrefab.Instance();
            container.AddChild(buildingButton);

            var binds = new Godot.Collections.Array();
            binds.Add(building);
            buildingButton.button.Connect("pressed", this, "SelectBuilding", binds);

            buildingButton.textureRect.Texture = buildingTexture;
        }
    }

    private void SelectBuilding(PackedScene building)
    {
        GD.Print(building);
        selectedBuilding = building;
    }

    public PackedScene GetSelectedBuilding()
    {
        return selectedBuilding;
    }
}
