using UnityEngine;
using System.Collections;

public class CloudSpawner : MonoBehaviour {

	public int numClouds = 40;

	public Vector3 cloudPosMin;
	public Vector3 cloudPosMax;

	public float cloudSpeed = 0.6f;

	public float cloudScaleMin = 1.0f;
	public float cloudScaleMax = 6.0f;


	//internal fields
	private GameObject[] cloudsInstances;
	public GameObject[] cloudPrefab;

	private void Awake(){


		//create an array to hold our cloud instances

		cloudsInstances = new GameObject[numClouds];



		//find that clouds anchor object

		GameObject anchor = GameObject.Find ("Cloudspawner");


		//create those clouds through array

		GameObject cloud;
		for(int i = 0; i < numClouds; i++){



			//pick cloud rnd prefab between 0-cloudPrefabs.Length-1

			int prefabNum = Random.Range(0, cloudPrefab.Length-1);

			//instantiate cloud and position it bw min and max POS

			cloud = Instantiate(cloudPrefab[prefabNum]);

			Vector3 cPos = Vector3.zero;
			cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
			cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);

			//scale cloudoo

			float scaleU = Random.value;									//rmd bw 0-1.0
			float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);

			cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);

			cPos.z = 100 - 90*scaleU;

			cloud.transform.position = cPos;
			cloud.transform.localScale = Vector3.one*scaleVal;

			//make cloud child of cloud anchor


			cloud.transform.parent = anchor.transform;

			//add the cloud to our cloud instances

			cloudsInstances[i] = cloud;

		}

	}




	void Update() {
		// Iterate all clouds
		foreach(GameObject cloud in cloudsInstances) {
			// Get cloud scale and position
			Vector3 cPos = cloud.transform.position;
			float scaleValue = cloud.transform.localScale.x;			//"localScale" from parent || "scale" relativ zur welt
			
			// faster clouds
			cPos.x -= scaleValue * Time.deltaTime * cloudSpeed;			//multipy scaleValue for different speeds for different sized clouds
			
			// reset start postion if at border
			if(cPos.x < cloudPosMin.x){
				cPos.x = cloudPosMax.x;
			}
			//new position for cloud
			cloud.transform.position = cPos;
		}

		
	}

	




}
