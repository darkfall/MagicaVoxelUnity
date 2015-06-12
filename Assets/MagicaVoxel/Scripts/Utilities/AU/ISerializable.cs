using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using SimpleJSON;
using UnityEngine;

namespace AU
{

    public interface IJSONSerializable
    {

        void OnSerialize(JSONClass jc);

        void OnDeserialize(JSONNode jc);

    }


    public class JSONSerialization
    {
        public static void Serialize(JSONClass jc, object obj, bool serializeStatic = false)
        {
            foreach (FieldInfo mi in obj.GetType().GetFields())
            {
                if (mi.GetCustomAttributes(typeof(NonSerializedAttribute), true).Length > 0)
                    continue;

                if (!serializeStatic && mi.IsStatic)
                    continue;

                if (mi.FieldType.IsArray)
                {
                    IEnumerable arrobjs = (IEnumerable)mi.GetValue(obj);

                    JSONArray arr = new JSONArray();

                    if (typeof(IJSONSerializable).IsAssignableFrom(mi.FieldType.GetElementType()))
                    {
                        foreach (object aobj in arrobjs)
                        {
                            JSONClass cls = new JSONClass();
                            ((IJSONSerializable)aobj).OnSerialize(cls);
                            arr.Add(cls);
                        }
                    }
                    else
                    {
                        if (mi.FieldType.GetElementType() == typeof(GameObject))
                        {
                            foreach (object aobj in arrobjs)
                            {
                                arr.Add(AssetUtility.GetAssetPath((GameObject)aobj));
                            }
                        }
                        else
                        {
                            foreach (object aobj in arrobjs)
                            {
                                arr.Add(aobj.ToString());
                            }
                        }
                    }
                    jc[mi.Name] = arr;

                }
                else
                {
                    if (typeof(IJSONSerializable).IsAssignableFrom(mi.FieldType))
                    {
                        JSONClass cls = new JSONClass();
                        (mi.GetValue(obj) as IJSONSerializable).OnSerialize(cls);
                        jc[mi.Name] = cls;
                    }
                    else
                    {
                        if (mi.FieldType == typeof(GameObject))
                        {
                            jc[mi.Name] = AssetUtility.GetAssetPath((GameObject)mi.GetValue(obj));
                        }
                        else if (mi.FieldType == typeof(Color))
                        {
                            Color c = (Color)mi.GetValue(obj);
                            jc[mi.Name] = SerializeColor(c);
                        }
                        else if (mi.FieldType == typeof(Vector4))
                        {
                            Vector4 c = (Vector4)mi.GetValue(obj);
                            jc[mi.Name] = SerializeVector4(c);
                        }
                        else if (mi.FieldType == typeof(Vector3))
                        {
                            Vector3 c = (Vector3)mi.GetValue(obj);
                            jc[mi.Name] = SerializeVector3(c);
                        }
                        else if (mi.FieldType == typeof(Vector2))
                        {
                            Vector2 c = (Vector2)mi.GetValue(obj);
                            jc[mi.Name] = SerializeVector2(c);
                        }
                        else if (mi.FieldType == typeof(TimeSpan))
                        {
                            jc[mi.Name] = ((TimeSpan)mi.GetValue(obj)).ToString();
                        }
                        else if(mi.FieldType.IsEnum)
                        {
                            Enum v = (Enum)mi.GetValue(obj);
                            jc[mi.Name] = string.Format("{0} {1}", v.GetType().Name, v.ToString());
                        }
                        else
                        {
                            object v = mi.GetValue(obj);
                            if (mi.FieldType == typeof(string))
                                v = "";

                            if (v != null)
                                jc[mi.Name] = mi.GetValue(obj).ToString();
                            else
                                Debug.LogError("[JSONSerialization] Cannot save field: " + mi.Name + " due to its (null)");
                        }
                    }
                }
            }
        }

