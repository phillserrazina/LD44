using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	// VARIABLES

	public float healthPoints = 100f;
	public float speed = 0;
	protected int verticalDirection = 0;
	protected int horizontalDirection = 0;

	private float takingDpsTime = 0;

	protected Rigidbody2D rb;
	protected SpriteRenderer spriteRenderer;

	// EXECUTION METHODS

	protected virtual void Awake () {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// METHODS

	public void TakeDamage(float dmg, float slow=0, float slowDuration=0, float dps=0, float dpsDuration=0) {
		healthPoints -= dmg;
		StartCoroutine(TakeDamageVisuals(slowDuration > 0));

		if (slowDuration != 0)
			StartCoroutine(ApplySlow(slow, slowDuration));
		if (dpsDuration != 0) {
			StopCoroutine("ApplyDps");
			StartCoroutine(ApplyDps(dps, dpsDuration));
		}
	}

	private IEnumerator TakeDamageVisuals(bool slowed) {
		spriteRenderer.color = Color.red;
		yield return new WaitForSeconds(0.15f);
		spriteRenderer.color = slowed ? Color.cyan : Color.white;
	}

	private IEnumerator ApplySlow(float rate, float duration) {
		float temp = speed;

		speed *= (rate / 100f);
		yield return new WaitForSeconds(duration);
		speed = temp;

		spriteRenderer.color = Color.white;
	}

	private IEnumerator ApplyDps(float dmg, float duration) {
		if (duration > 0) {
			healthPoints -= dmg;
			StartCoroutine(TakeDamageVisuals(false));
			yield return new WaitForSeconds(1f);
			duration--;
			StartCoroutine (ApplyDps(dmg, duration));
		}
		
	}
}
