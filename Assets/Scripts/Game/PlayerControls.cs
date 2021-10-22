using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

	public Camera cam;
	public CharacterController controller;

	public float moveSpeed;
	public float gravity;
	public float jumpPower;

	[ReadOnly]
	public float rotX, rotY;
	[ReadOnly]
	public Vector3 velocity = Vector3.zero;
	[ReadOnly]
	public bool grounded = false;

	void Start() {
		rotX = transform.eulerAngles.x;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	void Update() {
		if (InputManager.GetKey(InputManager.WalkForward)) {
			controller.Move(transform.forward * moveSpeed * Time.deltaTime);
		}
		if (InputManager.GetKey(InputManager.WalkLeft)) {
			controller.Move(-transform.right * moveSpeed * Time.deltaTime);
		}
		if (InputManager.GetKey(InputManager.WalkBackward)) {
			controller.Move(-transform.forward * moveSpeed * Time.deltaTime);
		}
		if (InputManager.GetKey(InputManager.WalkRight)) {
			controller.Move(transform.right * moveSpeed * Time.deltaTime);
		}

		if (grounded && InputManager.GetKeyDown(InputManager.Jump)) {
			velocity.y += jumpPower;
		}

		float mouseMoveX = Input.GetAxis("Mouse X") * InputManager.MouseSensibility * Time.deltaTime;
		float mouseMoveY = Input.GetAxis("Mouse Y") * InputManager.MouseSensibility * Time.deltaTime;

		// Rotate Camera
		rotX -= mouseMoveY;
		rotX = Mathf.Clamp(rotX, -90, 90);
		cam.transform.localRotation = Quaternion.Euler(rotX, 0, 0);

		// Rotate Player
		transform.Rotate(Vector3.up, mouseMoveX);
	}

	void FixedUpdate() {
		velocity.y -= gravity;

		RaycastHit hit;
		int layerMask = ~0;
		grounded = false;
		if (velocity.y < 0 && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10.0f, layerMask)) {
			grounded = true;
			velocity.y = 0;
		}

		controller.Move(velocity);
	}

}
