using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character {

	// VARIABLES

	public WeaponSO[] availableWeapons;
	public Weapon currentWeapon;
	private int currentWeaponIndex = 0;
	public Vector2 attackDirection { get; private set; }
	public GameObject damageArea;

	// EXECUTION METHODS

	protected override void Awake () {
		base.Awake();

		attackDirection = new Vector2(0f, -1f);
		UpdateWeapon(availableWeapons[currentWeaponIndex]);
	}

	private void Update () {
		GetInput();
	}

	private void FixedUpdate() {
		MoveCharacter();
	}

	// METHODS

	private void GetInput() {
		verticalDirection = (int)Input.GetAxisRaw("Vertical");
		horizontalDirection = (int)Input.GetAxisRaw("Horizontal");

		if (verticalDirection != 0 || horizontalDirection != 0)
			attackDirection = new Vector2(horizontalDirection, verticalDirection);
		
		if (Input.GetKeyDown(KeyCode.Space)) {
			StartCoroutine(currentWeapon.Attack());
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			currentWeaponIndex++;
			
			if (currentWeaponIndex == availableWeapons.Length)
				currentWeaponIndex = 0;
				
			UpdateWeapon(availableWeapons[currentWeaponIndex]);
		}
	}

	private void MoveCharacter() {
		rb.velocity = new Vector2(horizontalDirection, verticalDirection) * speed * Time.fixedDeltaTime;
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
		currentWeapon.damage = newData.damage;
		currentWeapon.GetComponent<SpriteRenderer>().sprite = newData.graphic;
	}
}
