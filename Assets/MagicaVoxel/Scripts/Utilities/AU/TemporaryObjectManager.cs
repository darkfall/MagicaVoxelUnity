using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace AU
{

    [Serializable]
    public class TemporaryObjectCache
    {
        public GameObject prefabObject;

        public List<GameObject> activeObjects;
        public Stack<GameObject> freeObjects;
    }

    public class TemporaryObjectManager : MonoBehaviour
    {
        static TemporaryObjectManager instance;

        public static TemporaryObjectManager GetInstance()
        {
            if (!instance)
            {
                instance = FindObjectOfType<TemporaryObjectManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "TemporayObjectManager";
                    instance = go.AddComponent<TemporaryObjectManager>();
                }
            }
            return instance;
        }

        public GameObject objectRoot;
        public GameObject[] cachePrefabs;

        Dictionary<GameObject, TemporaryObjectCache> cacheObjDict = new Dictionary<GameObject, TemporaryObjectCache>();
        Dictionary<Object, TemporaryObjectCache> activeObjDict = new Dictionary<Object, TemporaryObjectCache>();
        NameGenerator _nameGenerator = new NameGenerator("TEMP_OBJECT_", "");

        public TemporaryObjectCache[] objectCaches
        {
            get { return cacheObjDict.Values.ToArray(); }
        }

        public void Awake()
        {
            instance = this;
        }

        public void Start()
        {
            instance = this;

            foreach (GameObject prefab in cachePrefabs)
            {
                if(prefab != null && !cacheObjDict.Keys.Contains(prefab))
                {
                    TemporaryObjectCache cache = new TemporaryObjectCache();
                    cache.prefabObject = prefab;
                    cache.freeObjects = new Stack<GameObject>();
                    cache.activeObjects = new List<GameObject>();

                    cacheObjDict.Add(cache.prefabObject, cache);
                }
            }
            activeObjDict = new Dictionary<Object, TemporaryObjectCache>();
        }

        GameObject InstPrefabViaCache(GameObject prefab)
        {
            GameObject newObj = null;
            if (prefab == null)
            {
                newObj = new GameObject();
            }
            else
            {
                TemporaryObjectCache cache;
                if (cacheObjDict.TryGetValue(prefab, out cache))
                {
                    if (cache.freeObjects.Count > 0)
                    {
                        newObj = cache.freeObjects.Pop();
                        cache.activeObjects.Add(newObj);

                        newObj.SetActive(true);
                        newObj.SendMessage("Reset", SendMessageOptions.DontRequireReceiver);
                    }
                    else
                    {
                        newObj = Instantiate(prefab) as GameObject;
                        cache.activeObjects.Add(newObj);
                        activeObjDict.Add(newObj, cache);
                    }
                }
                else
                {
                    newObj = Instantiate(prefab) as GameObject;
                }
            }
            
            return newObj;
        }

        public GameObject InstGameObject(GameObject prefab, Vector3 pos, Quaternion rot = new Quaternion())
        {
            GameObject newObj = InstPrefabViaCache(prefab);
            newObj.transform.position = pos;
            if(rot != Quaternion.identity)
                newObj.transform.rotation = rot;

            if (objectRoot != null)
                newObj.transform.parent = objectRoot.transform;
            else
                newObj.transform.parent = this.transform;

            return newObj;
        }

        public GameObject InstParticleSystem(GameObject prefab, Vector3 pos, Quaternion rot = new Quaternion())
        {
            GameObject newObj = InstGameObject(prefab, pos, rot);
            ParticleSystem ps = newObj.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            return newObj;
        }

        public GameObject InstGameObjectAsChild(GameObject prefab, GameObject transformParent)
        {
            GameObject newObj = InstPrefabViaCache(prefab);
            newObj.transform.SetParent(transformParent.transform);
            newObj.transform.localPosition = Vector3.zero;

            return newObj;
        }

        public GameObject InstParticleSystemAsChild(GameObject prefab, GameObject transformParent)
        {
            GameObject newObj = InstGameObjectAsChild(prefab, transformParent);
            ParticleSystem ps = newObj.GetComponent<ParticleSystem>();
            if (ps != null)
                ps.Play();

            return newObj;
        }

        public GameObject InstGameObject(Vector3 pos, Quaternion rot = new Quaternion())
        {
            GameObject newObj = new GameObject();
            newObj.name = _nameGenerator.next();

            if (objectRoot != null)
                newObj.transform.parent = objectRoot.transform;
            else
                newObj.transform.parent = this.transform;
            newObj.transform.localPosition = pos;

            if (rot != Quaternion.identity)
                newObj.transform.localRotation = rot;

            return newObj;
        }

        IEnumerator _DelayDestroy(Object o, float t)
        {
            yield return new WaitForSeconds(t);
            DestroyViaCache(o);
        }

        void DestroyViaCache(Object o)
        {
            TemporaryObjectCache cache;
            if(activeObjDict.TryGetValue(o, out cache))
            {
                cache.activeObjects.Remove((GameObject)o);
                cache.freeObjects.Push((GameObject)o);

                ((GameObject)o).SetActive(false);
            }
            else
            {
                Destroy(o);
            }
        }

        public void DelayDestroyObject(Object o, float t)
        {
            if (t == 0)
                DestroyObjectNow(o);
            else 
                StartCoroutine(_DelayDestroy(o, t));
        }

        public void DestroyObjectNow(Object o)
        {
            DestroyViaCache(o);
        }

    }
}

