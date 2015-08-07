using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	public Transform followTarget;
	public float smoothingSpeed = 3f;

	void LateUpdate () {
		transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.deltaTime * smoothingSpeed);
		transform.rotation = Quaternion.Lerp(transform.rotation, followTarget.rotation, Time.deltaTime * smoothingSpeed);
	}
}
