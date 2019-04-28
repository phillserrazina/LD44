using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	// VARIABLES

	public float healthPoints = 100f;
	public float speed = 0;
	public int verticalDirection { get; protected set; }
	public int horizontalDirection { get; protected set; }

	protected Rigidbody2D rb;
	protected SpriteRenderer spriteRenderer;
	protected AudioManager audioManager;

	private bool doingDps;

	// EXECUTION METHODS

	protected virtual void Awake () {
		rb = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
		audioManager = FindObjectOfType<AudioManager>();
	}

	// METHODS

	public void TakeDamage(float dmg, float slow=0, float slowDuration=0, float dps=0, float dpsDuration=0) {
		healthPoints -= dmg;
		StartCoroutine(TakeDamageVisuals(slowDuration > 0));

		if (gameObject.name == "Player") {
			StartCoroutine(ShakeCamera(0.2f, 0.05f));
			FindObjectOfType<AudioManager>().Play("Hit");
		}

		if (slowDuration != 0)
			StartCoroutine(ApplySlow(slow, slowDuration));
		if (dpsDuration != 0) {
			if (!doingDps)
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
		doingDps = true;

		if (duration > 0) {
			healthPoints -= dmg;
			StartCoroutine(TakeDamageVisuals(false));
			yield return new WaitForSeconds(1f);
			duration--;
			StartCoroutine (ApplyDps(dmg, duration));
		}
		else {
			doingDps = false;
		}
	}

	private IEnumerator ShakeCamera (float duration, float magnitude)
	{	
		Vector3 originalPosition = Camera.main.transform.localPosition;
		float elapsed = 0.0f;

		Camera.main.GetComponentInParent<CamFollowPlayer>().enabled = false;

		while (elapsed < duration)
		{
			float x = Random.Range (-1f, 1f) * magnitude;
			float y = Random.Range (-1f, 1f) * magnitude;

			elapsed += Time.deltaTime;

			Camera.main.transform.localPosition = new Vector3 (x, y, originalPosition.z);

			yield return null;
		}

		Camera.main.transform.localPosition = originalPosition;
		Camera.main.GetComponentInParent<CamFollowPlayer>().enabled = true;
	}
}
