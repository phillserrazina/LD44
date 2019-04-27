using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : AI {

	public float distToStop = 0;

	protected override void FixedUpdate() {
		base.FixedUpdate();

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
