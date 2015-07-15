using UnityEngine;
using System.Collections;

public class GoalBoss3 : MonoBehaviour {
	

//static field accesable from anywhere

	//storing if the goal was met

	public static bool goalMet = false;

	void OnTriggerEnter(Collider other){
		//check if object is projectile

		if (other.gameObject.CompareTag("projectile")) {

			//set static field to true
			goalMet = true;

			//set the alpha of the color to a higher opacity
			Color c = this.gameObject.GetComponent<Renderer>().material.color;

			c.a = 1.0f;

			this.gameObject.GetComponent<Renderer>().material.color = c;

			Application.LoadLevel("Win");


		}
	}
}
