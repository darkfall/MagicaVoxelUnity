using UnityEngine;
using System.Collections;

// mainly a sub class for editor
// incase there are other subobjects that don't want to be removed when reimporting
public class MVVoxModelMesh : MonoBehaviour {

#if UNITY_EDITOR

	// when using individual voxel import & then combinaning voxels, stores an array of voxels for undo
	public MVVoxel[] voxels;

	public MVVoxModel parentVoxModel {
		get { return GetComponentInParent<MVVoxModel> (); }
	}

#endif


}
