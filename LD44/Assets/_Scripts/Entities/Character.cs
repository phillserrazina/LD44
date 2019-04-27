using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	// VARIABLES

	public float healthPoints = 100f;
	public float speed = 0;
	protected int verticalDirection = 0;
	protected int horizontalDirection = 0;

	protected Rigidbody2D rb;
	protected SpriteRenderer spriteRenderer;

	// EXECUTION METHODS

	protected virtual void Awake () {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void TakeDamage(float dmg) {
		healthPoints -= dmg;
		StartCoroutine(TakeDamageVisuals());
	}

	private IEnumerator TakeDamageVisuals() {
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.15f);
		spriteRenderer.color = Color.white;
	}
}
