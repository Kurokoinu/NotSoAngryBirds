using UnityEngine;
using System.Collections;

public class BombDestroy : MonoBehaviour {

	public GameObject Particle_Bomb;
	
	
	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "bomb"){
			Instantiate(Particle_Bomb, this.transform.position, this.transform.rotation);
			Destroy(this.gameObject);
		}
	}
}
