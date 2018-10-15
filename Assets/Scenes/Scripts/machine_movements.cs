using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class machine_movements : MonoBehaviour {
	public Button btn2;
	public Text[] txt;
	public static int numberOfCommands = 2;
	public Rigidbody rb;
	public float Speed;
	public Transform[] points;
	private int currentPoint=0,i=0,l=0,labelPoint=-1,limit=0,baggsPassed=0;
	private bool exe = false,found;

	//For Suitcases

	public GameObject[] suits;
	private int currentCase = 0;
	private bool taken = false,on_output=false;
	public Transform[] SuitPoints;
	private int[] queueArray = new int[3];
	private int queuePointer = 0;


	// Use this for initialization
	public void Start () {
		
		Button button2 = btn2.GetComponent<Button>();
		rb = GetComponent<Rigidbody> ();

		button2.onClick.AddListener (taskOnClick2);
		transform.position = points [0].position;

	
		l = 0;
		for (int l = 0; l < 3; l++) {
			queueArray [l] = -1;
		}



	}

	// Update is called once per frame
	void Update () {
		MoveSuitCases ();
		if (transform.position == points [currentPoint].position) 
		{
			currentPoint = 0;
			if (exe) {
				
				if (dragndrop.commands [i].Equals (0)) {
					currentPoint = 1;
				} else if (dragndrop.commands [i].Equals (1)) {
					currentPoint = 2;
				} else if (dragndrop.commands [i].Equals (2)) {
					currentPoint = 3;
				} else if (dragndrop.commands [i].Equals (3) && limit<2) {
					i = labelPoint;
					limit++;
				}
				else if (dragndrop.commands [i].Equals (4)) {
					labelPoint = i;

				}
				if (dragndrop.commands [i + 1] != -2) {
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
				if(baggsPassed == 3) txt[0].enabled = true;
				Destroy (suits [currentCase - 1]);
		}

		}

	

	}



	void taskOnClick2(){
		print (dragndrop.commands [0]);
		print (dragndrop.commands [1]);
		print (dragndrop.commands [2]);
		exe = true;
		i = 0;
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
				l = 0;
				while (l < 3) {
					if (queueArray [l] == -1) {
						queueArray [l] = currentCase;
						l = 3;
					}
					l++;
				}
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







	}




}
