using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Goal : MonoBehaviour {
	public Text winningText;
	ParticleSystem pSys;

	void Start () {
		pSys = GetComponent<ParticleSystem>();
		winningText.enabled = false;
	}

	void OnTriggerEnter () {
		pSys.Play();
		winningText.enabled = true;
	}
}
