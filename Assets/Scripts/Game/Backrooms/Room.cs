using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {

    public GameObject goDoorE, goDoorN, goDoorW, goDoorS;

    void Start() {
        int openDoors = Random.Range(1, 16);
        if((openDoors & 1 << 0) != 0) {
            goDoorE.SetActive(false);
        }
        if((openDoors & 1 << 1) != 0) {
            goDoorN.SetActive(false);
        }
        if((openDoors & 1 << 2) != 0) {
            goDoorW.SetActive(false);
        }
        if((openDoors & 1 << 3) != 0) {
            goDoorS.SetActive(false);
        }
    }

}