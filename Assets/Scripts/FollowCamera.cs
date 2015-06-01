using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {

	private float camZ;

	public static FollowCamera s;				//singleton instance of this class -> access: FollowCamera.s.poi;
	public GameObject poi; 					    //point of interest
	public float easing;
	
	Vector2 minXY;


	void Awake(){

		s = this;
		camZ = transform.position.z;

	}

	void Update(){

		if(poi == null){						//check if the poi is active
			return;


		}

		Vector3 destination = Vector3.Lerp(transform.position, poi.transform.position, easing);
		//destination = Vector3.Lerp(transform.position, poi.transform.position;, easing); //5% in direction of destination

		destination.x = Mathf.Max(minXY.x, destination.x);
		destination.y = Mathf.Max(minXY.y, destination.y);

		destination.z = camZ;
		transform.position = destination;

		this.GetComponent<Camera>().orthographicSize = destination.y + 10;


	}

}