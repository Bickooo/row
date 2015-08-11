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
	public LayerMask layerMask;
	public float rowingCutoffAngle = 2f;
	bool paddleUnderwater;
	float damping;
	bool isInRowingAngles;

	void Update () {
		RaycastHit hit;
		
		if (Physics.Raycast(paddle.position, Vector3.up, out hit, 100.0F, layerMask))
			paddleUnderwater = true;
		else
			paddleUnderwater = false;

		if(paddleUnderwater)
		{
			if(Input.GetAxisRaw(verticalAxis) != 0f && Input.GetAxisRaw(lateralAxis) != 0f)
				dragPoint.drag = 0;
			else
				dragPoint.drag = paddleDrag;
			
			damping = underwaterDamping;
		}
		else
		{
			damping = 1;
			dragPoint.drag = 0;
		}
		
		if(Input.GetAxisRaw(verticalAxis) != 0f)
			transform.Rotate(Vector3.forward * Input.GetAxis(verticalAxis) * verticalSpeed, Space.Self);
		
		if(Input.GetAxisRaw(lateralAxis) != 0f)
			transform.Rotate(Vector3.up * Input.GetAxis(lateralAxis) * lateralSpeed * damping, Space.World);
	}

	void FixedUpdate () {
		Vector3 localAngles = transform.localRotation.eulerAngles;

		if(localAngles.y > (minMaxLateral.x + rowingCutoffAngle) || localAngles.y < (minMaxLateral.y - rowingCutoffAngle))
			isInRowingAngles = true;
		else
			isInRowingAngles = false;

		if(Input.GetAxis(lateralAxis) != 0f && paddleUnderwater && isInRowingAngles) {
			boatRigidbody.AddForceAtPosition(transform.forward * Input.GetAxis(lateralAxis) * rowingPower, paddle.position);
		}
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
