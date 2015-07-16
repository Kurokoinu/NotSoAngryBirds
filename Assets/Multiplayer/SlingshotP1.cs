using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SlingshotP1 : MonoBehaviour {

	
	//Inspector fields
	public GameObject[] prefabProjectile;
	public float velocityMult = 0.03f;

	public bool gameover = false;
	
	
	//Internal fields
	private GameObject launchPoint;
	private bool aimingMode;
	
	private Vector3 launchPos;
	private GameObject projectile;

	//boolean for different balls
	public bool ball01 = true;
	public bool ball02 = false;
	public bool ball03 = false;

	public bool player1 = true;
	public SlingshotP2 slingscript;

	public Text turn;

	void UpdateGUI(){
		if(player1 == true){
			turn.text = "Player 1's turn!";
		}

		if(slingscript.player2 == true){
			turn.text = "Player 2's turn!";
		}
	}
	
	
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
		if(player1 == true){

		aimingMode = true;

		if(gameover == false){
		if(ball01 == true){
			
			//instantiate project tile
			projectile = Instantiate(prefabProjectile[0]) as GameObject;

		}

		if(ball02 == true){
			
			//instantiate project tile
			projectile = Instantiate(prefabProjectile[1]) as GameObject;
			Debug.Log("ball2" + ball02);
			
		}

		if(ball03 == true){
			
			//instantiate project tile
			projectile = Instantiate(prefabProjectile[2]) as GameObject;
			
		}
			
			//position project tile at launchpoint
			projectile.transform.position = launchPos;
			
			//disable physics
			projectile.GetComponent<Rigidbody>().isKinematic = true;
		}
			player1 = false;
			slingscript.player2 = true;


	}
	}
	
	void Update(){

		UpdateGUI();

		Debug.Log(gameover + "gameover");
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

		if(gameover == false){
		//set projectile to new pos
		projectile.transform.position = launchPos + mouseDelta;
		
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

	public void ToogleBalls01(){

		ball01 = true;
		ball02 = false;
		ball03 = false;
	}

	
	public void ToogleBalls02(){
		
		ball01 = false;
		ball02 = true;
		ball03 = false;
	}

	public void ToogleBalls03(){
		
		ball01 = false;
		ball02 = false;
		ball03 = true;
	}
}
