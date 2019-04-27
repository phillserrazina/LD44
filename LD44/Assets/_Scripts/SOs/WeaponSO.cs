using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName="New Weapon", menuName="Weapon")]
public class WeaponSO : ScriptableObject {

	public float damage;
	public float price;
	public Sprite graphic;
}
