using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

	public GameObject damageArea;
	public Character currentTarget;

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	// METHODS

	public override IEnumerator Attack() {
		damageArea.SetActive(true);
		player.meleeAttackEffect.SetActive(true);
		damageArea.GetComponent<MeleeDamageArea>().hit = false;
		damageArea.transform.localPosition = new Vector2(player.attackDirection.x / 4, player.attackDirection.y / 4);

		if (player.attackDirection.y < 0 && player.attackDirection.x == 0) {
			player.meleeAttackEffect.transform.rotation = Quaternion.Euler(0f, 0f, 0);
		}
		else if (player.attackDirection.y == 0 && player.attackDirection.x > 0) {
			player.meleeAttackEffect.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
		}
		else if (player.attackDirection.y > 0 && player.attackDirection.x == 0) {
			player.meleeAttackEffect.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
		}
		else if (player.attackDirection.y == 0 && player.attackDirection.x < 0) {
			player.meleeAttackEffect.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
		}
		
		player.meleeAttackEffect.transform.localPosition = new Vector2(player.attackDirection.x / 2, player.attackDirection.y / 2);

		yield return new WaitForSeconds(0.3f);
		damageArea.SetActive(false);
		player.meleeAttackEffect.SetActive(false);
	}
}
