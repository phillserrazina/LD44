using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

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
	}

	public void StartGame() {
		gameIsRunning = true;
		player.Initialize();
	}
}
