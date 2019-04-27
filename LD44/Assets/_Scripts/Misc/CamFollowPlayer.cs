using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour {

	private Player player;
	public float smoothness = 0.15f;
	private Vector3 velocity = Vector3.zero;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	private void Update() {
		Vector3 pos = player.transform.position + new Vector3(0f, 0f, -4f);
		transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, smoothness);
	}
}