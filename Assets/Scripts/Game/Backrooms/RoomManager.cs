using UnityEngine;

public class RoomManager : MonoBehaviour {

    public static GameObject[,] rooms = new GameObject[4,4];

    public GameObject pfRoom;

    void Start() {
        GenRooms();
    }

    public void GenRooms() {
        for (int x = 0; x < 4; ++x) {
            for (int y = 0; y < 4; ++y) {
                rooms[x,y] = Instantiate<GameObject>(pfRoom, new Vector3(x * 10, 0, y * 10), Quaternion.identity);
            }
        }
    }

}
