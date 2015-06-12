using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = System.Object;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AU
{
    public static class AssetUtility
    {
        public static Transform FindTransformInChildren(Transform parent, String name, Transform d = null)
        {
            Transform trans = parent.GetComponentsInChildren<Transform>().FirstOrDefault(t => t.name == name);
            if (trans == null) return d;
            return trans;
        }

        public static T GetCopyOf<T>(this Object comp, T other) where T : class
        {
            Type type = comp.GetType();
            if (type != other.GetType()) return null; // type mis-match
            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Default | BindingFlags.DeclaredOnly;
            PropertyInfo[] pinfos = type.GetProperties(flags);
            foreach (var pinfo in pinfos)
            {
                if (pinfo.CanWrite)
                {
                    try
                    {
                        pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                    }
                    catch { }
                }
            }
            FieldInfo[] finfos = type.GetFields(flags);
            foreach (var finfo in finfos)
            {
                finfo.SetValue(comp, finfo.GetValue(other));
            }
            return comp as T;
        }

#if UNITY_EDITOR
        public static string GetAssetPath(UnityEngine.Object rsrc)
        {
            if (rsrc == null)
            {
                return "";
            }
            var path = AssetDatabase.GetAssetPath(rsrc);

            while (String.IsNullOrEmpty(path))
            {
                var parent = PrefabUtility.GetPrefabParent(rsrc);

                if (parent == null)
                {
                    break;
                }

                path = AssetDatabase.GetAssetPath(parent);

                rsrc = parent;
            }

            int index = path.IndexOf("Resources");
            if(index != -1)
            {
                path = path.Substring(index + 10);
                return path.Split(new char[] { '.' })[0];
            }
            return "";
        }

        public static string GetAssetPathNonResource(UnityEngine.Object rsrc)
        {
            if (rsrc == null)
            {
                return "";
            }
            var path = AssetDatabase.GetAssetPath(rsrc);

            while (String.IsNullOrEmpty(path))
            {
                var parent = PrefabUtility.GetPrefabParent(rsrc);

                if (parent == null)
                {
                    break;
                }

                path = AssetDatabase.GetAssetPath(parent);

                rsrc = parent;
            }
            return path;
        }

        public static void SavePrefab(GameObject obj)
        {
            try
            {
                UnityEngine.Object parent = PrefabUtility.GetPrefabParent(obj);
                if (parent != null)
                    PrefabUtility.ReplacePrefab(parent as GameObject, parent, ReplacePrefabOptions.ConnectToPrefab);
            }
            catch
            {

            }
        }

#else

        // placeholder, this should not be called in runtime
        public static string GetAssetPath(UnityEngine.Object rsrc)
        {
            return  rsrc.name;
        }
        
        public static void SavePrefab(GameObject obj)
		{
		}

#endif


        public static T LoadAsset<T>(string path) where T: UnityEngine.Object
        {
			if(path.Length == 0)
			{
				return null;
			}
            T r = Resources.Load(path, typeof(T)) as T;
			if(r == null)
				Debug.LogError("[AssetUtility] Failed to load " + path);
			return r;
        }

#if UNITY_EDITOR
        // this guy uses relative path of the unity project
        // use only in editor please
        public static T LoadAssetAtPath<T>(string path) where T : UnityEngine.Object
        {
            if (path.Length == 0)
            {
                return null;
            }

			int asIndex = path.IndexOf ("Assets");
			if (asIndex > 0)
				path = path.Substring (asIndex);

            T r = AssetDatabase.LoadAssetAtPath(path, typeof(T)) as T;
            if (r == null)
                Debug.LogError("[AssetUtility] Failed to load " + path);
            return r;
        }
#endif

        public static bool TryLoadAsset<T>(string path, out T v) where T: UnityEngine.Object
        {
            if (path.Length == 0)
            {
                v = null;
                return false;
            }
            T r = Resources.Load(path, typeof(T)) as T;
            if (r == null)
            {
                v = null;
                return false;
            }
            v = r;
            return true;
        }

        public static bool AssetExists(string p)
        {
            if (Resources.Load(p) == null)
                return false;
            return true;
        }
    }
}