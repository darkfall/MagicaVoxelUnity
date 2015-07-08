using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

[System.Serializable]
public struct MVFaceCollection
{
	public byte[,,] colorIndices;
}

[System.Serializable]
public struct MVVoxel {
	public byte x, y, z, colorIndex;
}

[System.Serializable]
public class MVVoxelChunk
{
	// all voxels
	public byte[,,] voxels;

	// 6 dir, x+. x-, y+, y-, z+, z-
	public MVFaceCollection[] faces;

	public int x = 0, y = 0, z = 0;

	public int sizeX { get { return voxels.GetLength (0); } }
	public int sizeY { get { return voxels.GetLength (1); } }
	public int sizeZ { get { return voxels.GetLength (2); } }
}

public enum MVFaceDir
{
	XPos = 0,
	XNeg = 1,
	YPos = 2,
	YNeg = 3,
	ZPos = 4,
	ZNeg = 5
}
	
[System.Serializable]
public class MVMainChunk
{
	public MVVoxelChunk voxelChunk;

	public Color[] palatte;

	public int sizeX, sizeY, sizeZ;

	public byte[] version;

#region default_palatte
	public static Color[] defaultPalatte = new Color[] {
		new Color(1.000000f, 1.000000f, 1.000000f),
		new Color(1.000000f, 1.000000f, 0.800000f),
		new Color(1.000000f, 1.000000f, 0.600000f),
		new Color(1.000000f, 1.000000f, 0.400000f),
		new Color(1.000000f, 1.000000f, 0.200000f),
		new Color(1.000000f, 1.000000f, 0.000000f),
		new Color(1.000000f, 0.800000f, 1.000000f),
		new Color(1.000000f, 0.800000f, 0.800000f),
		new Color(1.000000f, 0.800000f, 0.600000f),
		new Color(1.000000f, 0.800000f, 0.400000f),
		new Color(1.000000f, 0.800000f, 0.200000f),
		new Color(1.000000f, 0.800000f, 0.000000f),
		new Color(1.000000f, 0.600000f, 1.000000f),
		new Color(1.000000f, 0.600000f, 0.800000f),
		new Color(1.000000f, 0.600000f, 0.600000f),
		new Color(1.000000f, 0.600000f, 0.400000f),
		new Color(1.000000f, 0.600000f, 0.200000f),
		new Color(1.000000f, 0.600000f, 0.000000f),
		new Color(1.000000f, 0.400000f, 1.000000f),
		new Color(1.000000f, 0.400000f, 0.800000f),
		new Color(1.000000f, 0.400000f, 0.600000f),
		new Color(1.000000f, 0.400000f, 0.400000f),
		new Color(1.000000f, 0.400000f, 0.200000f),
		new Color(1.000000f, 0.400000f, 0.000000f),
		new Color(1.000000f, 0.200000f, 1.000000f),
		new Color(1.000000f, 0.200000f, 0.800000f),
		new Color(1.000000f, 0.200000f, 0.600000f),
		new Color(1.000000f, 0.200000f, 0.400000f),
		new Color(1.000000f, 0.200000f, 0.200000f),
		new Color(1.000000f, 0.200000f, 0.000000f),
		new Color(1.000000f, 0.000000f, 1.000000f),
		new Color(1.000000f, 0.000000f, 0.800000f),
		new Color(1.000000f, 0.000000f, 0.600000f),
		new Color(1.000000f, 0.000000f, 0.400000f),
		new Color(1.000000f, 0.000000f, 0.200000f),
		new Color(1.000000f, 0.000000f, 0.000000f),
		new Color(0.800000f, 1.000000f, 1.000000f),
		new Color(0.800000f, 1.000000f, 0.800000f),
		new Color(0.800000f, 1.000000f, 0.600000f),
		new Color(0.800000f, 1.000000f, 0.400000f),
		new Color(0.800000f, 1.000000f, 0.200000f),
		new Color(0.800000f, 1.000000f, 0.000000f),
		new Color(0.800000f, 0.800000f, 1.000000f),
		new Color(0.800000f, 0.800000f, 0.800000f),
		new Color(0.800000f, 0.800000f, 0.600000f),
		new Color(0.800000f, 0.800000f, 0.400000f),
		new Color(0.800000f, 0.800000f, 0.200000f),
		new Color(0.800000f, 0.800000f, 0.000000f),
		new Color(0.800000f, 0.600000f, 1.000000f),
		new Color(0.800000f, 0.600000f, 0.800000f),
		new Color(0.800000f, 0.600000f, 0.600000f),
		new Color(0.800000f, 0.600000f, 0.400000f),
		new Color(0.800000f, 0.600000f, 0.200000f),
		new Color(0.800000f, 0.600000f, 0.000000f),
		new Color(0.800000f, 0.400000f, 1.000000f),
		new Color(0.800000f, 0.400000f, 0.800000f),
		new Color(0.800000f, 0.400000f, 0.600000f),
		new Color(0.800000f, 0.400000f, 0.400000f),
		new Color(0.800000f, 0.400000f, 0.200000f),
		new Color(0.800000f, 0.400000f, 0.000000f),
		new Color(0.800000f, 0.200000f, 1.000000f),
		new Color(0.800000f, 0.200000f, 0.800000f),
		new Color(0.800000f, 0.200000f, 0.600000f),
		new Color(0.800000f, 0.200000f, 0.400000f),
		new Color(0.800000f, 0.200000f, 0.200000f),
		new Color(0.800000f, 0.200000f, 0.000000f),
		new Color(0.800000f, 0.000000f, 1.000000f),
		new Color(0.800000f, 0.000000f, 0.800000f),
		new Color(0.800000f, 0.000000f, 0.600000f),
		new Color(0.800000f, 0.000000f, 0.400000f),
		new Color(0.800000f, 0.000000f, 0.200000f),
		new Color(0.800000f, 0.000000f, 0.000000f),
		new Color(0.600000f, 1.000000f, 1.000000f),
		new Color(0.600000f, 1.000000f, 0.800000f),
		new Color(0.600000f, 1.000000f, 0.600000f),
		new Color(0.600000f, 1.000000f, 0.400000f),
		new Color(0.600000f, 1.000000f, 0.200000f),
		new Color(0.600000f, 1.000000f, 0.000000f),
		new Color(0.600000f, 0.800000f, 1.000000f),
		new Color(0.600000f, 0.800000f, 0.800000f),
		new Color(0.600000f, 0.800000f, 0.600000f),
		new Color(0.600000f, 0.800000f, 0.400000f),
		new Color(0.600000f, 0.800000f, 0.200000f),
		new Color(0.600000f, 0.800000f, 0.000000f),
		new Color(0.600000f, 0.600000f, 1.000000f),
		new Color(0.600000f, 0.600000f, 0.800000f),
		new Color(0.600000f, 0.600000f, 0.600000f),
		new Color(0.600000f, 0.600000f, 0.400000f),
		new Color(0.600000f, 0.600000f, 0.200000f),
		new Color(0.600000f, 0.600000f, 0.000000f),
		new Color(0.600000f, 0.400000f, 1.000000f),
		new Color(0.600000f, 0.400000f, 0.800000f),
		new Color(0.600000f, 0.400000f, 0.600000f),
		new Color(0.600000f, 0.400000f, 0.400000f),
		new Color(0.600000f, 0.400000f, 0.200000f),
		new Color(0.600000f, 0.400000f, 0.000000f),
		new Color(0.600000f, 0.200000f, 1.000000f),
		new Color(0.600000f, 0.200000f, 0.800000f),
		new Color(0.600000f, 0.200000f, 0.600000f),
		new Color(0.600000f, 0.200000f, 0.400000f),
		new Color(0.600000f, 0.200000f, 0.200000f),
		new Color(0.600000f, 0.200000f, 0.000000f),
		new Color(0.600000f, 0.000000f, 1.000000f),
		new Color(0.600000f, 0.000000f, 0.800000f),
		new Color(0.600000f, 0.000000f, 0.600000f),
		new Color(0.600000f, 0.000000f, 0.400000f),
		new Color(0.600000f, 0.000000f, 0.200000f),
		new Color(0.600000f, 0.000000f, 0.000000f),
		new Color(0.400000f, 1.000000f, 1.000000f),
		new Color(0.400000f, 1.000000f, 0.800000f),
		new Color(0.400000f, 1.000000f, 0.600000f),
		new Color(0.400000f, 1.000000f, 0.400000f),
		new Color(0.400000f, 1.000000f, 0.200000f),
		new Color(0.400000f, 1.000000f, 0.000000f),
		new Color(0.400000f, 0.800000f, 1.000000f),
		new Color(0.400000f, 0.800000f, 0.800000f),
		new Color(0.400000f, 0.800000f, 0.600000f),
		new Color(0.400000f, 0.800000f, 0.400000f),
		new Color(0.400000f, 0.800000f, 0.200000f),
		new Color(0.400000f, 0.800000f, 0.000000f),
		new Color(0.400000f, 0.600000f, 1.000000f),
		new Color(0.400000f, 0.600000f, 0.800000f),
		new Color(0.400000f, 0.600000f, 0.600000f),
		new Color(0.400000f, 0.600000f, 0.400000f),
		new Color(0.400000f, 0.600000f, 0.200000f),
		new Color(0.400000f, 0.600000f, 0.000000f),
		new Color(0.400000f, 0.400000f, 1.000000f),
		new Color(0.400000f, 0.400000f, 0.800000f),
		new Color(0.400000f, 0.400000f, 0.600000f),
		new Color(0.400000f, 0.400000f, 0.400000f),
		new Color(0.400000f, 0.400000f, 0.200000f),
		new Color(0.400000f, 0.400000f, 0.000000f),
		new Color(0.400000f, 0.200000f, 1.000000f),
		new Color(0.400000f, 0.200000f, 0.800000f),
		new Color(0.400000f, 0.200000f, 0.600000f),
		new Color(0.400000f, 0.200000f, 0.400000f),
		new Color(0.400000f, 0.200000f, 0.200000f),
		new Color(0.400000f, 0.200000f, 0.000000f),
		new Color(0.400000f, 0.000000f, 1.000000f),
		new Color(0.400000f, 0.000000f, 0.800000f),
		new Color(0.400000f, 0.000000f, 0.600000f),
		new Color(0.400000f, 0.000000f, 0.400000f),
		new Color(0.400000f, 0.000000f, 0.200000f),
		new Color(0.400000f, 0.000000f, 0.000000f),
		new Color(0.200000f, 1.000000f, 1.000000f),
		new Color(0.200000f, 1.000000f, 0.800000f),
		new Color(0.200000f, 1.000000f, 0.600000f),
		new Color(0.200000f, 1.000000f, 0.400000f),
		new Color(0.200000f, 1.000000f, 0.200000f),
		new Color(0.200000f, 1.000000f, 0.000000f),
		new Color(0.200000f, 0.800000f, 1.000000f),
		new Color(0.200000f, 0.800000f, 0.800000f),
		new Color(0.200000f, 0.800000f, 0.600000f),
		new Color(0.200000f, 0.800000f, 0.400000f),
		new Color(0.200000f, 0.800000f, 0.200000f),
		new Color(0.200000f, 0.800000f, 0.000000f),
		new Color(0.200000f, 0.600000f, 1.000000f),
		new Color(0.200000f, 0.600000f, 0.800000f),
		new Color(0.200000f, 0.600000f, 0.600000f),
		new Color(0.200000f, 0.600000f, 0.400000f),
		new Color(0.200000f, 0.600000f, 0.200000f),
		new Color(0.200000f, 0.600000f, 0.000000f),
		new Color(0.200000f, 0.400000f, 1.000000f),
		new Color(0.200000f, 0.400000f, 0.800000f),
		new Color(0.200000f, 0.400000f, 0.600000f),
		new Color(0.200000f, 0.400000f, 0.400000f),
		new Color(0.200000f, 0.400000f, 0.200000f),
		new Color(0.200000f, 0.400000f, 0.000000f),
		new Color(0.200000f, 0.200000f, 1.000000f),
		new Color(0.200000f, 0.200000f, 0.800000f),
		new Color(0.200000f, 0.200000f, 0.600000f),
		new Color(0.200000f, 0.200000f, 0.400000f),
		new Color(0.200000f, 0.200000f, 0.200000f),
		new Color(0.200000f, 0.200000f, 0.000000f),
		new Color(0.200000f, 0.000000f, 1.000000f),
		new Color(0.200000f, 0.000000f, 0.800000f),
		new Color(0.200000f, 0.000000f, 0.600000f),
		new Color(0.200000f, 0.000000f, 0.400000f),
		new Color(0.200000f, 0.000000f, 0.200000f),
		new Color(0.200000f, 0.000000f, 0.000000f),
		new Color(0.000000f, 1.000000f, 1.000000f),
		new Color(0.000000f, 1.000000f, 0.800000f),
		new Color(0.000000f, 1.000000f, 0.600000f),
		new Color(0.000000f, 1.000000f, 0.400000f),
		new Color(0.000000f, 1.000000f, 0.200000f),
		new Color(0.000000f, 1.000000f, 0.000000f),
		new Color(0.000000f, 0.800000f, 1.000000f),
		new Color(0.000000f, 0.800000f, 0.800000f),
		new Color(0.000000f, 0.800000f, 0.600000f),
		new Color(0.000000f, 0.800000f, 0.400000f),
		new Color(0.000000f, 0.800000f, 0.200000f),
		new Color(0.000000f, 0.800000f, 0.000000f),
		new Color(0.000000f, 0.600000f, 1.000000f),
		new Color(0.000000f, 0.600000f, 0.800000f),
		new Color(0.000000f, 0.600000f, 0.600000f),
		new Color(0.000000f, 0.600000f, 0.400000f),
		new Color(0.000000f, 0.600000f, 0.200000f),
		new Color(0.000000f, 0.600000f, 0.000000f),
		new Color(0.000000f, 0.400000f, 1.000000f),
		new Color(0.000000f, 0.400000f, 0.800000f),
		new Color(0.000000f, 0.400000f, 0.600000f),
		new Color(0.000000f, 0.400000f, 0.400000f),
		new Color(0.000000f, 0.400000f, 0.200000f),
		new Color(0.000000f, 0.400000f, 0.000000f),
		new Color(0.000000f, 0.200000f, 1.000000f),
		new Color(0.000000f, 0.200000f, 0.800000f),
		new Color(0.000000f, 0.200000f, 0.600000f),
		new Color(0.000000f, 0.200000f, 0.400000f),
		new Color(0.000000f, 0.200000f, 0.200000f),
		new Color(0.000000f, 0.200000f, 0.000000f),
		new Color(0.000000f, 0.000000f, 1.000000f),
		new Color(0.000000f, 0.000000f, 0.800000f),
		new Color(0.000000f, 0.000000f, 0.600000f),
		new Color(0.000000f, 0.000000f, 0.400000f),
		new Color(0.000000f, 0.000000f, 0.200000f),
		new Color(0.933333f, 0.000000f, 0.000000f),
		new Color(0.866667f, 0.000000f, 0.000000f),
		new Color(0.733333f, 0.000000f, 0.000000f),
		new Color(0.666667f, 0.000000f, 0.000000f),
		new Color(0.533333f, 0.000000f, 0.000000f),
		new Color(0.466667f, 0.000000f, 0.000000f),
		new Color(0.333333f, 0.000000f, 0.000000f),
		new Color(0.266667f, 0.000000f, 0.000000f),
		new Color(0.133333f, 0.000000f, 0.000000f),
		new Color(0.066667f, 0.000000f, 0.000000f),
		new Color(0.000000f, 0.933333f, 0.000000f),
		new Color(0.000000f, 0.866667f, 0.000000f),
		new Color(0.000000f, 0.733333f, 0.000000f),
		new Color(0.000000f, 0.666667f, 0.000000f),
		new Color(0.000000f, 0.533333f, 0.000000f),
		new Color(0.000000f, 0.466667f, 0.000000f),
		new Color(0.000000f, 0.333333f, 0.000000f),
		new Color(0.000000f, 0.266667f, 0.000000f),
		new Color(0.000000f, 0.133333f, 0.000000f),
		new Color(0.000000f, 0.066667f, 0.000000f),
		new Color(0.000000f, 0.000000f, 0.933333f),
		new Color(0.000000f, 0.000000f, 0.866667f),
		new Color(0.000000f, 0.000000f, 0.733333f),
		new Color(0.000000f, 0.000000f, 0.666667f),
		new Color(0.000000f, 0.000000f, 0.533333f),
		new Color(0.000000f, 0.000000f, 0.466667f),
		new Color(0.000000f, 0.000000f, 0.333333f),
		new Color(0.000000f, 0.000000f, 0.266667f),
		new Color(0.000000f, 0.000000f, 0.133333f),
		new Color(0.000000f, 0.000000f, 0.066667f),
		new Color(0.933333f, 0.933333f, 0.933333f),
		new Color(0.866667f, 0.866667f, 0.866667f),
		new Color(0.733333f, 0.733333f, 0.733333f),
		new Color(0.666667f, 0.666667f, 0.666667f),
		new Color(0.533333f, 0.533333f, 0.533333f),
		new Color(0.466667f, 0.466667f, 0.466667f),
		new Color(0.333333f, 0.333333f, 0.333333f),
		new Color(0.266667f, 0.266667f, 0.266667f),
		new Color(0.133333f, 0.133333f, 0.133333f),
		new Color(0.066667f, 0.066667f, 0.066667f),
		new Color(0.000000f, 0.000000f, 0.000000f)
	};
#endregion
}

