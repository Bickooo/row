using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Goal : MonoBehaviour {
	public Text winningText;

	[HideInInspector]
	public bool gameOver = false;
	ParticleSystem pSys;

	void Start () {
		pSys = GetComponent<ParticleSystem>();
		winningText.enabled = false;
	}

	void OnTriggerEnter () {
		pSys.Play();
		winningText.enabled = true;
		GameObject.FindObjectOfType<Timer>().enabled = false;
	}
}
