using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private bool gameIsRunning = false;
	private bool gameOver = false;

	private void Awake() {
		gameIsRunning = true;
	}

	private void Update() {
		if (gameIsRunning == false) {
			Time.timeScale = 0f;
		}
	}
}
