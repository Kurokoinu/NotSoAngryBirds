using UnityEngine;
using System.Collections;
using UnityEngine.UI;

enum GameState{								//werte angeben die die variable haben kann
	idle,
	playing,
	levelend
}

public class GameController : MonoBehaviour {

	public static GameController s;

	//public inspector
	public GameObject[] castles;
	public Text guiTextLvl;
	public Text guiTextScore;

	public Vector3 castlePose;

	//private fields
	private int level;
	private int levelMax;
	private int shotsTaken;

	private GameObject castle;
	private GameState state = GameState.idle;
	private string showing = "Slingshot";



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

		shotsTaken = 0;


		// reset camera
		SwitchView("Both");
		ProjectileTail.S.Clear();

		Goal.goalMet = false;
		UpdateGUI();

		state = GameState.playing;

	}


	void UpdateGUI(){

		guiTextLvl.text = "Level:" + (level+1) + "/" + levelMax;
		guiTextScore.text = "Shots:" + shotsTaken;

	}


	void Update(){

		UpdateGUI();

		if(state == GameState.playing && Goal.goalMet) {
			if(FollowCamera.s.poi.tag == "projectile" && FollowCamera.s.poi.GetComponent<Rigidbody>().IsSleeping()) {

				state = GameState.levelend;

				SwitchView("Both");

				//next level start
				Invoke ("NextLevel", 3f);

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


	public static void ShotFired(){
		s.shotsTaken++;
	}
}
