using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameMenu : MonoBehaviour
{
	public TMP_Text message;
	[HideInInspector] public static GameMenu menu;
	[SerializeField] private GameObject resumeBtn;
	[SerializeField] private GameObject quitBtn;
	[SerializeField] private GameObject closeBtn;


	void Awake(){
		if(menu != null){
			Destroy(gameObject);
		}else{
			menu = this;
			Time.timeScale = 0f;
		}
	}

	public void isPlayerDead(bool state){
		if(state){
			message.text = "You have lost all light.";
			resumeBtn.SetActive(false);
			quitBtn.SetActive(true);
			closeBtn.SetActive(false);
		}else{
			message.text = "Paused";
			resumeBtn.SetActive(true);
			quitBtn.SetActive(false);
		}
	}

	public void quit(){
		Application.Quit();
	}

	public void goToMainMenu(){
		Time.timeScale = 1f;
		SceneManager.LoadScene(0);
	}

	public void closeWindow(){
		Time.timeScale = 1f;
		Destroy(gameObject);
	}
}
