 using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {


	//Inspector fields
	public GameObject prefabProjectile;
	public float velocityMult = 0.03f;


	//Internal fields
	private GameObject launchPoint;
	private bool aimingMode;

	private Vector3 launchPos;
	private GameObject projectile;


	void Awake() {
		Transform launchPointTrans = transform.FindChild ("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;
	}


	void OnMouseEnter() {
		print ("Yo!");
		launchPoint.SetActive(true);
	}


	void OnMouseExit() {
		print ("no!");
		if(aimingMode != true){
			launchPoint.SetActive(false);}


	}


	void OnMouseDown(){
		//set aim to aiminng modeee!
		aimingMode = true;

		//instantiate project tile
		projectile = Instantiate(prefabProjectile) as GameObject;

		//position project tile at launchpoint
		projectile.transform.position = launchPos;

		//disable physics
		projectile.GetComponent<Rigidbody>().isKinematic = true;
	}


	void Update(){
		//check aiming mode
		if (!aimingMode) return;

		//mouse pos 3D space
		Vector3 mousePos2D = Input.mousePosition;
		mousePos2D.z = -Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

		//calculate delta btw launchPos and mouse pos
		Vector3 mouseDelta = mousePos3D - launchPos;

		//constrain the delta to radius of the sphere collider
		float maxMagnitude = this.GetComponent<SphereCollider>().radius;
		mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxMagnitude);

		//set projectile to new pos
		Vector3 projPos = launchPos + mouseDelta;
		projectile.transform.position = projPos;

		//check mouse released
		if (Input.GetMouseButtonUp(0)){
			aimingMode = false;
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;
			FollowCamera.s.poi = projectile;

			projectile = null;

			//fire it OFF
			GameController.ShotFired();

		}

	}
}
