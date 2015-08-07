using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ForceZone : MonoBehaviour 
{
	// Force applied to objects that enter this object's trigger boundaries
	public float force = 1f;
	
	// Internal list that tracks objects that enter this object's "zone"
	private List<Collider> objects = new List<Collider>();
	
	// This function is called every fixed framerate frame
	void FixedUpdate()
	{
		// For every object being tracked
		for(int i = 0; i < objects.Count; i++)
		{
			// Get the rigid body for the object.
			Rigidbody body = objects[i].attachedRigidbody;
			
			// Apply the force
			body.AddForce(transform.forward * force);
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		objects.Add(other);
	}
	
	void OnTriggerExit(Collider other)
	{
		objects.Remove(other);
	}
}
