using System;
using Godot;

public partial class Boid : Node2D {
	public Vector2 Velocity { get; private set; }
	public Vector2 Acceleration { get; private set; }
	private float maxForce = 0.05f;
	private Vector2 maxForceVector;
	private float maxSpeed = 2.0f;
	private Vector2 maxSpeedVector;
	
	public void Instantiate(Vector2 position) {
		Position = position;
		Velocity = new Vector2((float)(new Random().NextDouble() * 2 - 1), (float)(new Random().NextDouble() * 2 - 1));
		Acceleration = Vector2.Zero;
		maxForceVector = new Vector2(maxForce, maxForce);
		maxSpeedVector = new Vector2(maxSpeed, maxSpeed);
	}

	public void ApplyForce(Vector2 force) {
		Acceleration += force;
	}

	public void Update() {
		Velocity += Acceleration;
		Velocity = Velocity.Clamp(-maxSpeedVector, maxSpeedVector);
		Position += Velocity;
		Acceleration = Vector2.Zero;
	}

	public void Flock(Boid[] boids) {
		Vector2 separation = Separate(boids);
		Vector2 alignment = Align(boids);
		Vector2 cohesion = Cohere(boids);

		ApplyForce(separation);
		ApplyForce(alignment);
		ApplyForce(cohesion);
	}

	private Vector2 Separate(Boid[] boids) {
		float desiredSeparation = 25.0f;
		Vector2 steer = Vector2.Zero;
		int count = 0;

		foreach (Boid other in boids) {
			float distance = Position.DistanceTo(other.Position);
			if (distance > 0 && distance < desiredSeparation) {
				Vector2 diff = Position - other.Position;
				diff = diff.Normalized();
				diff /= distance;
				steer += diff;
				count++;
			}
		}

		if (count > 0) {
			steer /= count;
		}

		if (steer.Length() > 0) {
			steer = steer.Normalized() * maxSpeed - Velocity;
			steer = steer.Clamp(-maxForceVector, maxForceVector);
		}

		return steer;
	}

	private Vector2 Align(Boid[] boids) {
		float neighborDist = 50.0f;
		Vector2 sum = Vector2.Zero;
		int count = 0;

		foreach (Boid other in boids) {
			float distance = Position.DistanceTo(other.Position);
			if (distance > 0 && distance < neighborDist) {
				sum += other.Velocity;
				count++;
			}
		}

		if (count > 0) {
			sum /= count;
			sum = sum.Normalized() * maxSpeed;
			Vector2 steer = sum - Velocity;
			steer = steer.Clamp(-maxForceVector, maxForceVector);
			return steer;
		}

		return Vector2.Zero;
	}

	private Vector2 Cohere(Boid[] boids) {
		float neighborDist = 50.0f;
		Vector2 sum = Vector2.Zero;
		int count = 0;

		foreach (Boid other in boids) {
			float distance = Position.DistanceTo(other.Position);
			if (distance > 0 && distance < neighborDist) {
				sum += other.Position;
				count++;
			}
		}

		if (count > 0) {
			sum /= count;
			return Seek(sum);
		}

		return Vector2.Zero;
	}

	private Vector2 Seek(Vector2 target) {
		Vector2 desired = target - Position;
		desired = desired.Normalized() * maxSpeed;
		Vector2 steer = desired - Velocity;
		steer = steer.Clamp(-maxForceVector, maxForceVector);
		return steer;
	}
}
