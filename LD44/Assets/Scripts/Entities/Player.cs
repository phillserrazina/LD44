using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	// EXECUTION METHODS

	protected override void Awake () {
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
		verticalDirection = Input.GetAxisRaw("Vertical");
		horizontalDirection = Input.GetAxisRaw("Horizontal");
	}

	private void MoveCharacter() {
		rb.velocity = new Vector2(horizontalDirection, verticalDirection) * speed * Time.fixedDeltaTime;
	}
}
