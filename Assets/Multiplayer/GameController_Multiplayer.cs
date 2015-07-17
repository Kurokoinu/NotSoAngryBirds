using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum GameState2{								//werte angeben die die variable haben kann
	idle,
	playing,
	levelend
}

public class GameController_Multiplayer : MonoBehaviour {

	public static GameController_Multiplayer s;

	//public inspector
	public GameObject[] castles;

	public Vector3 castlePose;

	//private fields
	private int level;
	private int levelMax;

	private GameObject castle;
	private GameState2 state = GameState2.idle;
	private string showing = "Both";

	private int maxShots;

	public GameObject sling;
	public SlingshotP1 slingscript;

	public GameObject FollowCam;
	public FollowCamera follocam;


	void Start() {


		s = this;

		//initiate stuff (lvl max,lvl...)
		level = 0;
		levelMax = castles.Length;

		StartLevel();
	}


	void StartLevel(){

		//clean up: if castle->destroy
		if(castle != null) {

			Destroy (castle);

		}

		//destroy projectiles
		GameObject[] projectiles = GameObject.FindGameObjectsWithTag("projectile");
		foreach(GameObject p in projectiles){
			Destroy(p);
		}

		//instantiate new castle and reset shots taken
		castle = Instantiate (castles[level]) as GameObject;
		castle.transform.position = castlePose;


		// reset camera
		SwitchView("Both");
		ProjectileTail.S.Clear();

		Goal.goalMet = false;


		state = GameState2.playing;

	}




	void Update(){



		if(state == GameState2.playing && Goal.goalMet) {
			if(FollowCamera.s.poi.tag == "projectile") {

				state = GameState2.levelend;

				SwitchView("Both");

				//next level start
				Invoke ("NextLevel", 2f);

			}
		}
	}


	void NextLevel() {

		level++;
		if(level == levelMax){					//when max level is done, reset lvl to 0
			level = 0;
		}

		StartLevel();
	}


	public void SwitchView(string view){

		//switch over all possible view "both"(nichts), "castle"(s.castle), "Slingshot (0-wert)

		s.showing = view;
		switch(s.showing){
		case "Slingshot":
			FollowCamera.s.poi = null;
			break;

		case "Castle":
			FollowCamera.s.poi = s.castle;
			break;

		case "Both":
			FollowCamera.s.poi = GameObject.Find("ViewBoth");
			break;
		}

	}


}
