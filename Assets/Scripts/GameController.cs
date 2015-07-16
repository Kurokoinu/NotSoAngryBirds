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
	public Text NPC;

	public Vector3 castlePose;

	//private fields
	private int level;
	private int levelMax;
	private int shotsTaken;

	private GameObject castle;
	private GameState state = GameState.idle;
	private string showing = "Slingshot";

	private int maxShots;

	public GameObject sling;
	public Slingshot slingscript;

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
		guiTextScore.text = "Shots:" + shotsTaken + "/" + maxShots;


		if(Application.loadedLevelName == "Stage01")
		{		
			if (level == 0)
			{
				if(shotsTaken == 1)
				{
					NPC.text = "Good.";
				}

			}

			if (level == 1)
			{
				if(shotsTaken == 0)
				{
					NPC.text = "These things are bouncy.";
				}

				if(shotsTaken == 1)
				{
					NPC.text = "Great. (눈_눈)";
				}
				
			}

			if (level == 2)
			{
				if(shotsTaken == 0)
				{
					NPC.text = "You see the metal things? Metal = not moving, got it?.";
				}
				
			}
		}

		if (Application.loadedLevelName == "Stage02"){

			if(level == 0){
				if(shotsTaken == 1)
				{
					NPC.text = "Be careful, the floor is slippery. Maybe.";
				}
			}

			if(level == 1){
				if(shotsTaken == 2)
				{
					NPC.text = "...I made this one on my first try- I mean...don't give up.";
				}

				if(shotsTaken == 4)
				{
					NPC.text = "...";
				}

				if(shotsTaken == 9)
				{
					NPC.text = "...last chance";
				}
			}

			if(level == 2){
				if(shotsTaken == 0)
				{
					NPC.text = "This should be pretty easy. Anyways, next Stage is last Stage.";
				}

			}
		}

		
		if (Application.loadedLevelName == "Stage03"){
			
			if(level == 0){
				if(shotsTaken == 2)
				{
					NPC.text = "Be sure not to use the bomb to hit the TV. They won't do anything, not even to the big on";
				}
			}

			if(level == 1){
				if(shotsTaken == 0)
				{
					NPC.text = "More.................TVs.";
				}

				if(shotsTaken == 2)
				{
					NPC.text = "Have you heard about the midnight channel?";
				}

				if(shotsTaken == 3)
				{
					NPC.text = "I guess not.";
				}

				
				if(shotsTaken == 4)
				{
					NPC.text = "This ones difficult, but not impossible.";
				}
			}

			if(level == 2){
				if(shotsTaken == 0)
				{
					NPC.text = "That's the main TV! Destory it.";
				}
			}

		}
	}


	void Update(){

		slingscript.gameover = false;

		if(shotsTaken >= maxShots){

			slingscript.gameover = true;
		}

		SetShotMax();
		CheckGameOver();

		UpdateGUI();

		if(state == GameState.playing && Goal.goalMet) {
			if(FollowCamera.s.poi.tag == "projectile") {

				state = GameState.levelend;

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


	public static void ShotFired(){
		s.shotsTaken++;
	}

	void SetShotMax(){

		
		if (level == 0){
			maxShots = 5;
			
		}
		
		if (level == 1){
			maxShots = 10;
			
		}
		
		if (level == 2){
			maxShots = 15;
			
		}
	}

	void CheckGameOver(){

		if (level == 0 && slingscript.gameover == true && follocam.isResting == true){
			Application.LoadLevel ("GameOver");

		}

		if (level == 1 && shotsTaken >= maxShots){
			Application.LoadLevel ("GameOver");
			
		}

		if (level == 2 && shotsTaken >= maxShots){
			Application.LoadLevel ("GameOver");
			
		}
	}
}
