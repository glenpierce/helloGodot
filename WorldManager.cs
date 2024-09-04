using Godot;
using System;

public partial class WorldManager : Node {
	private Flock flock;
	private Node2D player;
	public override void _Ready() {
		player = GetParent().GetNode<Node2D>("Player");
		flock = new Flock();
		PackedScene boidScene = GD.Load<PackedScene>("res://boid.tscn");
		Node boidGroup = new Node();
		AddChild(boidGroup);
		
		for(int i = 0; i < 10; i++) {
			Boid boidInstance = boidScene.Instantiate<Boid>();
			boidInstance.Instantiate(new Vector2((float)new Random().NextDouble() * 8, (float)new Random().NextDouble() * 6));
			flock.AddBoid(boidInstance);
			boidGroup.AddChild(boidInstance);
		}
	}

	public override void _Process(double delta) {
		flock.Update();
		flock.setTarget(player.Position);
	}
}
