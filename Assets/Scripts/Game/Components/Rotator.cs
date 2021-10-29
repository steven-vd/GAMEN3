using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	public float speed;
	public Vector3 rotateFrom;
	public Vector3 rotateTo;

	public void Invert() {
		Vector3 tmp = rotateFrom;
		rotateFrom = rotateTo;
		rotateTo = tmp;
	}

	void Update() {
		transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, rotateTo, speed * Time.deltaTime));
	}
}
