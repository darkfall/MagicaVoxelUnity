using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

#if UNITY_EDITOR
using UnityEditorInternal;
using UnityEditor;
#endif

namespace AU
{
    public class ReadOnlyAttribute : PropertyAttribute
    {
    }

    public class ObjectInfoNameSelector : PropertyAttribute
    {
    }

    public class TypeNameAttribute : PropertyAttribute
    {

    }

    public class FabricAudioSelector : PropertyAttribute
    {

    }

    public class AssetStringField : PropertyAttribute
    {
        public Type typeOfObject = typeof(UnityEngine.Object);
    }

    public static class AUEditorUtility
    {
        public static List<T> ResizeList<T>(List<T> list, int newSize) where T : new()
        {
            List<T> newList = new List<T>();
            int maxLen = Mathf.Min(newSize, list.Count);
            for (int i = 0; i < maxLen; ++i)
            {
                newList.Add(list[i]);
            }
            for (int i = 0; i < newSize - maxLen; ++i)
                newList.Add(new T());
            return newList;
        }

        public static void DestroyAllChild(GameObject go)
        {
            Transform t = go.transform;
            while (t.childCount > 0)
            {
                GameObject.DestroyImmediate(t.GetChild(0).gameObject);
            }
        }

        public static Mesh CreatePlaneMesh(float width, float height, int widthSegments, int heightSegments)
        {
            Mesh m = new Mesh();
            m.name = string.Format("PLANE_{0}x{1}", (int)width, (int)height);

            int hCount2 = widthSegments + 1;
            int vCount2 = heightSegments + 1;
            int numTriangles = widthSegments * heightSegments * 6;
            int numVertices = hCount2 * vCount2;

            Vector3[] vertices = new Vector3[numVertices];
            Vector2[] uvs = new Vector2[numVertices];
            int[] triangles = new int[numTriangles];

            int index = 0;
            float uvFactorX = 1.0f / widthSegments;
            float uvFactorY = 1.0f / heightSegments;
            float scaleX = width / widthSegments;
            float scaleY = height / heightSegments;
            for (float y = 0.0f; y < vCount2; y++)
            {
                for (float x = 0.0f; x < hCount2; x++)
                {
                    vertices[index] = new Vector3(x * scaleX - width / 2f, 0.0f, y * scaleY - height / 2f);
                    uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
                }
            }

            index = 0;
            for (int y = 0; y < heightSegments; y++)
            {
                for (int x = 0; x < widthSegments; x++)
                {
                    triangles[index] = (y * hCount2) + x;
                    triangles[index + 1] = ((y + 1) * hCount2) + x;
                    triangles[index + 2] = (y * hCount2) + x + 1;

                    triangles[index + 3] = ((y + 1) * hCount2) + x;
                    triangles[index + 4] = ((y + 1) * hCount2) + x + 1;
                    triangles[index + 5] = (y * hCount2) + x + 1;
                    index += 6;
                }
            }

            m.vertices = vertices;
            m.uv = uvs;
            m.triangles = triangles;
            m.RecalculateNormals();

            return m;
        }

#if UNITY_EDITOR

        static List<Color> colorStack = new List<Color>();
        static List<float> labelWidthStack = new List<float>();

        public static string ReadOnlyTextField(string label, string k)
        {
            GUI.enabled = false;
            string x = EditorGUILayout.TextField(label, k);
            GUI.enabled = true;
            return x;
        }

        public static Color EditorYellowColor()
        {
            if (!InternalEditorUtility.HasPro())
                return new Color(208.0f / 255.0f, 199.0f / 255.0f, 98.0f / 255.0f, 1.0f);
            else
                return Color.yellow;
        }

        public static void PushGUIColor(Color c)
        {
            colorStack.Add(GUI.color);
            GUI.color = c;
        }

        public static void PopGUIColor(int c = 1)
        {
            for (int i = 0; i < c; ++i)
            {
                if (colorStack.Count == 0)
                {
                    Debug.LogError("[EditorUtility] Color stack empty, cannot pop color, make sure PushColor and PopColor are balanced");
                    return;
                }

                GUI.color = colorStack.Last();
                colorStack.RemoveAt(colorStack.Count - 1);
            }
        }

        public static void PushLabelWidth(float w)
        {
            labelWidthStack.Add(EditorGUIUtility.labelWidth);
            EditorGUIUtility.labelWidth = w;
        }

