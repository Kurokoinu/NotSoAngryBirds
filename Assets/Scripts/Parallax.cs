using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;
	private float[] parallaxScales;
	public float smoothing;

	private Transform cam;
	private Vector3 previousCamPos;


	void Start () {
	
		cam = Camera.main.transform;
		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for(int i = 0; i < backgrounds.Length; i++){

			parallaxScales[i] = backgrounds[i].position.z * -1;				//fill array with bg
		}
	}
	

	void LateUpdate () {													//parallax update should happen after camera
	

		for(int i = 0; i < backgrounds.Length; i++){

			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];	//amount of movement

			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
			//smooth movement
			backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
			
		}

		previousCamPos = cam.position;
	}
}
