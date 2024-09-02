using Godot;
using System;

public partial class Node2DCharacterController : Node2D {
	public override void _Ready() {
	}

	public override void _Process(double delta) {
		Vector2 direction = Vector2.Zero;

		if (Input.IsActionPressed("ui_right")) {
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("ui_left")) {
			direction.X -= 1.0f;
		}
		if (Input.IsActionPressed("ui_down")) {
			direction.Y += 1.0f;
		}
		if (Input.IsActionPressed("ui_up")) {
			direction.Y -= 1.0f;
		}
		
		Position += direction;
	}
}
