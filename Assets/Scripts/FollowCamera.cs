using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
	
	public static FollowCamera s;				//singleton instance of this class -> access: FollowCamera.s.poi;
		
	public GameObject poi; 					    //point of interest	
	public float easing = 0.03f;
	public Vector2 minXY;

	private float camZ;


	public bool isResting = false;



	void Awake(){
		
		s = this;
		camZ = transform.position.z;
		
	}

	
	void FixedUpdate(){
		
		Vector3 destination;
		
		if(poi == null){						//check if the poi is active
			destination = Vector3.zero;			//set time destionation to the zero-vector (default point of interest(slingshot)
		}
		
		else{
			
			
			//get its position
			destination = poi.transform.position;
			
			//check if poi is a projectile
			// check if it is resting(sleeping)
			//set it to "null" as default value in next update
			
			if(poi.tag == "projectile"){
				if(poi.GetComponent<Rigidbody>().IsSleeping()){
					isResting = true;
					poi = null;
					return;
					
				}
			}
		}
		
		
		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);
		
		destination = Vector3.Lerp(transform.position, destination, easing);
		
		destination.z = camZ;
		transform.position = destination;
		
		this.GetComponent<Camera>().orthographicSize = destination.y + 10;
		
	}
	

}
