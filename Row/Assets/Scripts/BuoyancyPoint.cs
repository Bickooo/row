using UnityEngine;
using System.Collections;

public class BuoyancyPoint : MonoBehaviour {
	public Rigidbody rb;
	public float seaLevel = 1f;
	public float forceMultiplier;

	void FixedUpdate () {
		if(transform.position.y < seaLevel) {
			rb.AddForceAtPosition((Vector3.up * (seaLevel - transform.position.y)) * forceMultiplier, transform.position);

#if UNITY_EDITOR
			Debug.DrawLine(transform.position, new Vector3(transform.position.x, 0, transform.position.z), Color.green);
#endif
		}
	}
}
