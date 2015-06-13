using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(MVVoxModelMesh))]
public class MVVoxModelMeshInspector : Editor {

	public override void OnInspectorGUI ()
	{
		MVVoxModelMesh m = this.target as MVVoxModelMesh;
		if (m.voxels != null && m.voxels.Length > 1) {
			if (GUILayout.Button ("Convert to Voxels")) {

				CreateVoxels (m.voxels, m.parentVoxModel);

				GameObject.DestroyImmediate (m.gameObject);
			}
		}
	}

	public static void CreateVoxels(MVVoxel[] voxels, MVVoxModel voxModel) {
		MVVoxelChunk chunk = voxModel.vox.voxelChunk;

		float sizePerVox = voxModel.sizePerVox;

		float cx = sizePerVox * chunk.sizeX / 2;
		float cy = sizePerVox * chunk.sizeY / 2;
		float cz = sizePerVox * chunk.sizeZ / 2;

		Material mat = (voxModel.voxMaterial != null) ? voxModel.voxMaterial : MVImporter.DefaultMaterial;

		List<GameObject> objects = new List<GameObject> ();
		foreach (MVVoxel voxel in voxels) {
			float px = voxel.x * sizePerVox - cx, py = voxel.y * sizePerVox - cy, pz = voxel.z * sizePerVox - cz;

			GameObject go = MVImporter.CreateGameObject (voxModel.gameObject.gameObject.transform,
				               		 					 new Vector3 (px, py, pz),
														 string.Format ("Voxel ({0}, {1}, {2})", voxel.x, voxel.y, voxel.z),
				                						 MVImporter.CubeMeshWithColor (sizePerVox, voxModel.vox.palatte [chunk.voxels [voxel.x, voxel.y, voxel.z] - 1]),
				                						 mat);

			MVVoxModelVoxel v = go.AddComponent<MVVoxModelVoxel> ();
			v.voxel = new MVVoxel () { x = voxel.x, y = voxel.y, z = voxel.z, colorIndex = chunk.voxels [voxel.x, voxel.y, voxel.z] };

			objects.Add (go);
		}

		Selection.objects = objects.ToArray ();
	}

}
