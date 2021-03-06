﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {

	public int maxEnemies;
	private int currentEnemies;

	public GameObject[] allEnemies;
	private List<GameObject> canSpawnEnemies = new List<GameObject>();

	public float spawnRate;
	public float minSpawnTime;
	public float maxSpawnTime;
	private float currentSpawnTime;

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
		maxEnemies = 5 * PlayerPrefs.GetInt("Level");

		foreach (GameObject e in allEnemies) {
			AI ai = e.GetComponent<AI>();

			if (ai.boss) {
				if (ai.bossLevel == PlayerPrefs.GetInt("Level")) {
					canSpawnEnemies.Add(e);
				}
			}
			else {
				if (ai.level <= PlayerPrefs.GetInt("Level")) {
					canSpawnEnemies.Add(e);
				}
			}
		}
	}
	
	private void Update() {
		if (currentEnemies >= maxEnemies)
			return;
		
		if (currentSpawnTime <= 0) {
			SpawnEnemy();
			currentSpawnTime = Random.Range(minSpawnTime, maxSpawnTime) * spawnRate;
		}
		else {
			currentSpawnTime -= Time.deltaTime;
		}
	}

	private void SpawnEnemy() {
		float spawnY = player.transform.position.y > 0 ? Random.Range(-8, -4) : Random.Range(4, 8);
		float spawnX = player.transform.position.x > 0 ? Random.Range(-8, -4) : Random.Range(4, 8);

		Vector2 spawnPos = new Vector2(spawnX, spawnY);

		GameObject toSpawn = canSpawnEnemies[Random.Range(0, canSpawnEnemies.Count)];

		Instantiate(toSpawn, spawnPos, Quaternion.identity);

		if (toSpawn.GetComponent<AI>().boss)
			canSpawnEnemies.Remove(toSpawn);

		currentEnemies++;
	}
}
