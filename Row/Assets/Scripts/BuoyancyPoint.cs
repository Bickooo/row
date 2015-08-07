using UnityEngine;
using System.Collections;

public class BuoyancyPoint : MonoBehaviour {
	public Rigidbody rb;
	public float surfaceOffset = 1f;
	public float forceMultiplier;
	public LayerMask layerMask;
	Vector3 surface;

	void FixedUpdate () {
		if(transform.position.y < surface.y) {
			rb.AddForceAtPosition((transform.up * ((surface.y + surfaceOffset) - transform.position.y)) * forceMultiplier, transform.position);
		}
	}

	void Update () {
		RaycastHit hit;
		
		if (Physics.Raycast(transform.position, Vector3.up, out hit, 100.0F, layerMask)) {
			surface = hit.point;
		}
		else {
			surface = Vector3.up * Mathf.NegativeInfinity;
		}

#if UNITY_EDITOR
		Debug.DrawLine(transform.position, new Vector3(transform.position.x, surface.y, transform.position.z), Color.red);
#endif
	}
}
