using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lives : MonoBehaviour {

	// Use this for initialization
	public  static int howmuch = 3;
	public static int unlockedlevel = 0;

	public int getLives(){
		return howmuch;
	}

	public void downLives(){
		howmuch--;
	}
}
