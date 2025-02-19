#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.IO;
using Newtonsoft.Json;


namespace GB
{
    public class EditorUserData : EditorWindow
    {
        [MenuItem("GB/Core/UserData")]
        static void init()
        {
            EditorWindow.GetWindow(typeof(EditorUserData));
        }

        void OnEnable()
        {
            
            Load();
        }

        private void OnFocus()
        {
            Load();
        }


        void Load()
        {
    

            string path = Path.Combine(Application.persistentDataPath, "user_data.data");

            if (File.Exists(path))
            {
                string gz = File.ReadAllText(path); // 파일에서 읽기
                string json = Gzip.DeCompression(gz);
                userData = JsonConvert.DeserializeObject<UserData>(json);

            }
            else
            {
                userData = new UserData();
            }


            _serializedObject = new SerializedObject(this);
            _myUser = _serializedObject.FindProperty("userData");
            if (_myUser == null)
                Debug.Log("_myUser == null");


        }

        public void Save()
        {
            if(userData == null) userData= new UserData();
            
            string json = userData.ToJson();
            string gz = Gzip.Compression(json);
            string path = Path.Combine(Application.persistentDataPath, "user_data.data");
            File.WriteAllText(path, gz); // 파일에 저장
        }




        private SerializedObject _serializedObject;
        private SerializedProperty _myUser;
        public UserData userData = new UserData();
        private Vector2 scrollPosition;

        private void OnGUI()
        {
            var customHeaderStyle = CustomHeaderStyle();
            var buttonStyle = ButtonSyle();

            GUILayout.BeginArea(new Rect(10, 20, position.width - 20, position.height - 20));
            GUILayout.Label("GB USER DATA", customHeaderStyle);

            if (userData != null && _myUser != null)
            {
                GUILayout.BeginVertical("box");

                scrollPosition = GUILayout.BeginScrollView(scrollPosition);

                _serializedObject.Update();
                EditorGUILayout.PropertyField(_myUser);
                _serializedObject.ApplyModifiedProperties();
                GUILayout.EndScrollView();
                GUILayout.EndVertical();
            }

            GUI.backgroundColor = new Color(1f, 0.5f, 0f, 1f);

            if (GUILayout.Button("Save", buttonStyle))
            {
                Save();
            }

            GUI.backgroundColor = new Color(0f, 0f, 1f, 1f);

            if (GUILayout.Button("Load", buttonStyle))
            {
                Load();
                Repaint();
            }

            GUI.backgroundColor = new Color(0f, 1f, 1f, 1f);

            if (GUILayout.Button("Open UserData", buttonStyle))
            {
                string scriptPath = "Assets/GB/UserData/UserData.cs"; // 열고 싶은 스크립트의 경로
                Object scriptAsset = AssetDatabase.LoadAssetAtPath(scriptPath, typeof(MonoScript));
                if (scriptAsset != null)
                    AssetDatabase.OpenAsset(scriptAsset);
                else
                    Debug.LogWarning($"Script not found at path: {scriptPath}");
            }

            GUI.backgroundColor = Color.white;

            GUILayout.EndArea();
        }


        GUIStyle CustomHeaderStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 50;
            style.alignment = TextAnchor.MiddleCenter;
            style.margin = new RectOffset(0, 0, 0, 30);
            style.fontStyle = FontStyle.Bold;
            return style;
        }

        GUIStyle CustomSectionStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 18;
            style.alignment = TextAnchor.MiddleCenter;
            style.fontStyle = FontStyle.Bold;
            return style;
        }

        GUIStyle TextStyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.label);
            style.fontSize = 12;
            return style;
        }
        GUIStyle TextFiledSyle()
        {
            GUIStyle style = new GUIStyle(GUI.skin.textField);
            style.fontSize = 12;
            return style;
        }
        GUIStyle ButtonSyle()
        {
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.fontSize = 13;
            buttonStyle.margin = new RectOffset(5, 5, 10, 10);
            buttonStyle.fontStyle = FontStyle.Bold;
            buttonStyle.alignment = TextAnchor.MiddleCenter;
            buttonStyle.padding = new RectOffset(10, 10, 5, 5);
            return buttonStyle;

        }
    }
}

#endif
