﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Character {

	// VARIABLES

	public List<WeaponSO> availableWeapons = new List<WeaponSO>();
	public Weapon currentWeapon;
	private int currentWeaponIndex = 0;
	public Vector2 attackDirection { get; private set; }
	public GameObject damageArea;

	[Header("Visuals")]
	public Sprite frontModel;
	public Sprite leftSideModel;
	public Sprite rightSideModel;
	public Sprite backModel;

	public Transform frontWeaponPoint;
	public Transform leftWeaponPoint;
	public Transform rightWeaponPoint;

	public GameObject ammoVisual;
	public Image currentWeaponIcon;
	public TMP_Text ammoText;

	// EXECUTION METHODS

	protected override void Awake () {
		base.Awake();

		attackDirection = new Vector2(0f, -1f);
	}

	private void Start() {
		spriteRenderer.sprite = frontModel;
		currentWeapon.transform.position = frontWeaponPoint.position;
		currentWeapon.transform.rotation = frontWeaponPoint.rotation;
	}

	private void Update () {
		if (healthPoints <= 0)
			return;
		
		if (currentWeapon.weaponData != null) {
			currentWeaponIcon.sprite = currentWeapon.weaponData.graphic;
			ammoText.text = currentWeapon.weaponData.currentAmmo.ToString();

			if (currentWeapon.weaponData.weaponType == WeaponSO.WeaponTypes.RANGED) {
				ammoVisual.SetActive(true);
			}
			else {
				ammoVisual.SetActive(false);
			}
		}

		GetInput();
		UpdateAnimations();
	}

	private void FixedUpdate() {
		MoveCharacter();
	}

	// METHODS

	public void Initialize() {
		currentWeaponIndex = availableWeapons.Count - 1;
		UpdateWeapon(availableWeapons[currentWeaponIndex]);
		for (int i = 0; i < availableWeapons.Count; i++) {
			availableWeapons[i].currentAmmo = PlayerPrefs.GetInt("Level") * 4;
		}
	}

	private void GetInput() {
		verticalDirection = (int)Input.GetAxisRaw("Vertical");
		horizontalDirection = (int)Input.GetAxisRaw("Horizontal");

		if (verticalDirection != 0 || horizontalDirection != 0)
			attackDirection = new Vector2(horizontalDirection, verticalDirection);
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (currentWeapon.weaponData.weaponType == WeaponSO.WeaponTypes.MELEE)
				StartCoroutine(currentWeapon.MeleeAttack());
			if (currentWeapon.weaponData.weaponType == WeaponSO.WeaponTypes.RANGED)
				currentWeapon.RangedAttack();
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			currentWeaponIndex++;
			
			if (currentWeaponIndex == availableWeapons.Count)
				currentWeaponIndex = 0;
				
			UpdateWeapon(availableWeapons[currentWeaponIndex]);
		}
	}

	private void MoveCharacter() {
		rb.velocity = new Vector2(horizontalDirection, verticalDirection) * speed * Time.fixedDeltaTime;
	}

	private void UpdateAnimations() {
		if (verticalDirection > 0) {
			spriteRenderer.sprite = backModel;
			currentWeapon.transform.position = frontWeaponPoint.position;
			currentWeapon.transform.rotation = frontWeaponPoint.rotation;
		}
		else if (verticalDirection < 0) {
			spriteRenderer.sprite = frontModel;
			currentWeapon.transform.position = frontWeaponPoint.position;
			currentWeapon.transform.rotation = frontWeaponPoint.rotation;
		}
		else if (horizontalDirection > 0) {
			spriteRenderer.sprite = rightSideModel;
			currentWeapon.transform.position = rightWeaponPoint.position;
			currentWeapon.transform.rotation = rightWeaponPoint.rotation;
		}
		else if (horizontalDirection < 0) {
			spriteRenderer.sprite = leftSideModel;
			currentWeapon.transform.position = leftWeaponPoint.position;
			currentWeapon.transform.rotation = leftWeaponPoint.rotation;
		}
	}

	public void UpdateDamageAreaPos() {
		damageArea.GetComponent<MeleeDamageArea>().hit = false;
		damageArea.transform.localPosition = new Vector2(attackDirection.x / 4, attackDirection.y / 4);

		if (attackDirection.y < 0 && attackDirection.x == 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 0);
		}
		else if (attackDirection.y < 0 && attackDirection.x > 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
		}
		else if (attackDirection.y == 0 && attackDirection.x > 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
		}
		else if (attackDirection.y > 0 && attackDirection.x > 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 135f);
		}
		else if (attackDirection.y > 0 && attackDirection.x == 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
		}
		else if (attackDirection.y > 0 && attackDirection.x < 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 225f);
		}
		else if (attackDirection.y == 0 && attackDirection.x < 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 270f);
		}
		else if (attackDirection.y < 0 && attackDirection.x < 0) {
			damageArea.transform.rotation = Quaternion.Euler(0f, 0f, 315f);
		}
	}

	private void UpdateWeapon(WeaponSO newData) {
		currentWeapon.weaponData = newData;
		currentWeapon.GetComponent<SpriteRenderer>().sprite = newData.graphic;
	}
}
