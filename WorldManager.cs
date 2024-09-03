using Godot;
using System;

public partial class WorldManager : Node {
	public override void _Ready() {
		PackedScene npcScene = GD.Load<PackedScene>("res://npc.tscn");
		Node2D player = GetParent().GetNode<Node2D>("Player");
		for(int i = 0; i < 10; i++) {
			for(int j = 0; j < 10; j++) {
				Node2D npcInstance = npcScene.Instantiate<Node2D>();
				npcInstance.Position = new Vector2(i * 100, j * 100);
				NPCController npcController = npcInstance as NPCController;
				if (npcController != null) {
					npcController.SetPlayer(player);
				}
				AddChild(npcInstance);
			}
		}
	}

	public override void _Process(double delta) {
	}
}