public static class MVImporter 
{
	public static Material DefaultMaterial {
		get {
			return new Material (Shader.Find ("MagicaVoxel/FlatColor"));
		}
	}

	public static MVMainChunk LoadVOXFromData(byte[] data) {
		using (MemoryStream ms = new MemoryStream (data)) {
			using (BinaryReader br = new BinaryReader (ms)) {
				MVMainChunk mainChunk = new MVMainChunk ();

				// "VOX "
				br.ReadInt32 ();
				// "VERSION "
				mainChunk.version = br.ReadBytes(4);

				byte[] chunkId = br.ReadBytes (4);
				if (chunkId [0] != 'M' ||
					chunkId [1] != 'A' ||
					chunkId [2] != 'I' ||
					chunkId [3] != 'N') {
					Debug.LogError ("[MVImport] Invalid MainChunk ID, main chunk expected");
					return null;
				}

				int chunkSize = br.ReadInt32 ();
				int childrenSize = br.ReadInt32 ();

				// main chunk should have nothing... skip
				br.ReadBytes (chunkSize); 

				int readSize = 0;
				while (readSize < childrenSize) {
					chunkId = br.ReadBytes (4);
					if (chunkId [0] == 'S' &&
						chunkId [1] == 'I' &&
						chunkId [2] == 'Z' &&
						chunkId [3] == 'E') {

						readSize += ReadSizeChunk (br, mainChunk);

					} else if (chunkId [0] == 'X' &&
						chunkId [1] == 'Y' &&
						chunkId [2] == 'Z' &&
						chunkId [3] == 'I') {

						readSize += ReadVoxelChunk (br, mainChunk.voxelChunk);

					} else if (chunkId [0] == 'R' &&
						chunkId [1] == 'G' &&
						chunkId [2] == 'B' &&
						chunkId [3] == 'A') {

						mainChunk.palatte = new Color[256];
						readSize += ReadPalattee (br, mainChunk.palatte);

					}
					else {
						Debug.LogError ("[MVImport] Chunk ID not recognized, got " + System.Text.Encoding.ASCII.GetString(chunkId));
						return null;
					}
				}

				GenerateFaces (mainChunk.voxelChunk);
				if (mainChunk.palatte == null)
					mainChunk.palatte = MVMainChunk.defaultPalatte;

				return mainChunk;
			}
		}
	}

