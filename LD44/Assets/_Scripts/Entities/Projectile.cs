using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public Character caster;
	public WeaponSO weaponData;
	public float projectileSpeed;
	private bool ignoreEnemies = false;
	private bool ignorePlayer = false;
	private Vector2 target;
	private float horDir;
	private float verDir;

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	private void FixedUpdate() {
		if (ignoreEnemies) {
			transform.position = Vector2.MoveTowards(transform.position, target, projectileSpeed * Time.fixedDeltaTime);
			if (transform.position.Equals(target))
				Destroy(gameObject);
		}

		if (ignorePlayer) {
			GetComponent<Rigidbody2D>().velocity = new Vector2(horDir, verDir) * projectileSpeed * 50 * Time.fixedDeltaTime;
			Destroy(gameObject, 5f);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (ignoreEnemies && other.gameObject.Equals(player.gameObject)) {
			Destroy(gameObject);
			player.TakeDamage(weaponData.damage, weaponData.slowRate,
								weaponData.slowDuration, weaponData.damagePerSecond, weaponData.dpsDuration);
			
		}

		if (ignorePlayer && other.GetComponent<AI>() != null) {
			Destroy(gameObject);
			other.GetComponent<AI>().TakeDamage(weaponData.damage,
														weaponData.slowRate,
														weaponData.slowDuration,
														weaponData.damagePerSecond,
														weaponData.dpsDuration);
			
		}
	}

	public void Initialize() {
		if (caster.GetComponent<AI>() != null) {
			ignoreEnemies = true;
			target = new Vector2(player.transform.position.x, player.transform.position.y);
		}
		else if (caster.GetComponent<Player>() != null) {
			ignorePlayer = true;
			Sprite cs = player.GetComponent<SpriteRenderer>().sprite;

			if (cs == player.leftSideModel) {
				horDir = -1;
				verDir = 0;
				transform.rotation = Quaternion.Euler(0f, 0f, 90f);
			}
			else if (cs == player.rightSideModel) {
				horDir = 1;
				verDir = 0;
				transform.rotation = Quaternion.Euler(0f, 0f, -90f);
			}
			else if (cs == player.frontModel) {
				horDir = 0;
				verDir = -1;
				transform.rotation = Quaternion.Euler(0f, 0f, 180f);
			}
			else if (cs == player.backModel) {
				horDir = 0;
				verDir = 1;
				transform.rotation = Quaternion.Euler(0f, 0f, 0f);
			}
		}
	}
}
