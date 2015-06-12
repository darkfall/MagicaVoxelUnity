using UnityEngine;
using UnityEditor;
using System.Collections;

public class MVImportMenu {

	[MenuItem("MagicaVoxel/Import")]
	public static void Import()
	{
		string path = EditorUtility.OpenFilePanel(
			"Open VOX model",
			"Assets/MagicaVoxel/Vox",
			"vox"
		);

		if (path.Length > 0) {

			MVMainChunk mc = MVImporter.LoadVOX (path);

			Mesh m = MVImporter.CreateMeshFromChunk (mc.voxelChunk, mc.palatte);

			if (m != null) {
				GameObject go = new GameObject ();
				MeshFilter mf = go.AddComponent<MeshFilter> ();
				mf.mesh = m;

				MeshRenderer mr = go.AddComponent<MeshRenderer> ();

				go.AddComponent<BoxCollider> ();

				Material mat = new Material (Shader.Find ("MagicaVoxel/FlatColor"));
				mr.material = mat;
			}

		}
	}

}