	public static MVMainChunk LoadVOX(string path)
	{
		byte[] bytes = File.ReadAllBytes (path);
		if (bytes [0] != 'V' ||
			bytes [1] != 'O' ||
			bytes [2] != 'X' ||
			bytes [3] != ' ') {
			Debug.LogError ("Invalid VOX file, magic number mismatch");
			return null;
		}

		return LoadVOXFromData (bytes);
	}

	public static void GenerateFaces(MVVoxelChunk voxelChunk)
	{
		voxelChunk.faces = new MVFaceCollection[6];
		for (int i = 0; i < 6; ++i) {
			voxelChunk.faces [i].colorIndices = new byte[voxelChunk.sizeX, voxelChunk.sizeY, voxelChunk.sizeZ];
		}

		for (int x = 0; x < voxelChunk.sizeX; ++x) {
			for (int y = 0; y < voxelChunk.sizeY; ++y) {
				for (int z = 0; z < voxelChunk.sizeZ; ++z) {
					// left right
					if(x == 0 || voxelChunk.voxels[x-1, y, z] == 0)
						voxelChunk.faces [(int)MVFaceDir.XNeg].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];	

					if (x == voxelChunk.sizeX - 1 || voxelChunk.voxels [x + 1, y, z] == 0)
						voxelChunk.faces [(int)MVFaceDir.XPos].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];

					// up down
					if(y == 0 || voxelChunk.voxels[x, y-1, z] == 0)
						voxelChunk.faces [(int)MVFaceDir.YNeg].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];	

					if (y == voxelChunk.sizeY - 1 || voxelChunk.voxels [x, y+1, z] == 0)
						voxelChunk.faces [(int)MVFaceDir.YPos].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];

