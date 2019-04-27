using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon {

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	// METHODS

	public override IEnumerator Attack() {
		player.damageArea.SetActive(true);
		player.UpdateDamageAreaPos();

		yield return new WaitForSeconds(0.3f);
		player.damageArea.SetActive(false);
	}
}
