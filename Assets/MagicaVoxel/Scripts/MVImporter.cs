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

	public static MVMainChunk LoadVOX(string path)
	{
		byte[] bytes = File.ReadAllBytes (path);
		if (bytes [0] != 'V' ||
		   bytes [1] != 'O' ||
		   bytes [2] != 'X' ||
		   bytes [3] != ' ') {
			throw new FileLoadException ("Invalid VOX file, magic number mismatch");
		}

		using (MemoryStream ms = new MemoryStream (bytes)) {
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
					throw new FileLoadException ("Invalid MainChunk ID, main chunk expected");
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
						throw new FileLoadException ("Chunk ID not recognized, got " + System.Text.Encoding.ASCII.GetString(chunkId));
					}
				}

				GenerateFaces (mainChunk.voxelChunk);
				if (mainChunk.palatte == null)
					mainChunk.palatte = MVMainChunk.defaultPalatte;

				return mainChunk;
			}
		}
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
					if(x == 0 || voxelChunk.voxels[x-1,y,z] == 0)
						voxelChunk.faces [(int)MVFaceDir.XNeg].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];	

					if (x == voxelChunk.sizeX - 1 || voxelChunk.voxels [x + 1, y, z] == 0)
						voxelChunk.faces [(int)MVFaceDir.XPos].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];

					// up down
					if(y == 0 || voxelChunk.voxels[x,y-1,z] == 0)
						voxelChunk.faces [(int)MVFaceDir.YNeg].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];	

					if (y == voxelChunk.sizeY - 1 || voxelChunk.voxels [x, y+1, z] == 0)
						voxelChunk.faces [(int)MVFaceDir.YPos].colorIndices [x, y, z] = voxelChunk.voxels [x, y, z];

					// forward backward
					if(z == 0 || voxelChunk.voxels[x,y,z-1] == 0)
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
		mainChunk.sizeY = br.ReadInt32 ();
		mainChunk.sizeZ = br.ReadInt32 ();

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

	public static GameObject[] CreateVoxelGameObjectsForChunk(MVVoxelChunk chunk, Color[] palatte, Transform parent, Material mat, float sizePerVox) {
		List<GameObject> result = new List<GameObject> ();

		float cx = sizePerVox * chunk.sizeX / 2;
		float cy = sizePerVox * chunk.sizeY / 2;
		float cz = sizePerVox * chunk.sizeZ / 2;

		for (int x = 0; x < chunk.sizeX; ++x) {
			for (int y = 0; y < chunk.sizeY; ++y) {
				for (int z = 0; z < chunk.sizeZ; ++z) {

					if (chunk.voxels [x, y, z] != 0) {
						float px = x * sizePerVox - cx, py = y * sizePerVox - cy, pz = z * sizePerVox - cz;

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

	public static Mesh[] CreateMeshesFromChunk(MVVoxelChunk chunk, Color[] palatte, float sizePerVox)
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
		float cx = sizePerVox * chunk.sizeX / 2;
		float cy = sizePerVox * chunk.sizeY / 2;
		float cz = sizePerVox * chunk.sizeZ / 2;

		int totalQuadCount = 0;
		for (int f = 0; f < 6; ++f) {
			for (int x = 0; x < chunk.sizeX; ++x) {
				for (int y = 0; y < chunk.sizeY; ++y) {
					for (int z = 0; z < chunk.sizeZ; ++z) {
						// left
						if (chunk.faces [f].colorIndices [x, y, z] != 0) {
							float px = x * sizePerVox - cx, py = y * sizePerVox - cy, pz = z * sizePerVox - cz;

							switch (f) {
							case 1:
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz + halfSize));
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz + halfSize));
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz - halfSize));
								break;

							case 0:
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize, py + halfSize, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize, py + halfSize, pz + halfSize));
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz + halfSize));
								break;

							case 3:
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz + halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz + halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz - halfSize));
								break;

							case 2:
								verts.Add (new Vector3 (px + halfSize, py + halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz + halfSize));
								verts.Add (new Vector3 (px + halfSize, py + halfSize, pz + halfSize));
								break;

							case 5:
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize, py + halfSize, pz - halfSize));
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz - halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz - halfSize));
								break;

							case 4:
								verts.Add (new Vector3 (px - halfSize, py + halfSize, pz + halfSize));
								verts.Add (new Vector3 (px - halfSize, py - halfSize, pz + halfSize));
								verts.Add (new Vector3 (px + halfSize, py - halfSize, pz + halfSize));
								verts.Add (new Vector3 (px + halfSize, py + halfSize, pz + halfSize));
								break;
							}

							normals.Add (faceNormals [f]);
							normals.Add (faceNormals [f]);
							normals.Add (faceNormals [f]);
							normals.Add (faceNormals [f]);

							// color index starts with 1
							Color c = palatte [chunk.faces [f].colorIndices [x, y, z] - 1];

							colors.Add (c);
							colors.Add (c);
							colors.Add (c);
							colors.Add (c);

							indicies.Add (totalQuadCount * 4 + 0);
							indicies.Add (totalQuadCount * 4 + 1);
							indicies.Add (totalQuadCount * 4 + 2);
							indicies.Add (totalQuadCount * 4 + 2);
							indicies.Add (totalQuadCount * 4 + 3);
							indicies.Add (totalQuadCount * 4 + 0);

							totalQuadCount += 1;

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
								totalQuadCount = 0;
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
		}

		Debug.Log (string.Format ("[MVImport] Mesh generated, total quads {0}", totalQuadCount));

		return result.ToArray();
	}
}