					// forward backward
					if(z == 0 || voxelChunk.voxels[x, y, z-1] == 0)
						voxelChunk.faces [(int)MVFaceDir.ZNeg].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];	

					if (z == voxelChunk.sizeZ - 1 || voxelChunk.voxels [x, y, z+1] == 0)
						voxelChunk.faces [(int)MVFaceDir.ZPos].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];
				}
			}
		}
	}

	static int ReadSizeChunk(BinaryReader br, MVMainChunk mainChunk)
	{
		int chunkSize = br.ReadInt32 ();
		int childrenSize = br.ReadInt32 ();

		mainChunk.sizeX = br.ReadInt32 ();
		mainChunk.sizeZ = br.ReadInt32 ();
		mainChunk.sizeY = br.ReadInt32 ();

		mainChunk.voxelChunk = new MVVoxelChunk ();
		mainChunk.voxelChunk.voxels = new byte[mainChunk.sizeX, mainChunk.sizeY, mainChunk.sizeZ];

		Debug.Log (string.Format ("[MVImporter] Voxel Size {0}x{1}x{2}", mainChunk.sizeX, mainChunk.sizeY, mainChunk.sizeZ));

		if (childrenSize > 0) {
			br.ReadBytes (childrenSize);
			Debug.LogWarning ("[MVImporter] Nested chunk not supported");
		}

		return chunkSize + childrenSize + 4 * 3;
	}

	static int ReadVoxelChunk(BinaryReader br, MVVoxelChunk chunk)
	{
		int chunkSize = br.ReadInt32 ();
		int childrenSize = br.ReadInt32 ();
		int numVoxels = br.ReadInt32 ();

		for (int i = 0; i < numVoxels; ++i) {
			int x = (int)br.ReadByte ();
			int z = (int)br.ReadByte ();
			int y = (int)br.ReadByte ();

			chunk.voxels [x, y, z] = br.ReadByte();
		}

		if (childrenSize > 0) {
			br.ReadBytes (childrenSize);
			Debug.LogWarning ("[MVImporter] Nested chunk not supported");
		}

		return chunkSize + childrenSize + 4 * 3;
	}

	static int ReadPalattee(BinaryReader br, Color[] colors)
	{
		int chunkSize = br.ReadInt32 ();
		int childrenSize = br.ReadInt32 ();

		for (int i = 0; i < 256; ++i) {
			colors [i] = new Color ((float)br.ReadByte () / 255.0f, (float)br.ReadByte () / 255.0f, (float)br.ReadByte () / 255.0f, (float)br.ReadByte () / 255.0f);
		}

		if (childrenSize > 0) {
			br.ReadBytes (childrenSize);
			Debug.LogWarning ("[MVImporter] Nested chunk not supported");
		}

		return chunkSize + childrenSize + 4 * 3;
	}

	public static Mesh[] CreateMeshes(MVMainChunk chunk, float sizePerVox)
	{
		return CreateMeshesFromChunk (chunk.voxelChunk, chunk.palatte, sizePerVox);
	}

	public static GameObject[] CreateVoxelGameObjects(MVMainChunk chunk, Transform parent, Material mat, float sizePerVox)
	{
		return CreateVoxelGameObjectsForChunk (chunk.voxelChunk, chunk.palatte, parent, mat, sizePerVox);
	}

	public static GameObject[] CreateVoxelGameObjects(MVMainChunk chunk, Transform parent, Material mat, float sizePerVox, Vector3 origin)
	{
		return CreateVoxelGameObjectsForChunk(chunk.voxelChunk, chunk.palatte, parent, mat, sizePerVox, origin);
	}

	public static GameObject[] CreateIndividualVoxelGameObjects(MVMainChunk chunk, Transform parent, Material mat, float sizePerVox)
	{
		return CreateIndividualVoxelGameObjectsForChunk (chunk.voxelChunk, chunk.palatte, parent, mat, sizePerVox);
	}

	public static GameObject[] CreateIndividualVoxelGameObjects(MVMainChunk chunk, Transform parent, Material mat, float sizePerVox, Vector3 origin)
	{
		return CreateIndividualVoxelGameObjectsForChunk(chunk.voxelChunk, chunk.palatte, parent, mat, sizePerVox, origin);
	}

	public static Mesh CubeMeshWithColor(float size, Color c) {
		float halfSize = size / 2;

		Vector3[] verts = new Vector3[] {
			new Vector3 (-halfSize, -halfSize, -halfSize),
			new Vector3 (-halfSize, halfSize, -halfSize),
			new Vector3 (halfSize, halfSize, -halfSize),
			new Vector3 (halfSize, -halfSize, -halfSize),
			new Vector3 (halfSize, -halfSize, halfSize),
			new Vector3 (halfSize, halfSize, halfSize),
			new Vector3 (-halfSize, halfSize, halfSize),
			new Vector3 (-halfSize, -halfSize, halfSize)
		};

		int[] indicies = new int[] {
			0, 1, 2, //   1
			0, 2, 3,
			3, 2, 5, //   2
			3, 5, 4,
			5, 2, 1, //   3
			5, 1, 6,
			3, 4, 7, //   4
			3, 7, 0,
			0, 7, 6, //   5
			0, 6, 1,
			4, 5, 6, //   6
			4, 6, 7
		};

		Color[] colors = new Color[] {
			c,
			c,
			c,
			c,
			c,
			c,
			c,
			c
		};

		Mesh mesh = new Mesh ();
		mesh.vertices = verts;
		mesh.colors = colors;
		mesh.triangles = indicies;
		mesh.RecalculateNormals ();	
		return mesh;
	}


	public static GameObject CreateGameObject(Transform parent, Vector3 pos, string name, Mesh mesh, Material mat) {
		GameObject go = new GameObject ();
		go.name = name;
		go.transform.SetParent (parent);
		go.transform.localPosition = pos;

		MeshFilter mf = go.AddComponent<MeshFilter> ();
		mf.mesh = mesh;

		MeshRenderer mr = go.AddComponent<MeshRenderer> ();
		mr.material = mat;

		return go;
	}

	public static GameObject[] CreateIndividualVoxelGameObjectsForChunk(MVVoxelChunk chunk, Color[] palatte, Transform parent, Material mat, float sizePerVox)
	{
		Vector3 origin = new Vector3(
			sizePerVox * chunk.sizeX / 2,
			sizePerVox * chunk.sizeY / 2,
			sizePerVox * chunk.sizeZ / 2);

		return CreateIndividualVoxelGameObjectsForChunk(chunk, palatte, parent, mat, sizePerVox, origin);
	}

	public static GameObject[] CreateIndividualVoxelGameObjectsForChunk(MVVoxelChunk chunk, Color[] palatte, Transform parent, Material mat, float sizePerVox, Vector3 origin) {
		List<GameObject> result = new List<GameObject> ();

		for (int x = 0; x < chunk.sizeX; ++x) {
			for (int y = 0; y < chunk.sizeY; ++y) {
				for (int z = 0; z < chunk.sizeZ; ++z) {

					if (chunk.voxels [x, y, z] != 0) {
						float px = (x - origin.x + 0.5f) * sizePerVox, py = (y - origin.y + 0.5f) * sizePerVox, pz = (z - origin.z + 0.5f) * sizePerVox;

						GameObject go = CreateGameObject (
							parent, 
							new Vector3 (px, py, pz),
							string.Format ("Voxel ({0}, {1}, {2})", x, y, z),
							MVImporter.CubeMeshWithColor (sizePerVox, palatte [chunk.voxels [x, y, z] - 1]),
							mat);

						MVVoxModelVoxel v = go.AddComponent<MVVoxModelVoxel> ();
						v.voxel = new MVVoxel () { x = (byte)x, y = (byte)y, z = (byte)z, colorIndex = chunk.voxels [x, y, z] };

						result.Add (go);
					}
				}
			}
		}

		return result.ToArray();
	}

	public static GameObject[] CreateVoxelGameObjectsForChunk(MVVoxelChunk chunk, Color[] palatte, Transform parent, Material mat, float sizePerVox)
	{
		Vector3 origin = new Vector3(
			sizePerVox * chunk.sizeX / 2,
			sizePerVox * chunk.sizeY / 2,
			sizePerVox * chunk.sizeZ / 2);

		return CreateVoxelGameObjectsForChunk(chunk, palatte, parent, mat, sizePerVox, origin);
	}

	public static GameObject[] CreateVoxelGameObjectsForChunk(MVVoxelChunk chunk, Color[] palatte, Transform parent, Material mat, float sizePerVox, Vector3 origin) {
		List<GameObject> result = new List<GameObject> ();

		Mesh[] meshes = CreateMeshesFromChunk (chunk, palatte, sizePerVox, origin);

		int index = 0;
		foreach (Mesh mesh in meshes) {
			GameObject go = new GameObject ();
			go.name = string.Format ("VoxelMesh ({0})", index);
			go.transform.SetParent (parent);
			go.transform.localPosition = Vector3.zero;

			MeshFilter mf = go.AddComponent<MeshFilter> ();
			mf.mesh = mesh;

			MeshRenderer mr = go.AddComponent<MeshRenderer> ();
			mr.material = mat;

			go.AddComponent<MVVoxModelMesh> ();
			result.Add (go);
			index++;
		}

		return result.ToArray ();
	}

	public static Mesh[] CreateMeshesFromChunk(MVVoxelChunk chunk, Color[] palatte, float sizePerVox)
	{
		Vector3 origin = new Vector3(
			sizePerVox * chunk.sizeX / 2,
			sizePerVox * chunk.sizeY / 2,
			sizePerVox * chunk.sizeZ / 2);

		return CreateMeshesFromChunk(chunk, palatte, sizePerVox, origin);
	}

	public static Mesh[] CreateMeshesFromChunk(MVVoxelChunk chunk, Color[] palatte, float sizePerVox, Vector3 origin)
	{
		List<Vector3> verts = new List<Vector3> ();
		List<Vector3> normals = new List<Vector3> ();
		List<Color> colors = new List<Color> ();
		List<int> indicies = new List<int> ();

		Vector3[] faceNormals = new Vector3[] {
			Vector3.right,
			Vector3.left,
			Vector3.up,
			Vector3.down,
			Vector3.forward,
			Vector3.back
		};

		List<Mesh> result = new List<Mesh> ();

		if (sizePerVox <= 0.0f)
			sizePerVox = 0.1f;
		
		float halfSize = sizePerVox / 2.0f;

		int currentQuadCount = 0;
		int totalQuadCount = 0;
		for (int f = 0; f < 6; ++f) {
			for (int x = 0; x < chunk.sizeX; ++x) {
				for (int y = 0; y < chunk.sizeY; ++y) {
					for (int z = 0; z < chunk.sizeZ; ++z) {

						int cidx = chunk.faces [f].colorIndices [x, y, z];

						if (cidx != 0) {
							float px = (x - origin.x + 0.5f) * sizePerVox, py = (y - origin.y + 0.5f) * sizePerVox, pz = (z - origin.z + 0.5f) * sizePerVox;

							int rx = x, ry = y, rz = z;
							switch (f) {
							case 1:
							case 0:
								{
									ry = y + 1;
									while (ry < chunk.sizeY && chunk.faces [f].colorIndices [x, ry, z] == cidx)
										ry++;
									ry--;

									rz = z + 1;
									while (rz < chunk.sizeZ) {
										bool inc = true;
										for (int k = y; k <= ry; ++k) {
											inc = inc & (chunk.faces [f].colorIndices [x, k, rz] == cidx);
										}

										if (inc)
											rz++;
										else
											break;
									}
									rz--;
									break;
								}

							case 3:
							case 2:
								{
									rx = x + 1;
									while (rx < chunk.sizeX && chunk.faces [f].colorIndices [rx, y, z] == cidx)
										rx++;
									rx--;

									rz = z + 1;
									while (rz < chunk.sizeZ) {
										bool inc = true;
										for (int k = x; k <= rx; ++k) {
											inc = inc & (chunk.faces [f].colorIndices [k, y, rz] == cidx);
										}

										if (inc)
											rz++;
										else
											break;
									}
									rz--;
									break;
								}

							case 5:
							case 4:
								{
									rx = x + 1;
									while (rx < chunk.sizeX && chunk.faces [f].colorIndices [rx, y, z] == cidx)
										rx++;
									rx--;

									ry = y + 1;
									while (ry < chunk.sizeY) {
										bool inc = true;
										for (int k = x; k <= rx; ++k) {
											inc = inc & (chunk.faces [f].colorIndices [k, ry, z] == cidx);
										}

										if (inc)
											ry++;
										else
											break;
									}
									ry--;
									break;
								}
							}


							for (int kx = x; kx <= rx; ++kx) {
								for (int ky = y; ky <= ry; ++ky) {
									for (int kz = z; kz <= rz; ++kz) {
										if(kx != x || ky != y || kz != z)
											chunk.faces [f].colorIndices [kx, ky, kz] = 0;
									}
								}
							}

							int dx = rx - x;
							int dy = ry - y;
							int dz = rz - z;

							switch (f) {
							case 1:
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz + halfSize + sizePerVox * dz));
								verts.Add (new Vector3 (px - halfSize, py + halfSize + sizePerVox * dy, pz + halfSize + sizePerVox * dz));
								verts.Add (new Vector3 (px - halfSize, py + halfSize + sizePerVox * dy, pz - halfSize));
								break;

							case 0:
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize, py + halfSize + sizePerVox * dy, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize, py + halfSize + sizePerVox * dy, pz + halfSize + sizePerVox * dz));
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz + halfSize + sizePerVox * dz));
								break;

							case 3:
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py - halfSize, pz + halfSize + sizePerVox * dz));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz + halfSize + sizePerVox * dz));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz - halfSize));
								break;

							case 2:
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py + halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz + halfSize + sizePerVox * dz));
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py + halfSize, pz + halfSize + sizePerVox * dz));
								break;

							case 5:
								verts.Add (new Vector3 (px - halfSize, py + halfSize + sizePerVox * dy, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py + halfSize + sizePerVox * dy, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz - halfSize));
								break;

							case 4:
								verts.Add (new Vector3 (px - halfSize, py + halfSize + sizePerVox * dy, pz + halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz + halfSize));
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py - halfSize, pz + halfSize));
								verts.Add (new Vector3 (px + halfSize + sizePerVox * dx, py + halfSize + sizePerVox * dy, pz + halfSize));
								break;
							}

							normals.Add (faceNormals [f]);
							normals.Add (faceNormals [f]);
							normals.Add (faceNormals [f]);
							normals.Add (faceNormals [f]);

							// color index starts with 1
							Color c = palatte [cidx - 1];

							colors.Add (c);
							colors.Add (c);
							colors.Add (c);
							colors.Add (c);

							indicies.Add (currentQuadCount * 4 + 0);
							indicies.Add (currentQuadCount * 4 + 1);
							indicies.Add (currentQuadCount * 4 + 2);
							indicies.Add (currentQuadCount * 4 + 2);
							indicies.Add (currentQuadCount * 4 + 3);
							indicies.Add (currentQuadCount * 4 + 0);

							currentQuadCount += 1;

							// u3d max
							if (verts.Count + 4 >= 65000) {
								Mesh mesh = new Mesh ();
								mesh.vertices = verts.ToArray();
								mesh.colors = colors.ToArray();
								mesh.normals = normals.ToArray();
								mesh.triangles = indicies.ToArray ();
								mesh.Optimize ();
								result.Add (mesh);

								verts.Clear ();
								colors.Clear ();
								normals.Clear ();
								indicies.Clear ();

								totalQuadCount += currentQuadCount;
								currentQuadCount = 0;
							}
						}
					}
				}
			}
		}

		if (verts.Count > 0) {
			Mesh mesh = new Mesh ();
			mesh.vertices = verts.ToArray();
			mesh.colors = colors.ToArray();
			mesh.normals = normals.ToArray();
			mesh.triangles = indicies.ToArray ();
			mesh.Optimize ();
			result.Add (mesh);

			totalQuadCount += currentQuadCount;
		}

		Debug.Log (string.Format ("[MVImport] Mesh generated, total quads {0}", totalQuadCount));

		return result.ToArray();
	}
}

