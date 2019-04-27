using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Character {

	public float distToStop;
	private Player player;

	protected override void Awake() {
		base.Awake();
		player = FindObjectOfType<Player>();
	}

	private void FixedUpdate() {
		if (Vector2.Distance(transform.position, player.transform.position) > distToStop) {
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
		}

		if (this.healthPoints <= 0)
			Destroy(this.gameObject);
	}
}
