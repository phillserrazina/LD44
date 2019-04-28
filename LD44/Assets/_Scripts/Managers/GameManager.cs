using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	public AudioMixer audioMixer;

	public GameObject endScreen;
	public GameObject victoryScreen;
	public GameObject pauseMenu;
	public GameObject storeMenu;
	public GameObject pauseIcon;
	public SpriteRenderer currentArenaGraphic;
	public Sprite[] arenaGraphics;
	[HideInInspector] public int killedEnemies = 0;
	private int currentLevel = 1;

	public bool gameIsPaused = false;

	private bool hasPlayedCheer = false;

	private Player player;
	private SpawnManager spawnManager;
	private AudioManager audioManager;

	// EXECUTION FUNCTIONS

	private void Awake() {
		audioManager = FindObjectOfType<AudioManager>();

		if (SceneManager.GetActiveScene().name == "MainMenu") {
			return;
		}

		if (PlayerPrefs.GetInt("Level") <= 0)
			PlayerPrefs.SetInt("Level", 1);
		currentLevel = PlayerPrefs.GetInt("Level", 1);
		player = FindObjectOfType<Player>();
		spawnManager = FindObjectOfType<SpawnManager>();
		player.healthPoints = 5 * currentLevel;

		currentArenaGraphic.sprite = arenaGraphics[Random.Range(0, arenaGraphics.Length)];

		Time.timeScale = 0f;
	}

	private void Update() {
		if (SceneManager.GetActiveScene().name == "MainMenu") {
			return;
		}

		if (player.healthPoints <= 0) {
			Time.timeScale = 0;
			endScreen.SetActive(true);
		}

		if (killedEnemies >= spawnManager.maxEnemies) {
			victoryScreen.SetActive(true);
			if (!hasPlayedCheer) {
				hasPlayedCheer = true;
				audioManager.Play("Cheering");
			}
		}

		if (endScreen.activeSelf == true || victoryScreen.activeSelf == true || storeMenu.activeSelf == true) {
			pauseIcon.SetActive(false);
			return;
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (!gameIsPaused) {
				PauseGame();
			}
			else {
				UnpauseGame();
			}
		}
	}

	// METHODS

	public void StartGame() {
		Time.timeScale = 1f;
		pauseIcon.SetActive(true);
		audioManager.Play("Start");
		player.Initialize();
	}

	public void PauseGame() {
		Time.timeScale = 0f;
		gameIsPaused = true;
		pauseMenu.SetActive(true);
		pauseIcon.SetActive(false);
	}

	public void UnpauseGame() {
		Time.timeScale = 1f;
		gameIsPaused = false;
		pauseMenu.SetActive(false);
		pauseIcon.SetActive(true);
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

	public void SetVolumeGeneral(float volume) {
		audioMixer.SetFloat("volume", volume);
	}

	public void SetVolumeMusic(float volume) {
		audioMixer.SetFloat("musicVolume", volume);
	}

	public void SetVolumeVfx(float volume) {
		audioMixer.SetFloat("vfxVolume", volume);
	}
}
