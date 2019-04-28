using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public WeaponSO weaponData;

	private Player player;

	private AudioManager audioManager;

	private void Awake() {
		player = FindObjectOfType<Player>();
		audioManager = FindObjectOfType<AudioManager>();
	}

	// METHODS

	public IEnumerator MeleeAttack() {
		float v = Random.value;

		if (v < 0.5f) {
			audioManager.Play("Sword_Swing_01");
		}
		else {
			audioManager.Play("Sword_Swing_02");
		}

		player.damageArea.SetActive(true);
		player.UpdateDamageAreaPos();

		yield return new WaitForSeconds(0.3f);
		player.damageArea.SetActive(false);
	}

	// METHODS

	public void RangedAttack() {
		if (weaponData.currentAmmo <= 0) {
			audioManager.Play("Empty");
			return;
		}
		
		float v = Random.value;

		if (v < 0.5f) {
			audioManager.Play("Arrow_01");
		}
		else {
			audioManager.Play("Arrow_02");
		}

		weaponData.currentAmmo--;
		Projectile p = Instantiate(weaponData.projectile, gameObject.transform.position, Quaternion.identity);
				p.caster = player;
				p.Initialize();
	}
}
