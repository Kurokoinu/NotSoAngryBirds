using UnityEngine;
using System.Collections;

public class Bot : MonoBehaviour {
		

	public GameObject Particle_Bomb;

		void OnTriggerEnter(Collider other) {

			Instantiate(Particle_Bomb, this.transform.position, this.transform.rotation);
			Destroy(other.gameObject);
		}
		

}
