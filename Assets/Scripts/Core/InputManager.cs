using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

	public static float MouseSensibility = 320;

	public static List<KeyCode> WalkForward = new List<KeyCode>(new KeyCode[] { KeyCode.W });
	public static List<KeyCode> WalkLeft = new List<KeyCode>(new KeyCode[] { KeyCode.A });
	public static List<KeyCode> WalkBackward = new List<KeyCode>(new KeyCode[] { KeyCode.S });
	public static List<KeyCode> WalkRight = new List<KeyCode>(new KeyCode[] { KeyCode.D });
	public static List<KeyCode> Jump = new List<KeyCode>(new KeyCode[] { KeyCode.Space });
	public static List<KeyCode> Interact = new List<KeyCode>(new KeyCode[] { KeyCode.E });

	public static List<KeyCode> Quicksave = new List<KeyCode>(new KeyCode[] { KeyCode.F8 });
	public static List<KeyCode> Quickload = new List<KeyCode>(new KeyCode[] { KeyCode.F9 });

	public static bool GetKey(List<KeyCode> control) {
		foreach (KeyCode key in control) {
			if (Input.GetKey(key)) {
				return true;
			}
		}
		return false;
	}

	public static bool GetKeyDown(List<KeyCode> control) {
		foreach (KeyCode key in control) {
			if (Input.GetKeyDown(key)) {
				return true;
			}
		}
		return false;
	}

	public static bool GetKeyUp(List<KeyCode> control) {
		foreach (KeyCode key in control) {
			if (Input.GetKeyUp(key)) {
				return true;
			}
		}
		return false;
	}
}
