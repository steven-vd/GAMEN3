using UnityEngine;

public class TerrainGenerator : MonoBehaviour {

	[Range(1, 40)]
	public float Complexity = 4;
	[Range(1, 20)]
	public int IterCount = 8;
	[Range(1.1f, 10.0f)]
	public float IterImpactFalloff = 4;
	[Range(1.1f, 10.0f)]
	public float IterComplexityIncrease = 4;

	void Start() {
		Terrain t = GetComponent<Terrain>();
		GenHeights(t.terrainData, Random.Range(0, int.MaxValue / 1000));
		Paint(t.terrainData);
	}

	public void GenHeights(TerrainData td, int seed = 0) {
		int width = td.alphamapWidth;
		int height = td.alphamapHeight;

		var heights = new float[width, height];
		float complexity = Complexity;
		for (int i = 2; i < IterCount + 2; i++) {
			for (int x = 0; x < width; x++) {
				for (int y = 0; y < height; y++) {
					heights[x, y] += Mathf.PerlinNoise(
						(seed + x) * complexity * IterComplexityIncrease * i / width,
						y * complexity * IterComplexityIncrease * i / height
					) / i;
				}
			}
			complexity /= IterImpactFalloff;
		}

		float lowest = heights[0, 0];
		foreach (float h in heights) {
			lowest = Mathf.Min(lowest, h);
		}
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				heights[x, y] -= lowest;
			}
		}
		td.SetHeights(0, 0, heights);
	}

	public void Paint(TerrainData td) {
		var map = new float[td.alphamapWidth, td.alphamapHeight, 2];
		for (int y = 0; y < td.alphamapHeight; y++) {
			for (int x = 0; x < td.alphamapWidth; x++) {
				float normX = x * 1.0f / (td.alphamapWidth - 1);
				float normY = y * 1.0f / (td.alphamapHeight - 1);

				if (td.GetHeight(x, y) > 30) {
					map[y, x, 0] = 1;
					map[y, x, 1] = 0;
				} else {
					map[y, x, 0] = 0;
					map[y, x, 1] = 1;
				}
			}
		}
		td.SetAlphamaps(0, 0, map);
	}

}
