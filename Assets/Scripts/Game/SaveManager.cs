using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	public static SaveManager Instance;
	public static readonly long version = 2021_11_12;

	public MonoBehaviour[] Saveables;

	void Awake() {
		Instance = this;
	}

	public static void Save(string file = "quick.save") {
		using (BinaryWriter w = new BinaryWriter(File.OpenWrite(file))) {
			w.Write(version);
			foreach (MonoBehaviour mb in Instance.Saveables) {
				(mb as ISaveable).Save(w);
			}
		}
	}

	public static void Load(string file = "quick.save") {
		using (BinaryReader r = new BinaryReader(File.OpenRead(file))) {
			long version = r.ReadInt64();
			if (version != SaveManager.version) {
				throw new FileLoadException($"Save-File '{file}' is of version '{version}' and cannot be loaded with version '{SaveManager.version}'");
			}
			foreach (MonoBehaviour mb in Instance.Saveables) {
				(mb as ISaveable).Load(r);
			}
		}
	}
}
