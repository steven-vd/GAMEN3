using System.IO;

public interface ISaveable {

	void Save(BinaryWriter w);
	void Load(BinaryReader r);

}
