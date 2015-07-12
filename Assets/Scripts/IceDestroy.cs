using UnityEngine;
using System.Collections;

public class IceDestroy : MonoBehaviour {

	public GameObject Particle_Ice;

	
	void OnTriggerEnter(Collider other) {

		Instantiate(Particle_Ice, this.transform.position, this.transform.rotation);		
		Destroy(this.gameObject);
		
	}
}
