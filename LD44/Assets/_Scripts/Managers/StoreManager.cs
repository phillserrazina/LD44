using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour {

	public GameObject itemPrefab;
	public GameObject itemList;

	private Player player;
	public WeaponSO[] allWeapons;

	private void Awake() {
		player = FindObjectOfType<Player>();
	}

	private void OnEnable() {
		int numOfWeapons = Random.Range(1, Mathf.Clamp(allWeapons.Length+1, 0, 7));

		for (int i = 0; i < numOfWeapons; i++) {
			itemPrefab.GetComponent<StoreItem>().weapon = allWeapons[Random.Range(0, numOfWeapons)];

			GameObject go = Instantiate(itemPrefab) as GameObject;
			go.transform.SetParent(itemList.transform);
			go.transform.localScale = Vector3.one;
		}
	}

	private void OnDisable() {
		for (int i = 0; i < itemList.transform.childCount; i++) {
			Destroy(itemList.transform.GetChild(i).gameObject);
		}
	}

	public void BuyWeapon (WeaponSO weapon) {
		if (player.healthPoints < weapon.price)
			return;
		
		player.availableWeapons.Add(weapon);
		player.healthPoints -= weapon.price;
		
		for (int i = 0; i < itemList.transform.childCount; i++) {
			if (itemList.transform.GetChild(i).GetComponent<StoreItem>().weapon.Equals(weapon)) {
				Destroy(itemList.transform.GetChild(i).gameObject);
			}
		}
	}
}
