using System.IO;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AiDude : MonoBehaviour, ISaveable {

    [ReadOnly]
    public NavMeshAgent navMeshAgent;
    [ReadOnly]
    public float lastAttack = 0;

    void Awake() {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        navMeshAgent.destination = PlayerControls.Instance.transform.position;
        lastAttack += Time.deltaTime;
        if (lastAttack > 1 && Vector3.Distance(transform.position, PlayerControls.Instance.transform.position) < 2) {
            PlayerControls.Instance.healthBar.value -= 10;
            lastAttack = 0;
        }
    }

	public void Save(BinaryWriter w) {
		//pos
		w.Write(transform.position.x);
		w.Write(transform.position.y);
		w.Write(transform.position.z);
		//player rot
		w.Write(transform.localRotation.x);
		w.Write(transform.localRotation.y);
		w.Write(transform.localRotation.z);
		w.Write(transform.localRotation.w);
	}

	public void Load(BinaryReader r) {
		//pos
		{
			float posX = r.ReadSingle();
			float posY = r.ReadSingle();
			float posZ = r.ReadSingle();
			transform.position = new Vector3(posX, posY, posZ);
		}
		//player rot
		{
			float rotX = r.ReadSingle();
			float rotY = r.ReadSingle();
			float rotZ = r.ReadSingle();
			float rotW = r.ReadSingle();
			transform.localRotation = new Quaternion(rotX, rotY, rotZ, rotW);
		}
	}

}
