using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AI {

	public float distToStop = 0;

	protected override void FixedUpdate() {
		base.FixedUpdate();

		if (Time.frameCount % 30 == 0)
		{
			if (Random.value > 0.9f) 
			{
				float v = Random.value;

				if (v < 0.3f) {
					audioManager.Play("Crawling_Bug_01");
				}
				else if (v < 0.6f) {
					audioManager.Play("Crawling_Bug_02");
				}
				else {
					audioManager.Play("Crawling_Bug_03");
				}
			}
		}

		if (Vector2.Distance(transform.position, player.transform.position) > distToStop) {
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
		}
		else
		{
			if (currentAttackCD <= 0) {
				player.TakeDamage(damage);
				currentAttackCD = attackCD;
			}
			else {
				currentAttackCD -= Time.fixedDeltaTime;
			}
		}
	}
}
