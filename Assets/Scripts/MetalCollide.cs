using UnityEngine;
using System.Collections;

public class MetalCollide : MonoBehaviour {

	public GameObject Particle_colider;

	void OnCollisionEnter(Collider other) {
		
		Instantiate(Particle_colider, this.transform.position, this.transform.rotation);		

	}
}

