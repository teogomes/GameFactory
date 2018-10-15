using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class dragndrop3 : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler{
	private static int slots = 6;//always one more
	private static int numOfCommands =6;
	GameObject[] emptyslot = new GameObject[slots];
	float[] distance = new float[slots];
	public static int[] commands = new int[numOfCommands];
	GameObject panel;


	void Start () {
		int i = 0;
		for  (i = 0; i < numOfCommands; i++) {
			commands [i] = -1;
		}
		commands [numOfCommands-1] = -2;

		for(i=1;i<slots+1;i++){
			emptyslot[i-1] = GameObject.Find("Slot"+i);
		}
			


	}
	
	// Update is called once per frame
	void Update () {
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		

		for (int i = 0; i < slots -1; i++) { // me epifilaxi to slots -1 
			if (Vector3.Distance(this.transform.position,emptyslot[i].transform.position)<15f){
				emptyslot[i].tag = "empty";
				commands [i] = -1;
			}
		}

	}




	#region IDragHandler implementation
	public void OnDrag (PointerEventData eventData)
	{
		
		this.transform.position = Input.mousePosition;
	}
	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		for(int i=0;i<slots-1;i++){ //find distance between command and all the slots
				
				distance[i] = Vector3.Distance(transform.position,emptyslot[i].transform.position);
			}
		float min=distance[0];
		int which = 0;
		for (int i = 1; i < slots-1 ; i++) {   //get the minimum dist
			if (distance [i] < min) {
				min = distance [i];
				which = i;
			}
		}
		if (min < 30f && emptyslot [which].tag.Equals ("empty")) {
			this.transform.position = emptyslot [which].transform.position;
			emptyslot [which].tag = "occupied";
			commands [which] = numCommand (this.name);
		} else {
			this.transform.position = transform.parent.gameObject.transform.position;
		
		}

	}

	#endregion

	int numCommand(string command){
		if (command.Equals ("input"))
			return 0;
		else if (command.Equals ("output"))
			return 1;
		else if (command.Equals ("queue"))
			return 2;
		else if (command.Equals ("jLabel"))
			return 3;
		else if (command.Equals ("Label"))
			return 4;
		else
			return -1;

	}
}