        public static void PopLabelWidth(int c = 1)
        {
            for (int i = 0; i < c; ++i)
            {
                if (labelWidthStack.Count == 0)
                {
                    Debug.LogError("[EditorUtility] LabelWidth stack empty, cannot pop color, make sure PushColor and PopColor are balanced");
                    return;
                }

                EditorGUIUtility.labelWidth = labelWidthStack.Last();
                labelWidthStack.RemoveAt(labelWidthStack.Count - 1);
            }
        }

        public static void ColoredLabel(string t, Color c, TextAnchor anchor = TextAnchor.MiddleLeft)
        {
            Color o = GUI.color;
            GUI.color = c;
            TextAnchor prevAnchor = GUI.skin.label.alignment;
            EditorStyles.label.alignment = anchor;
            EditorGUILayout.LabelField(t);
            EditorStyles.label.alignment = prevAnchor;
            GUI.color = o;
        }

        public static void ColoredLabel(string t, Color c, TextAnchor anchor, params GUILayoutOption[] options)
        {
            Color o = GUI.color;
            GUI.color = c;
            TextAnchor prevAnchor = GUI.skin.label.alignment;
            EditorStyles.label.alignment = anchor;
            EditorGUILayout.LabelField(t, options);
            EditorStyles.label.alignment = prevAnchor;
            GUI.color = o;
        }

        public static void SectionTitle(string title, Color c)
        {
            EditorGUILayout.Space();
            ColoredLabel(title, c);
        }

        public static void AssetField<T>(string label, ref T obj, ref string path) where T : UnityEngine.Object
        {
            if (path.Length > 0)
            {
                PushGUIColor(Color.yellow);
                EditorGUILayout.HelpBox("Path: " + path, MessageType.None);
                PopGUIColor();

                if (obj == null)
                {
                    obj = AssetUtility.LoadAsset<T>(path);
                    if (obj == null)
                    {
                        path = "";
                    }
                }
            }

            obj = EditorGUILayout.ObjectField(label, obj, typeof(T), false) as T;
            if (GUI.changed)
            {
                if (obj != null)
                {
                    path = AssetUtility.GetAssetPath(obj);
                    if (path.Length == 0)
                    {
                        Debug.LogError("Prefab must be in [Resources] directory due to Unity runtime requirement");
                        obj = null;
                    }
                }
                else
                {
                    path = "";
                }
            }
        }


        public static void ColoredHelpBox(Color c, string text, MessageType mt = MessageType.None)
        {
            PushGUIColor(c);
            EditorGUILayout.HelpBox(text, mt);
            PopGUIColor();
        }

        public static void LayoutPushIndentLevel()
        {
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel += 1;
        }

        public static void LayoutPopIndentLevel()
        {
            EditorGUI.indentLevel -= 1;
            EditorGUILayout.EndVertical();
        }



        public static string GetSelectedPath()
        {
            string path = "";

            foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }

#endif
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }

    [CustomPropertyDrawer(typeof(AssetStringField))]
    public class AssetStringFieldDrawer : PropertyDrawer
    {
        UnityEngine.Object o = null;

        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.ObjectField(position, property.name, o, (this.attribute as AssetStringField).typeOfObject, false);
            if (EditorGUI.EndChangeCheck())
            {
                property.stringValue = AssetUtility.GetAssetPath(o);
            }
        }
    }

    [CustomPropertyDrawer(typeof(TypeNameAttribute))]
    public class TypeNameDrawer : PropertyDrawer
    {
        public UnityEngine.Object cacheObject;

        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true) * 2;
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            position.height /= 2;

            EditorGUI.BeginChangeCheck();
            cacheObject = EditorGUI.ObjectField(position, "Type Obj", cacheObject, typeof(UnityEngine.Object), true);
            if (EditorGUI.EndChangeCheck())
            {
                if (cacheObject != null)
                {
                    if (cacheObject.GetType() == typeof(MonoScript))
                    {
                        MonoScript script = cacheObject as MonoScript;
                        property.stringValue = script.GetClass().FullName;
                    }
                    else
                    {
                        property.stringValue = cacheObject.GetType().FullName;
                    }
                }
            }

            position.y += position.height;

            GUI.enabled = false;
            EditorGUI.LabelField(position, "Type Name", property.stringValue.Length > 0 ? property.stringValue : "(none)");
            GUI.enabled = true;
        }
    }


#endif

}