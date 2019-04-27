using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject endScreen;

	private bool gameIsRunning = false;
	private bool gameOver = false;

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	private void Update() {
		if (gameIsRunning == false) {
			Time.timeScale = 0f;
		}
		else {
			Time.timeScale = 1f;
		}

		if (player.healthPoints <= 0) {
			Time.timeScale = 0;
			endScreen.SetActive(true);
		}
	}

	public void StartGame() {
		gameIsRunning = true;
		player.Initialize();
	}

	public void ChangeScene(string sceneName) {
		SceneManager.LoadScene(sceneName);
	}

	public void Retry() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
