using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Rotator : MonoBehaviour, ISaveable {
	public float speed;
	public Vector3 rotateFrom;
	public Vector3 rotateTo;

	public void Invert() {
		Vector3 tmp = rotateFrom;
		rotateFrom = rotateTo;
		rotateTo = tmp;
	}

	public void Save(BinaryWriter w) {
		//speed
		w.Write(speed);
		//rotateFrom
		{
			w.Write(rotateFrom.x);
			w.Write(rotateFrom.y);
			w.Write(rotateFrom.z);
		}
		//rotateTo
		{
			w.Write(rotateTo.x);
			w.Write(rotateTo.y);
			w.Write(rotateTo.z);
		}
	}

	public void Load(BinaryReader r) {
		//speed
		speed = r.ReadSingle();
		//rotateFrom
		{
			float rotX = r.ReadSingle();
			float rotY = r.ReadSingle();
			float rotZ = r.ReadSingle();
			rotateFrom = new Vector3(rotX, rotY, rotZ);
		}
		//rotateTo
		{
			float rotX = r.ReadSingle();
			float rotY = r.ReadSingle();
			float rotZ = r.ReadSingle();
			rotateTo = new Vector3(rotX, rotY, rotZ);
			transform.rotation = Quaternion.Euler(rotateTo);
		}
	}

	void Update() {
		transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, rotateTo, speed * Time.deltaTime));
	}
}
