using UnityEngine;
using System.Collections;

// stub class for editor
public class MVVoxModelVoxel : MonoBehaviour {

#if UNITY_EDITOR

	public MVVoxel voxel;

	public MVVoxModel parentVoxModel {
		get { return GetComponentInParent<MVVoxModel> (); }
	}

#endif

}
