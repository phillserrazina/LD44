using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public AudioMixer audioMixer;

	public void NewGame() {
		PlayerPrefs.SetInt("Level", 1);
		SceneManager.LoadScene("Arena");
	}

	public void ContinueGame() {
		SceneManager.LoadScene("Arena");
	}

	public void QuitGame() {
		Application.Quit();
	}

	public void SetVolume(float volume) {
		audioMixer.SetFloat("volume", volume);
	}
}
