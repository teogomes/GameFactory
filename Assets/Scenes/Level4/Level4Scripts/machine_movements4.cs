using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class machine_movements4 : MonoBehaviour {
	public Button btn2;
	public GameObject gameoverPanel,livesPanel,infoPanel,pausePanel;
	public static int numberOfCommands = 7;
	public Rigidbody rb;
	public float Speed;
	private string code;
	public Transform[] points;
	private int currentPoint=0,i=0,l=0,labelPoint=-1,limit=0,baggsPassed=0,buttonpassed=0;
	private bool exe = false,found;
	private float timer = 90;
	public Text timerText;
	private bool gameover = false;

	//For Suitcases

	public GameObject[] suits;
	private int currentCase = 0;
	private bool taken = false,on_output=false;
	public Transform[] SuitPoints;
	private int[] queueArray = new int[3];
	private int queuePointer = 0;
	private AudioSource sound;
	private AudioClip clip;

	private lives life;
	// Use this for initialization
	public void Start () {
		
		Button button2 = btn2.GetComponent<Button>();
		rb = GetComponent<Rigidbody> ();
		sound = GetComponent<AudioSource>();
		button2.onClick.AddListener (taskOnClick2);
		transform.position = points [0].position;

		l = 0;
		for (int l = 0; l < 3; l++) {
			queueArray [l] = -1;
		}

		//lifePanel
		life = new lives ();
		livesPanel.transform.GetChild (0).gameObject.SetActive (false);
		livesPanel.transform.GetChild (1).gameObject.SetActive (false);
		livesPanel.transform.GetChild (2).gameObject.SetActive (false);
		for(int i=0;i<life.getLives();i++){
			livesPanel.transform.GetChild (i).gameObject.SetActive (true);
		}

	}

	// Update is called once per frame
	void Update () {


		//timer
		if (!gameover) {
			timer -= Time.deltaTime;
			timerText.text = "Time: " + ((int)timer).ToString ();
		}
		if ((int)timer < 83)
			infoPanel.SetActive (false);
		
		if (timer < 0) {
			gameoverPanel.SetActive (true);
			gameoverPanel.transform.GetChild (1).GetChild (1).gameObject.SetActive(false);
			gameoverPanel.GetComponentInChildren<Text> ().text = " Try Again";
			life.downLives ();
		}
		if (pausePanel.activeInHierarchy || gameoverPanel.activeInHierarchy) 
			gameover = true;
		else
			gameover = false;


		MoveSuitCases ();
		if (transform.position == points [currentPoint].position) 
		{
			currentPoint = 0;
			if (exe) {
				
				if (dragndrop4.commands [i].Equals (0)) {
					currentPoint = 1;
				} else if (dragndrop4.commands [i].Equals (1)) {
					currentPoint = 2;
				} else if (dragndrop4.commands [i].Equals (2)) {
					currentPoint = 3;
				} else if (dragndrop4.commands [i].Equals (3) && limit < 2) {
					i = labelPoint;
					limit++;
				} else if (dragndrop4.commands [i].Equals (4)) {
					labelPoint = i;

				} else if (dragndrop4.commands [i].Equals (5)) {
					if (suits [currentCase].name == "Suitcase3")
						i++;
				}
				if (dragndrop4.commands [i + 1] != -2) {
					i++;
				}
				else {
					exe = false;
					limit = 0;

				}

			}
		}else
			transform.position = Vector3.MoveTowards (transform.position, points [currentPoint].position, Speed * Time.deltaTime);
		
			}

	public void MoveSuitCases(){



		if (taken) {
			suits [currentCase].transform.position = this.transform.position;
		}



		if (on_output) {
			suits [currentCase - 1].transform.position = Vector3.MoveTowards (suits [currentCase - 1].transform.position, SuitPoints [1].transform.position, Speed * Time.deltaTime);
			if (suits [currentCase - 1].transform.position == SuitPoints [1].transform.position){
				baggsPassed++;
				on_output = false;
				if (suits [currentCase - 1].name == "Suitcase3" && buttonpassed == 1) {
					gameoverPanel.SetActive (true);
					gameoverPanel.GetComponentInChildren<Text> ().text = "Great!Procceed to the next level ! \n Points: " + ((int)timer *960 - buttonpassed*750);
					clip = Resources.Load<AudioClip> ("levelcomp");
					sound.volume = 1f;
					sound.PlayOneShot (clip);
					sound.volume = 0.4f;
					lives.unlockedlevel = 4;
					gameover = true;
				} else {
					gameoverPanel.SetActive (true);
					gameoverPanel.transform.GetChild (1).GetChild (1).gameObject.SetActive(false);
					gameoverPanel.GetComponentInChildren<Text> ().text = " Try Again , Don't forget the loop";
					gameover = true;
					life.downLives ();

				}
				Destroy (suits [currentCase - 1]);

		}

		}

	

	}



	void taskOnClick2(){
		if (!gameover) 
			exe = true;
		else
			exe = false;
		i = 0;
		buttonpassed++;

	}




	//Player reached Queue And Let

	void OnTriggerEnter(Collider other) {

		if (other.name == "InputPoint") {
				if (currentCase < suits.Length) {
					taken = true;
				} else {
					print ("No more bags");
				}
		}


		if (other.name == "OutputPoint" && taken) {
			suits [currentCase].transform.position = SuitPoints [0].position;
			taken = false;
			on_output = true;

			for (int i = 0; i < 3; i++) {
				if (queueArray [i] == currentCase)
					queueArray [i] = -1;

			}
			currentCase++;
			l = 0;
			do {
				found = false;
				if (queueArray [l] == currentCase) {
					currentCase++;
					found = true;
				}
				l++;
			} while(!(l > 2 && !found));

		
		}

		if (other.name == "QueuePoint") {
			if (taken) {
				suits [currentCase].transform.position = new Vector3 (SuitPoints [2].position.x, SuitPoints [2].position.y, SuitPoints [2].position.z + ((queuePointer) * -1.6f));
				taken = false;
				currentCase++;
				queuePointer++;
			} else {
				
				l = 0;
				while (l < 3) {
					if (queueArray [l] != -1) {
						currentCase = queueArray [l];
						taken = true;
					}
					l++;
				}
			
			}
		}


		if (sound != null)
			sound.Play ();




	}




}
