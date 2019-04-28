using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : AI {

	public float distToShoot = 0;
	public Projectile projectile;

	protected override void FixedUpdate() {
		base.FixedUpdate();

		if (Vector2.Distance(transform.position, player.transform.position) > distToShoot) {
			transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.fixedDeltaTime);
		}
		else
		{
			if (currentAttackCD <= 0) {
				if (Random.value > 0.7f) {
					audioManager.Play("AlienSpit");
				}
				
				Projectile p = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
				p.caster = this;
				p.Initialize();
				currentAttackCD = attackCD;
			}
			else {
				currentAttackCD -= Time.fixedDeltaTime;
			}
		}
	}
}
