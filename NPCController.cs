using Godot;
using System;

public partial class NPCController : Node2D {
	private Node2D player;
	public override void _Ready() {
		player = GetParent().GetNode<Node2D>("Player");
	}

	public override void _Process(double delta) {
		Vector2 direction = Vector2.Zero;
		if(Position.DistanceTo(player.Position) < 100) {
			RunFromPlayer(player, ref direction);
		}
	}
	
	private void RunFromPlayer(Node2D player, ref Vector2 direction) {
		if (player.Position.X > Position.X) {
			direction.X -= 1.0f;
		}
		if (player.Position.X < Position.X) {
			direction.X += 1.0f;
		}
		if (player.Position.Y > Position.Y) {
			direction.Y -= 1.0f;
		}
		if (player.Position.Y < Position.Y) {
			direction.Y += 1.0f;
		}
		Position += direction;
	}
}
