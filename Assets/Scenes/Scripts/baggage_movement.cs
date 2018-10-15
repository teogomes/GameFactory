using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baggage_movement : MonoBehaviour {
	public Transform[] points;

	public float Speed;
	private bool input_move;


	// Use this for initialization
	void Start () {
		transform.position = points[0].position;


		input_move = true;
	}
	
	// Update is called once per frame
	void Update () {
		//baggage moving in input_line
		if (input_move) { 
			transform.position = Vector3.MoveTowards (transform.position, points [1].position, Speed * Time.deltaTime);
		}  
		if (transform.position == points [1].position) {
			input_move = false;
		}

	}
}
