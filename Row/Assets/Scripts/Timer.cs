using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	public Text timerText;
	bool timerIsRunning = false;
	float timer = 0f;
	float timerOffset = 0f;

	void Update () {
		if(timerIsRunning) {
			timer = Time.timeSinceLevelLoad;

			System.TimeSpan t = System.TimeSpan.FromSeconds(timer-timerOffset);
			timerText.text = t.ToString();
		}
		else
			timerOffset = Time.timeSinceLevelLoad;

		if(Input.anyKeyDown && !timerIsRunning) {
			timerIsRunning = true;
		}
	}
}
