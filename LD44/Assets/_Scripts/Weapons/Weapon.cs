using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public WeaponSO weaponData;

	private Player player;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	// METHODS

	public IEnumerator MeleeAttack() {
		player.damageArea.SetActive(true);
		player.UpdateDamageAreaPos();

		yield return new WaitForSeconds(0.3f);
		player.damageArea.SetActive(false);
	}

	// METHODS

	public void RangedAttack() {
		if (weaponData.currentAmmo <= 0)
			return;
		
		weaponData.currentAmmo--;
		Projectile p = Instantiate(weaponData.projectile, gameObject.transform.position, Quaternion.identity);
				p.caster = player;
				p.Initialize();
	}
}
