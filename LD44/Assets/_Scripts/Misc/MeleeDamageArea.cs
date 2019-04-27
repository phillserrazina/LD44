using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageArea : MonoBehaviour {

	private Player player;
	private Weapon weapon;

	public bool hit = false;
	
	private void OnEnable () {
		player = FindObjectOfType<Player>();
		weapon = player.currentWeapon;
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.GetComponent<Character>() == null || hit)
			return;

		other.gameObject.GetComponent<Character>().healthPoints -= weapon.damage;
		hit = true;
	}
}
