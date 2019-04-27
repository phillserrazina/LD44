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

	// EXECUTION METHODS

	protected virtual void Awake () {
		rb = GetComponent<Rigidbody2D>();
	}
}
