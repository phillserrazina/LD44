﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Character {

	public float distToStop;
	public float damage;
	private Player player;
	public int level;

	private float attackCD = 1f;
	private float currentAttackCD = 0;

	private GameManager gameManager;

	// EXECUTION FUNCTIONS

	protected override void Awake() {
		base.Awake();
		gameManager = FindObjectOfType<GameManager>();
		player = FindObjectOfType<Player>();
	}

	private void FixedUpdate() {
		if (Vector2.Distance(transform.position, player.transform.position) > distToStop) {
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
		}
		else
		{
			if (currentAttackCD <= 0) {
				player.TakeDamage(damage);
				currentAttackCD = attackCD;
			}
			else {
				currentAttackCD -= Time.fixedDeltaTime;
			}
		}

		if (this.healthPoints <= 0) {
			Destroy(this.gameObject);
			gameManager.killedEnemies++;
		}
	}
}
