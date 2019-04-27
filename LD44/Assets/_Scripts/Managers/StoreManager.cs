using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour {

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	public bool BuyWeapon(WeaponSO weapon) {
		if (player.healthPoints < weapon.price)
			return false;
		
		player.availableWeapons.Add(weapon);
		player.healthPoints -= weapon.price;

		return true;
	}
}
