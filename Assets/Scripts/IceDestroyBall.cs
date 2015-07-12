using UnityEngine;
using System.Collections;

public class IceDestroyBall : MonoBehaviour {

	public GameObject Particle_Ice;

	
	void OnTriggerEnter(Collider other) {
		
		if(other.tag == "floor" || other.tag == "castle"){
			Instantiate(Particle_Ice, this.transform.position, this.transform.rotation);		
			Destroy(this.gameObject);
		}
	}
}
