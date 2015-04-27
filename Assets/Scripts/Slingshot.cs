 using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	public GameObject launchPoint;

	void Awake() {
		Transform LaunchPointTrans = transform.Find ("LaunchPoint");
		launchPoint = LaunchPointTrans.gameObject;
		launchPoint.SetActive(false);

	}

	void OnMouseEnter() {
		print ("Yo!");
		launchPoint.SetActive(true);
	}

	void OnMouseExit() {
		print ("no!");
		launchPoint.SetActive(false);

	}
}
