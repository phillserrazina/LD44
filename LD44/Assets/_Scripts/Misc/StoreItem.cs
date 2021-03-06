﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreItem : MonoBehaviour {

	public Image itemIcon;
	public TMP_Text itemName;
	public TMP_Text itemPrice;
	public Button buyButton;
	public WeaponSO weapon;

	private StoreManager storeManager;

	private void OnEnable() {
		storeManager = FindObjectOfType<StoreManager>();

		itemIcon.sprite = weapon.graphic;
		itemName.text = weapon.name;
		itemPrice.text = weapon.price.ToString();
		buyButton.onClick.AddListener(() => { storeManager.BuyWeapon(weapon); } );
	}
}
