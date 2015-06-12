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

			Debug.Log (mc);
		}
	}

}
