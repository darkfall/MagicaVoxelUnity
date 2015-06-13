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
}
