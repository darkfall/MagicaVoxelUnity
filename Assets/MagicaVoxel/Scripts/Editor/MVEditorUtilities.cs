using UnityEngine;
using UnityEditor;
using System.Collections;


public static class MVEditorUtilities {
	
	public static Mesh CubeMesh()
	{
		return AssetDatabase.LoadAssetAtPath("Assets/MagicaVoxel/Meshes/cube.asset", typeof(Mesh)) as Mesh;
	}

	public static Mesh QuadMesh()
	{
		return AssetDatabase.LoadAssetAtPath("Assets/MagicaVoxel/Meshes/quad.asset", typeof(Mesh)) as Mesh;
	}

}
