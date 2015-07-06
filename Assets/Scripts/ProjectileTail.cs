using UnityEngine;
using System.Collections;
using System.Collections.Generic;					//for lists

public class ProjectileTail : MonoBehaviour {

	//singleton
	public static ProjectileTail S;
	//public inspector fields
	public float minDist = 0.1f;

	//Internal fields
	private LineRenderer line;
	private int pointCount;
	private GameObject _poi;
	private Vector3 lastPoint;

	//a property: looks to the outside like a field but calls get/set internally
	

	void Awake(){

		S = this;				//S contains the Object "ProcettaiL" in the hierarchy

		line = this.GetComponent<LineRenderer>();
		pointCount = 0;

		//----------new line material --> can be changed in the parameters----------------
		//line.material = new Material(Shader.Find("Mobile/Particles/Additive"));
	//	Color c1 = Color.yellow;
	//	Color c2 = Color.red;
	//	line.SetColors(c1,c2);
		//----------new line material----------------------------------------------------

		line.enabled = false;

	}


	void FixedUpdate(){

		//if no poi has been set, use the cameras poi (if its follows a projectile)
			//d.h. if poi == null und camera poi != null, soll der poi auf camera.s.poi
		if(poi == null){
			if(FollowCamera.s.poi != null){
				if(FollowCamera.s.poi.tag == "projectile"){
					poi = FollowCamera.s.poi;
				
				}

				else {
					return;			//no poi...
				}
			}
	
			else {
				return;				//no poi...
			}			

		}

		//now the poi defenitly has a value and its a projectile

		//add a point
		AddPoint();
		if(poi.GetComponent<Rigidbody>().IsSleeping()){
			poi = null;
		}
	}

	void AddPoint(){

		Vector3 pt = _poi.transform.position;
		//set position from line renderer

		//if the point isn't far enough from the last one, do NOTHING!
		if(pointCount > 0 && (pt -lastPoint).magnitude < minDist) {			//vectoren wie mit zahlen (-, *...)
																			//sqrt magnitute -> verbessert performance
			return;
		}

		//if we are dealing with the first launch point
		if(pointCount == 0){

			line.SetVertexCount(1);
			line.SetPosition(0, pt);
			pointCount +=1;


			line.enabled = true;

		}
		else{
			// else, not the first point
			pointCount++;
			
			line.SetVertexCount(pointCount);
			line.SetPosition(pointCount -1, pt);

		}

		lastPoint = pt;
	}



	public GameObject poi {
		get {
			return _poi;
		}
		
		set {
			//use "value" to acess the new value of the property
			//set the new value of _poi 
			_poi = value;								//poi = myGameObject -> calls the set function because you try to set poi to sth.
			
			//check if the poi was set to sth. (and now to sth. new)
			//reset EVERYTHING
			if(_poi != null){
				line.enabled = false;
				pointCount = 0;
				line.SetVertexCount(0); 			//reset the line renderer
			}
		}	
	}

	public void Clear(){

		_poi = null;
		line.enabled = false;
		pointCount = 0;
		line.SetVertexCount(0);
	}

}
