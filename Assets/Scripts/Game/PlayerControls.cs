using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour, ISaveable {

	public static PlayerControls Instance;

	public Camera cam;
	public CharacterController controller;

	public float moveSpeed;
	public float gravity;
	public float jumpPower;

	public Slider healthBar;

	[ReadOnly]
	public float rotX, rotY;
	[ReadOnly]
	public Vector3 velocity = Vector3.zero;
	[ReadOnly]
	public bool grounded = false;

	void Awake() {
		Instance = this;
	}

	void Start() {
		rotX = transform.eulerAngles.x;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		healthBar.value = 100;
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

		if (InputManager.GetKeyDown(InputManager.Quicksave)) {
			SaveManager.Save();
		}
		if (InputManager.GetKeyDown(InputManager.Quickload)) {
			SaveManager.Load();
		}

		if (grounded && InputManager.GetKeyDown(InputManager.Jump)) {
			velocity.y += jumpPower;
		}

		if (InputManager.GetKeyDown(InputManager.Interact)) {
			RaycastHit hit;
			int layerMask = ~0;
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 3.0f, layerMask)) {
				Interactable interactable = hit.transform.GetComponent<Interactable>();
				if (interactable != null) {
					interactable.onInteract.Invoke();
				}
			}
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
		if (velocity.y < 0 && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 1.2f, layerMask)) {
			grounded = true;
			velocity.y = 0;
		}

		controller.Move(velocity);
	}

	public void Save(BinaryWriter w) {
		//health
		w.Write(healthBar.value);
		//pos
		w.Write(transform.position.x);
		w.Write(transform.position.y);
		w.Write(transform.position.z);
		//player rot
		w.Write(transform.localRotation.x);
		w.Write(transform.localRotation.y);
		w.Write(transform.localRotation.z);
		w.Write(transform.localRotation.w);
		//cam rot
		w.Write(rotX);
	}

	public void Load(BinaryReader r) {
		//health
		healthBar.value = r.ReadSingle();
		//pos
		{
			float posX = r.ReadSingle();
			float posY = r.ReadSingle();
			float posZ = r.ReadSingle();
			controller.enabled = false;
			transform.position = new Vector3(posX, posY, posZ);
			controller.enabled = true;
		}
		//player rot
		{
			float rotX = r.ReadSingle();
			float rotY = r.ReadSingle();
			float rotZ = r.ReadSingle();
			float rotW = r.ReadSingle();
			transform.localRotation = new Quaternion(rotX, rotY, rotZ, rotW);
		}
		//cam rot
		{
			rotX = r.ReadSingle();
		}
	}

}
