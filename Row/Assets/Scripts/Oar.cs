using UnityEngine;
using System.Collections;

public class Oar : MonoBehaviour {
	public Vector2 minMaxVertical;
	public Vector2 minMaxLateral;
	public string verticalAxis;
	public string lateralAxis;
	public float verticalSpeed = 2f;
	public float lateralSpeed = 5f;
	public float underwaterDamping = 0.4f;
	public Transform paddle;
	public Rigidbody boatRigidbody;
	public float rowingPower = 5f;
	public Rigidbody dragPoint;
	public float paddleDrag = 2f;
	public float underwaterDepth = 0.1f;
	bool paddleUnderwater;
	float damping;

	void Update () {
		paddleUnderwater = paddle.position.y < underwaterDepth ? true : false;

		if(paddleUnderwater) {
			damping = underwaterDamping;
			dragPoint.drag = paddleDrag;
		}
		else {
			damping = 1;
			dragPoint.drag = 0;
		}

		if(Input.GetAxis(verticalAxis) != 0f)
			transform.Rotate(Vector3.forward * Input.GetAxis(verticalAxis) * verticalSpeed, Space.Self);

		if(Input.GetAxis(lateralAxis) != 0f)
			transform.Rotate(Vector3.up * Input.GetAxis(lateralAxis) * lateralSpeed * damping, Space.World);
	}

	void FixedUpdate () {
		if(Input.GetAxis(lateralAxis) != 0f && paddleUnderwater)
			boatRigidbody.AddForceAtPosition(transform.forward * Input.GetAxis(lateralAxis) * rowingPower, paddle.position);
	}

	void LateUpdate () {
		Vector3 localAngles = transform.localRotation.eulerAngles;
		float yAngle = localAngles.y;
		float zAngle = localAngles.z;

		if(yAngle < 180.0f)
			yAngle = Mathf.Clamp(yAngle, 0f, minMaxLateral.y);
		else
			yAngle = Mathf.Clamp(yAngle, minMaxLateral.x, 360f);

		if(zAngle < 180.0f)
			zAngle = Mathf.Clamp(zAngle, 0f, minMaxVertical.y);
		else
			zAngle = Mathf.Clamp(zAngle, minMaxVertical.x, 360f);

		localAngles = new Vector3(0, yAngle, zAngle);

		transform.localRotation = Quaternion.Euler(localAngles);
	}
}
