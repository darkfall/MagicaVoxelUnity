using UnityEngine;
using System.Collections;

public class MVVoxModel : MonoBehaviour {

#if UNITY_EDITOR
	[HideInInspector]
	// not neccessary in runtime
	public string ed_filePath = "";

	// for animations, voxels can later be combined into individual layers
	[HideInInspector]
	public bool ed_importAsIndividualVoxels = false;

#endif

	[HideInInspector]
	public MVMainChunk vox;

	[Range(0.01f, 5.0f)]
	public float sizePerVox = 1.0f;

	public Material voxMaterial = null;


	public void ClearVoxMeshes() {
		MVVoxModelMesh[] subMeshes = this.gameObject.GetComponentsInChildren<MVVoxModelMesh> ();
		foreach (MVVoxModelMesh subMesh in subMeshes)
			GameObject.DestroyImmediate (subMesh.gameObject);

		MVVoxModelVoxel[] subVoxels = this.gameObject.GetComponentsInChildren<MVVoxModelVoxel> ();
		foreach (MVVoxModelVoxel v in subVoxels)
			GameObject.DestroyImmediate (v.gameObject);

	}

	public void LoadVOXFile(string path, bool asIndividualVoxels) {
		ClearVoxMeshes ();

		if (path != null && path.Length > 0) {
			MVMainChunk v = MVImporter.LoadVOX (path);

			if (v != null) {
				Material mat = (this.voxMaterial != null) ? this.voxMaterial : MVImporter.DefaultMaterial;
				if (!asIndividualVoxels) {

					MVImporter.CreateVoxelGameObjects (v, this.gameObject.transform, mat, sizePerVox);

				} else {

					MVImporter.CreateIndividualVoxelGameObjects (v, this.gameObject.transform, mat, sizePerVox);

				}

				this.vox = v;
			}


		} else {
			Debug.LogError ("[MVVoxModel] Invalid file path");
		}
	}

	public void LoadVOXData(byte[] data, bool asIndividualVoxels) {
		ClearVoxMeshes ();

		MVMainChunk v = MVImporter.LoadVOXFromData(data);

		if (v != null) {
			Material mat = (this.voxMaterial != null) ? this.voxMaterial : MVImporter.DefaultMaterial;
			if (!asIndividualVoxels) {

				MVImporter.CreateVoxelGameObjects (v, this.gameObject.transform, mat, sizePerVox);

			} else {

				MVImporter.CreateIndividualVoxelGameObjects (v, this.gameObject.transform, mat, sizePerVox);

			}

			this.vox = v;
		}

	}
}
