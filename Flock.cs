using System.Collections.Generic;

public class Flock {
	private List<Boid> boids;

	public Flock() {
		boids = new List<Boid>();
	}

	public void AddBoid(Boid b) {
		boids.Add(b);
	}

	public void Update() {
		foreach (Boid b in boids) {
			b.Flock(boids.ToArray());
			b.Update();
		}
	}

	public List<Boid> GetBoids() {
		return boids;
	}
}
