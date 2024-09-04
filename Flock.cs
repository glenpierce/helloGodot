using System.Collections.Generic;
using Godot;

public class Flock {
	private List<Boid> boids;

	public Flock() {
		boids = new List<Boid>();
	}

	public void AddBoid(Boid b) {
		boids.Add(b);
	}

	public void Update() {
		foreach (Boid boid in boids) {
			boid.Flock(boids.ToArray());
			boid.Update();
		}
	}
	
	public void setTarget(Vector2 target) {
		foreach (Boid boid in boids) {
			boid.Target = target;
		}
	}

	public List<Boid> GetBoids() {
		return boids;
	}
}