        public static void Deserialize(JSONNode jc, object obj)
        {
            foreach (FieldInfo mi in obj.GetType().GetFields())
            {
                if (mi.GetCustomAttributes(typeof(NonSerializedAttribute), true).Length > 0)
                    continue;

                if (mi.FieldType.IsArray)
                {
                    JSONArray arr = (JSONArray)jc[mi.Name];
                    IList arrobjs = (IList)Activator.CreateInstance(mi.FieldType, arr.Count);
                    mi.SetValue(obj, arrobjs);

                    if (typeof(IJSONSerializable).IsAssignableFrom(mi.FieldType.GetElementType()))
                    {
                        int index = 0;
                        foreach (JSONNode node in arr)
                        {
                            object newobj = Activator.CreateInstance(mi.FieldType.GetElementType());
                            (newobj as IJSONSerializable).OnDeserialize(node);
                            arrobjs[index] = newobj;
                            index++;
                        }
                    }
                    else
                    {
                        Type elementType = mi.FieldType.GetElementType();
                        if (elementType == typeof(int))
                        {
                            for (int i = 0; i < arr.Count; ++i)
                            {
                                arrobjs[i] = arr[i].AsInt;
                            }
                        }
                        else if (elementType == typeof(float))
                        {
                            for (int i = 0; i < arr.Count; ++i)
                            {
                                arrobjs[i] = arr[i].AsFloat;
                            }
                        }
                        else if (elementType == typeof(double))
                        {
                            for (int i = 0; i < arr.Count; ++i)
                            {
                                arrobjs[i] = arr[i].AsDouble;
                            }
                        }
                        else if (elementType == typeof(string))
                        {
                            for (int i = 0; i < arr.Count; ++i)
                            {
                                arrobjs[i] = (string)arr[i];
                            }
                        }
                        else if (elementType == typeof(bool))
                        {
                            for (int i = 0; i < arr.Count; ++i)
                            {
                                arrobjs[i] = arr[i].AsBool;
                            }
                        }
                        else if (elementType == typeof(GameObject))
                        {
                            for (int i = 0; i < arr.Count; ++i)
                            {
                                arrobjs[i] = AssetUtility.LoadAsset<GameObject>(arr[i]);
                            }
                        }
                    }
                }
                else
                {
                    if (typeof(IJSONSerializable).IsAssignableFrom(mi.FieldType))
                    {
                        object v = mi.GetValue(obj);
                        JSONNode node = jc[mi.Name];
                        (v as IJSONSerializable).OnDeserialize(node);
                    }
                    else
                    {
                        if (mi.FieldType == typeof(int))
                        {
                            mi.SetValue(obj, jc[mi.Name].AsInt);
                        }
                        else if (mi.FieldType == typeof(float))
                        {
                            mi.SetValue(obj, jc[mi.Name].AsFloat);
                        }
                        else if (mi.FieldType == typeof(double))
                        {
                            mi.SetValue(obj, jc[mi.Name].AsDouble);
                        }
                        else if (mi.FieldType == typeof(string))
                        {
                            string v = (string)jc[mi.Name];
                            mi.SetValue(obj, v == null ? "": v);
                        }
                        else if (mi.FieldType == typeof(bool))
                        {
                            mi.SetValue(obj, jc[mi.Name].AsBool);
                        }
                        else if(mi.FieldType.IsEnum)
                        {
                            string v = jc[mi.Name];
                            char[] sep = { ' ' };
                            string[] s = v.Split(sep);
                            Type t = Type.GetType("AU." + s[0]);
                            mi.SetValue(obj, Enum.Parse(t, s[1]));
                        }
                        else if (mi.FieldType == typeof(Color))
                        {
                            Color c = DeserializeColor(jc[mi.Name]);
                            mi.SetValue(obj, c);
                        }
                        else if (mi.FieldType == typeof(Vector4))
                        {
                            Vector4 c = DeserializeVector4(jc[mi.Name]);
                            mi.SetValue(obj, c);
                        }
                        else if (mi.FieldType == typeof(Vector2))
                        {
                            Vector2 c = DeserializeVector2(jc[mi.Name]);
                            mi.SetValue(obj, c);
                        }
                        else if (mi.FieldType == typeof(Vector3))
                        {
                            Vector3 c = DeserializeVector3(jc[mi.Name]);
                            mi.SetValue(obj, c);
                        }
                        else if(mi.FieldType == typeof(TimeSpan))
                        {
                            TimeSpan sp;
                            if(TimeSpan.TryParse(jc[mi.Name], out sp))
                            {
                                mi.SetValue(obj, sp);
                            }
                            else
                            {
                                Debug.Log("[JSONSerialization] Invalid TimeSpan value " + jc[mi.Name]);
                            }
                        }
                        else if (mi.FieldType == typeof(GameObject))
                        {
                            mi.SetValue(obj, AssetUtility.LoadAsset<GameObject>(jc[mi.Name]));
                        }
                    }
                }

            }
        }

