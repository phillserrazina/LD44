using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="New Weapon", menuName="Weapon")]
public class WeaponSO : ScriptableObject {

	public enum WeaponTypes {
		RANGED,
		MELEE
	}

	public WeaponTypes weaponType;

	public float damage;
	public float price;
	public Sprite graphic;

	public Projectile projectile;

	[Header("Extra Effects")]
	[Range(0, 100)] public float slowRate;
	public float slowDuration;
	public float damagePerSecond;
	public float dpsDuration;
}
