using UnityEngine;
using UnityEditor;
using System.Collections;


public static class MVEditorUtilities {
	
	[MenuItem("Tools/MagicaVoxel/Load")]
	static void Load() {
		string path = EditorUtility.OpenFilePanel(
			"Open VOX model",
			"Assets/MagicaVoxel/Vox",
			"vox"
		);

		if (path != null && path.Length > 0)
		{
			GameObject go = new GameObject ();
			go.name = System.IO.Path.GetFileNameWithoutExtension (path);

			MVVoxModel voxModel = go.AddComponent<MVVoxModel> ();
			voxModel.ed_filePath = path;
			voxModel.LoadVOXFile (path, voxModel.ed_importAsIndividualVoxels);
		}
	}
}
