#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;


namespace GB
{

    public class PresenterTracker : EditorWindow
    {

        [MenuItem("GB/Core/PresenterTracker")]
        static void init()
        {
            EditorWindow.GetWindow(typeof(PresenterTracker));
        }


        private void OnGUI()
        {
            var customHeaderStyle = CustomHeaderStyle();
            var customSectionStyle = CustomSectionStyle();
            var buttonStyle = ButtonSyle();

            GUILayout.BeginArea(new Rect(10, 20, position.width - 20, position.height - 20));

            GUILayout.BeginVertical("box");
            GUILayout.Label("GB Presenter", customHeaderStyle);
            GUILayout.EndVertical();
            if (!Application.isPlaying)
            {
                GUI.backgroundColor = new Color(1f, 0f, 0f, 1.0f);
                GUILayout.BeginVertical("box");
                GUILayout.Label("No Playing...", customHeaderStyle);
                GUILayout.EndVertical();
                GUI.backgroundColor = Color.white;
                GUILayout.EndArea();
                return;

            }



            ViewBindsDraw();
            GUILayout.EndArea();

        }


        Vector2 _viewScrollPos = Vector2.zero;

        private void ViewBindsDraw()
        {
          
            if (Presenter.I.Views == null) return;

            GUILayout.Space(30);

            GUI.backgroundColor = Color.white;


            _viewScrollPos = GUILayout.BeginScrollView(_viewScrollPos);
            foreach (var v in Presenter.I.Views)
            {
                EditorGUILayout.BeginHorizontal();
                DrawLabel(v.Key, 15, Color.green, TextAnchor.MiddleLeft, FontStyle.Bold);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);

                for (int i = 0; i < v.Value.Count; ++i)
                {
                    var obj = v.Value[i];
                    if (obj != null)
                    {
                        EditorGUILayout.BeginHorizontal();
                        DrawLabel(obj.gameObject.name, 10, Color.white, TextAnchor.MiddleCenter);
                        if (GUILayout.Button("Tracker"))
                        {
                            Selection.activeGameObject = obj.gameObject;
                        }

                        EditorGUILayout.EndHorizontal();
                    }
                }

                GUILayout.Space(30);
            }

            GUILayout.EndScrollView();

        }

   

        private void DrawLabel(string value, int fontSize, Color color, TextAnchor anchor = TextAnchor.MiddleLeft, FontStyle fStyle = FontStyle.Normal)
        {
            GUIStyle gUIStyle = new GUIStyle(GUI.skin.label);
            GUIStyle style = new GUIStyle();
            style.fontSize = fontSize;
            style.normal.textColor = color;
            style.alignment = anchor;
            
            style.fontStyle = fStyle;
            GUILayout.Label(value, style);
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
