using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour {
	
	protected Animator animator;
	
	public bool ikActive = false;
	public Transform leftHandObj = null;
	public Transform rightHandObj = null;
	public Transform leftFootObj = null;
	public Transform rightFootObj = null;
	float distanceToLeftHandObj;
	float distanceToRightHandObj;
	
	void Start ()
	{
		animator = GetComponent<Animator>();
	}
	
	//a callback for calculating IK
	void OnAnimatorIK()
	{
		if(animator) {
			
			//if the IK is active, set the position and rotation directly to the goal. 
			if(ikActive) {
				// Set the left hand target position and rotation, if one has been assigned
				if(leftHandObj != null) {
					distanceToLeftHandObj = Vector3.Distance(transform.position, leftHandObj.position)-1f;

					if(distanceToLeftHandObj <= 1) {
						animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1-distanceToLeftHandObj);
						animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1-distanceToLeftHandObj);
						animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
						animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
					}
				}

				// Set the right hand target position and rotation, if one has been assigned
				if(rightHandObj != null) {
					distanceToRightHandObj = Vector3.Distance(transform.position, rightHandObj.position)-1f;
					
					if(distanceToLeftHandObj <= 1) {
						animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1-distanceToRightHandObj);
						animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1-distanceToRightHandObj);
						animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
						animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
					}
				}

				// Set the left foot target position and rotation, if one has been assigned
				if(leftFootObj != null) {
					animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1);
					animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1);
					animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
					animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);
				}

				// Set the right foot target position and rotation, if one has been assigned
				if(rightFootObj != null) {
					animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1);
					animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1);
					animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootObj.position);
					animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
				}
			}
			
			//if the IK is not active, set the position and rotation of the hand and head back to the original position
			else {
				animator.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftHand,0);

				animator.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightHand,0);

				animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot,0);
				animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot,0);

				animator.SetIKPositionWeight(AvatarIKGoal.RightFoot,0);
				animator.SetIKRotationWeight(AvatarIKGoal.RightFoot,0);
			}
		}
	}
}
