using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(MVVoxModel))]
public class MVVoxModelInspector : Editor {

	public override void OnInspectorGUI ()
	{
		base.OnInspectorGUI ();

		MVVoxModel voxModel = this.target as MVVoxModel;

		if(voxModel.vox != null)
			AU.AUEditorUtility.ColoredLabel (string.Format("Vox Controls ({0}x{1}x{2})", voxModel.vox.sizeX, voxModel.vox.sizeY, voxModel.vox.sizeZ), Color.green);
		else 
			AU.AUEditorUtility.ColoredLabel ("Vox Controls", Color.green);

		AU.AUEditorUtility.ColoredHelpBox (Color.yellow, "Enabling this may create lots of GameObjects, careful when the vox model is big");
		voxModel.importAsIndividualVoxels = EditorGUILayout.Toggle ("Import as Individual Voxels", voxModel.importAsIndividualVoxels);
		
		EditorGUILayout.BeginHorizontal ();
		if (GUILayout.Button ("Load")) {
			string path = EditorUtility.OpenFilePanel(
				"Open VOX model",
				"Assets/MagicaVoxel/Vox",
				"vox"
			);

			voxModel.filePath = path;

			Import (voxModel);
		}
		if (GUILayout.Button ("Reimport")) {

			Import (voxModel);
		}

		if (GUILayout.Button ("Clear")) {
			Clear (voxModel);
		}
		EditorGUILayout.EndHorizontal ();
	}

	public static void Clear(MVVoxModel voxModel) {
		MVVoxModelMesh[] subMeshes = voxModel.gameObject.GetComponentsInChildren<MVVoxModelMesh> ();
		foreach (MVVoxModelMesh subMesh in subMeshes)
			GameObject.DestroyImmediate (subMesh.gameObject);

		MVVoxModelVoxel[] subVoxels = voxModel.gameObject.GetComponentsInChildren<MVVoxModelVoxel> ();
		foreach (MVVoxModelVoxel v in subVoxels)
			GameObject.DestroyImmediate (v.gameObject);
		
	}

	public static void Import(MVVoxModel voxModel) {
		Clear (voxModel);

		if (voxModel.filePath != null && voxModel.filePath.Length > 0) {
			voxModel.vox = MVImporter.LoadVOX (voxModel.filePath);

			Material mat = (voxModel.defaultMaterial != null) ? voxModel.defaultMaterial : new Material (Shader.Find ("MagicaVoxel/FlatColor"));

			if (!voxModel.importAsIndividualVoxels) {
				
				Mesh[] meshes = MVImporter.CreateMeshes (voxModel.vox, voxModel.sizePerVox);
				string filename = System.IO.Path.GetFileNameWithoutExtension (voxModel.filePath);

				int index = 0;
				foreach (Mesh mesh in meshes) {
					GameObject go = new GameObject ();
					go.name = string.Format ("{0} ({1})", filename, index);
					go.transform.SetParent (voxModel.gameObject.transform);
					go.transform.localPosition = Vector3.zero;

					MeshFilter mf = go.AddComponent<MeshFilter> ();
					mf.mesh = mesh;

					MeshRenderer mr = go.AddComponent<MeshRenderer> ();
					mr.material = mat;

					go.AddComponent<MVVoxModelMesh> ();

					if(voxModel.addBoxColliders)
						go.AddComponent<BoxCollider> ();

					index++;
				}

			} else {

				MVImporter.CreateVoxelGameObjects (voxModel.vox, voxModel.gameObject.transform, mat, voxModel.sizePerVox);

			}

		} else {
			Debug.LogError ("[MVVoxModel] Invalid file path");
		}
	}
}
