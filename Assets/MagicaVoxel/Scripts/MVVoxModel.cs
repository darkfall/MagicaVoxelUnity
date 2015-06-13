using UnityEngine;
using System.Collections;

public class MVVoxModel : MonoBehaviour {

#if UNITY_EDITOR
	// not neccessary in runtime
	public string filePath = "";

	[Range(0.01f, 2.0f)]
	public float sizePerVox = 1.0f;

	// for animations, voxels can later be combined into individual layers
	[HideInInspector]
	public bool importAsIndividualVoxels = false;

	// automatically add a box collider ?
	public bool addBoxColliders = false;

	public Material defaultMaterial = null;

	[HideInInspector]
	public MVMainChunk vox;

#endif

}
