using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Character {

	public float damage;
	public int level;

	public float attackCD = 1f;
	protected float currentAttackCD = 0;

	protected GameManager gameManager;
	protected Player player;

	// EXECUTION FUNCTIONS

	protected override void Awake() {
		base.Awake();
		gameManager = FindObjectOfType<GameManager>();
		player = FindObjectOfType<Player>();
	}

	protected virtual void FixedUpdate() {
		if (this.healthPoints <= 0) {
			Destroy(this.gameObject);
			gameManager.killedEnemies++;
		}
	}
}
