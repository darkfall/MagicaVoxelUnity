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

	[MenuItem("Tools/MagicaVoxel/Load")]
	static void Load() {
		string path = EditorUtility.OpenFilePanel(
			"Open VOX model",
			"Assets/MagicaVoxel/Vox",
			"vox"
		);

		if (path != null && path.Length > 0)
		{
			string alphaMaskPath = string.Empty;
			if (EditorUtility.DisplayDialog("Question", "Do you want to load an alpha mask model?", "yes", "no"))
			{
				alphaMaskPath = EditorUtility.OpenFilePanel(
					"Open VOX model for alpha mask",
					"Assets/MagicaVoxel/Vox",
					"vox"
				);
			}

			GameObject go = new GameObject ();
			go.name = System.IO.Path.GetFileNameWithoutExtension (path);

			MVVoxModel voxModel = go.AddComponent<MVVoxModel> ();
			voxModel.ed_filePath = path;
			voxModel.ed_alphaMaskFilePath = alphaMaskPath;
			voxModel.LoadVOXFile (path, alphaMaskPath, voxModel.ed_importAsIndividualVoxels);
		}
	}
}
