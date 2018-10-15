using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	public GameObject mainPanel;
	public GameObject levelsPanel;
	public GameObject audioPanel;
	public GameObject pausePanel;
	public Button pauseButton;

	// Use this for initialization
	void Start () {
		Button pausebutton = pauseButton.GetComponent<Button>();
		pausebutton.onClick.AddListener (resumeGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartGame(){
		SceneManager.LoadScene ("Level1");
	
	}
	public void openLevelsPanel(){
		mainPanel.SetActive (false);
		levelsPanel.SetActive (true);
		for (int i = 0; i < 5; i++) {
			levelsPanel.transform.GetChild(2).GetChild(i).gameObject.SetActive (false);
		}
		for (int i = 0; i < lives.unlockedlevel +1; i++) {
			levelsPanel.transform.GetChild(2).GetChild(i).gameObject.SetActive (true);
		}
	}
	public void openAudioPanel(){
		mainPanel.SetActive (false);
		audioPanel.SetActive (true);
	}
	public void quitGame(){
		Application.Quit ();
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#endif
	}
	//LevelSelector

	public void level1(){
		SceneManager.LoadScene ("Level1");
	}

	public void level2(){
		lives.howmuch = 3;
		SceneManager.LoadScene ("Level2");

	}

	public void level3(){
		SceneManager.LoadScene ("Level3");
		lives.howmuch = 3;
	}

	public void level4(){
		SceneManager.LoadScene ("Level4");	
		lives.howmuch = 3;
	}

	public void level5(){
		SceneManager.LoadScene ("Level5");	
		lives.howmuch = 3;
	}

	public void toMainPanel(){
		mainPanel.SetActive (true);
		audioPanel.SetActive (false);
		levelsPanel.SetActive (false);

	}

	public void resumeGame(){
		if (!pausePanel.activeInHierarchy) {
			pausePanel.SetActive (true);
		} else {
			pausePanel.SetActive (false);
		}
	}

	public void restartlevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

	}
		
}
