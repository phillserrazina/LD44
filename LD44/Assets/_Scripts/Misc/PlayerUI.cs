using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour {

	public TMP_Text playerHealthText;

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	private void Update() {
		playerHealthText.text = player.healthPoints.ToString();
	}
}
