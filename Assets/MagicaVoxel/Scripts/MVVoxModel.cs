using UnityEngine;
using System.Collections;

public class MVVoxModel : MonoBehaviour {

#if UNITY_EDITOR
	[HideInInspector]
	// not neccessary in runtime
	public string ed_filePath = "";

	[HideInInspector]
	// not neccessary in runtime
	public string ed_alphaMaskFilePath = "";

	// for animations, voxels can later be combined into individual layers
	[HideInInspector]
	public bool ed_importAsIndividualVoxels = false;

#endif

	[HideInInspector]
	public MVMainChunk vox;

	[Range(0.01f, 5.0f)]
	public float sizePerVox = 1.0f;

	public Material voxMaterial = null;

	public Transform meshOrigin = null;

	public void ClearVoxMeshes() {
		MVVoxModelMesh[] subMeshes = this.gameObject.GetComponentsInChildren<MVVoxModelMesh> ();
		foreach (MVVoxModelMesh subMesh in subMeshes)
			GameObject.DestroyImmediate (subMesh.gameObject);

		MVVoxModelVoxel[] subVoxels = this.gameObject.GetComponentsInChildren<MVVoxModelVoxel> ();
		foreach (MVVoxModelVoxel v in subVoxels)
			GameObject.DestroyImmediate (v.gameObject);

	}

	public void LoadVOXFile(string path, string alphaMaskPath, bool asIndividualVoxels) {
		ClearVoxMeshes ();

		if (path != null && path.Length > 0)
		{
			MVVoxelChunk alphaChunk = null;
			if (!string.IsNullOrEmpty(alphaMaskPath))
			{
				MVMainChunk mc = MVImporter.LoadVOX(alphaMaskPath, generateFaces: false);
				if(mc != null)
					alphaChunk = mc.voxelChunk;
			}

			MVMainChunk v = MVImporter.LoadVOX (path, alphaChunk);

			if (v != null) {
				Material mat = (this.voxMaterial != null) ? this.voxMaterial : MVImporter.DefaultMaterial;

				if (!asIndividualVoxels) {

					if (meshOrigin != null)
						MVImporter.CreateVoxelGameObjects(v, this.gameObject.transform, mat, sizePerVox, meshOrigin.localPosition);
					else
						MVImporter.CreateVoxelGameObjects (v, this.gameObject.transform, mat, sizePerVox);

				} else {

					if (meshOrigin != null)
						MVImporter.CreateIndividualVoxelGameObjects(v, this.gameObject.transform, mat, sizePerVox, meshOrigin.localPosition);
					else
						MVImporter.CreateIndividualVoxelGameObjects (v, this.gameObject.transform, mat, sizePerVox);

				}

				this.vox = v;
			}


		} else {
			Debug.LogError ("[MVVoxModel] Invalid file path");
		}
	}

	public void LoadVOXData(byte[] data, bool asIndividualVoxels, byte[] alphaMaskData = null) {
		ClearVoxMeshes ();

		MVVoxelChunk alphaMask = null;
		if(alphaMaskData != null)
		{
			MVMainChunk mc = MVImporter.LoadVOXFromData(alphaMaskData, generateFaces: false);
			if (mc != null)
				alphaMask = mc.voxelChunk;
		}
		MVMainChunk v = MVImporter.LoadVOXFromData(data, alphaMask);

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
