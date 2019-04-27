using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	// VARIABLES

	public Weapon currentWeapon;
	public Vector2 attackDirection { get; private set; }

	public GameObject meleeAttackEffect;

	// EXECUTION METHODS

	protected override void Awake () {
		attackDirection = new Vector2(0f, -1f);
		base.Awake();
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
	}

	private void MoveCharacter() {
		rb.velocity = new Vector2(horizontalDirection, verticalDirection) * speed * Time.fixedDeltaTime;
	}
}
