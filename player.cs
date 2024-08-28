using Godot;
using System;

public partial class player : CharacterBody3D {
	public override void _Ready() {
	}
	
	public override void _PhysicsProcess(double delta) {
		Vector3 direction = Vector3.Zero;

		if (Input.IsActionPressed("ui_right")) {
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("ui_left")) {
			direction.X -= 1.0f;
		}
		if (Input.IsActionPressed("ui_down")) {
			direction.Z += 1.0f;
		}
		if (Input.IsActionPressed("ui_up")) {
			direction.Z -= 1.0f;
		}
		
		Velocity = direction;
		if(MoveAndSlide()) {
			for(int i = 0; i < this.GetSlideCollisionCount(); i++) {
				KinematicCollision3D kinematicCollision3D = this.GetSlideCollision(i);
				if (kinematicCollision3D.GetCollider().IsClass("RigidBody3D")) {
					RigidBody3D rigidBody3D = (RigidBody3D)kinematicCollision3D.GetCollider();
					rigidBody3D.ApplyCentralImpulse(this.Velocity.Normalized());					
				}
			}
		}
	}

	public override void _Process(double delta) {
	}
}