        public static string SerializeColor(Color c)
        {
            return string.Format("{0} {1} {2} {3}", c.r, c.g, c.b, c.a);
        }

        public static string SerializeVector4(Vector4 c)
        {
            return string.Format("{0} {1} {2} {3}", c.x, c.y, c.z, c.w);
        }

        public static string SerializeVector3(Vector3 c)
        {
            return string.Format("{0} {1} {2}", c.x, c.y, c.z);
        }

        public static string SerializeVector2(Vector2 c)
        {
            return string.Format("{0} {1}", c.x, c.y);
        }

        public static Vector3 DeserializeVector3(string v)
        {
            char[] sep = { ' ' };
            string[] s = v.Split(sep);
            Vector3 c = new Vector3();
            c.x = float.Parse(s[0]);
            c.y = float.Parse(s[1]);
            c.z = float.Parse(s[2]);
            return c;
        }

        public static Vector3 DeserializeVector4(string v)
        {
            char[] sep = { ' ' };
            string[] s = v.Split(sep);
            Vector4 c = new Vector4();
            c.x = float.Parse(s[0]);
            c.y = float.Parse(s[1]);
            c.z = float.Parse(s[2]);
            c.w = float.Parse(s[3]);
            return c;
        }

        public static Vector3 DeserializeVector2(string v)
        {
            char[] sep = { ' ' };
            string[] s = v.Split(sep);
            Vector2 c = new Vector2();
            c.x = float.Parse(s[0]);
            c.y = float.Parse(s[1]);
            return c;
        }

        public static Color DeserializeColor(string v)
        {
            char[] sep = { ' ' };
            string[] s = v.Split(sep);
            Color c = new Color();
            c.r = float.Parse(s[0]);
            c.g = float.Parse(s[1]);
            c.b = float.Parse(s[2]);
            c.a = float.Parse(s[3]);
            return c;
        }


        public static JSONArray SerializeComponents(GameObject obj)
        {
            JSONArray a = new JSONArray();
            IJSONSerializable[] serializables = obj.GetInterfaces<IJSONSerializable>().ToArray();
            foreach (IJSONSerializable hs in serializables)
            {
                JSONClass hsNode = new JSONClass();

                hs.OnSerialize(hsNode);

                hsNode["_meta_cls"] = hs.GetType().Name;

                a.Add(hsNode);
            }
            return a;
        }

        public static void DeserializeComponents(JSONArray components, GameObject obj)
        {
            foreach (JSONNode node in components.Childs)
            {
                string clsName = node["_meta_cls"];
                Type t = Type.GetType("AU." + clsName);
                IJSONSerializable s = obj.AddComponent(t) as IJSONSerializable;

                s.OnDeserialize(node);
            }
        }

    }

    public class AutoJSONSerializableBehaviour : MonoBehaviour, IJSONSerializable
    {
        public void OnSerialize(JSONClass jc)
        {
            JSONSerialization.Serialize(jc, this);
        }

        public void OnDeserialize(JSONNode jc)
        {
            JSONSerialization.Deserialize(jc, this);
        }
    }

    public class AutoJSONSerializable : IJSONSerializable
    {
        public void OnSerialize(JSONClass jc)
        {
            JSONSerialization.Serialize(jc, this);
        }

        public void OnDeserialize(JSONNode jc)
        {
            JSONSerialization.Deserialize(jc, this);
        }
    }
}