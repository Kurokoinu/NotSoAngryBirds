using UnityEngine;
using System.Collections;

public class MetalCollide : MonoBehaviour {

		public AudioClip impact;
		AudioSource audio;
		
		void Start() {
			audio = GetComponent<AudioSource>();
		}
		
	void OnCollisionEnter() {
	//	if(other.gameObject.CompareTag("projectile")){
			audio.PlayOneShot(impact, 0.7F);
	//	}
		}
	}

