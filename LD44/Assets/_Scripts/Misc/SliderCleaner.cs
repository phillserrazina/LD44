using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCleaner : MonoBehaviour {

	private void OnEnable () {
		float val;
		FindObjectOfType<MenuManager>().audioMixer.GetFloat("volume", out val);
		GetComponent<Slider>().value = val;
	}
}
