using UnityEngine;
using System.Collections;

public class BombBall : MonoBehaviour {

	public GameObject Particle_Bomb;
	
	
	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "castle" || other.tag == "floor"){
			Instantiate(Particle_Bomb, this.transform.position, this.transform.rotation);
			//Destroy(GameObject.FindGameObjectWithTag("castle"));
			if(other.tag == "castle"){
				Destroy (other.gameObject);
			}
			Destroy(this.gameObject);
		}
	}
}
