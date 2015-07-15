using UnityEngine;
using System.Collections;

public class BotBoss : MonoBehaviour {
		

	public GameObject Particle_Bomb;

		void Update(){

			if(Goal.goalMet == true){
				Instantiate(Particle_Bomb, this.transform.position, this.transform.rotation);
				Destroy(this.gameObject);
			}
		}

}
