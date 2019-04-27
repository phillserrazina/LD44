using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	// VARIABLES

	public float speed = 0;
	protected float verticalDirection = 0;
	protected float horizontalDirection = 0;

	protected Rigidbody2D rb;

	// EXECUTION METHODS

	private void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
}
