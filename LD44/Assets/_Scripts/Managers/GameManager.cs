using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	public GameObject endScreen;
	public GameObject victoryScreen;
	public SpriteRenderer currentArenaGraphic;
	public Sprite[] arenaGraphics;
	[HideInInspector] public int killedEnemies = 0;
	private int currentLevel = 1;

	private bool gameIsRunning = false;

	private Player player;
	private SpawnManager spawnManager;

	// EXECUTION FUNCTIONS

	private void Awake() {
		currentLevel = PlayerPrefs.GetInt("Level", 1);
		player = FindObjectOfType<Player>();
		spawnManager = FindObjectOfType<SpawnManager>();
		player.healthPoints = 5 * currentLevel;

		currentArenaGraphic.sprite = arenaGraphics[Random.Range(0, arenaGraphics.Length)];
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

		if (killedEnemies >= spawnManager.maxEnemies) {
			victoryScreen.SetActive(true);
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene("MainMenu");
		}
	}

	// METHODS

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

	public void NextLevel() {
		PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
		Retry();
	}
}
