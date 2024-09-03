using Godot;
using System;

public partial class NPCController : Node2D {
	private Node2D player;
	private float noticeDistance = 100.0f;
	private float happyDistance = 250.0f;
	private bool isHappy = true;
	public override void _Ready() {
		player = GetParent().GetNode<Node2D>("Player");
	}

	public override void _Process(double delta) {
		Vector2 direction = Vector2.Zero;
		if (Position.DistanceTo(player.Position) < noticeDistance) {
			isHappy = false;
		}
		if (Position.DistanceTo(player.Position) > happyDistance) {
			isHappy = true;
		}

		if(!isHappy) {
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
