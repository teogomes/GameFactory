using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scnManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void toMenu(){
		print ("to menu");
		SceneManager.LoadScene ("Menu");
	}

	public void toNextLevel(){
		print (SceneManager.GetActiveScene ().buildIndex + 1);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		lives.howmuch = 3;
	}

	public void restartlevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

	}
}
