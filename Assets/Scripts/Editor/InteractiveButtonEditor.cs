using UnityEditor;

[CustomEditor(typeof(InteractiveButton))]
public class InteractiveButtonEditor : Editor
{
    private SerializedProperty smoothTime;
    private SerializedProperty initialScale;
    private SerializedProperty finalScale;

    private SerializedProperty selectSounds;
    private SerializedProperty interactSound;

    public void OnEnable()
    {
        // Scale
        smoothTime = serializedObject.FindProperty("moveTime");
        initialScale = serializedObject.FindProperty("initialScale");
        finalScale = serializedObject.FindProperty("finalScale");

        // Sound        
        selectSounds = serializedObject.FindProperty("selectSounds");
        interactSound = serializedObject.FindProperty("interactSounds");
    }

    public override void OnInspectorGUI()
    {
        InteractiveButton myButton = (InteractiveButton)target;
        serializedObject.Update();

        EditorGUILayout.LabelField("Scale Settings", EditorStyles.boldLabel);
        myButton.canScale = EditorGUILayout.Toggle("Can scale", myButton.canScale);
        if (myButton.canScale)
        {
            EditorGUILayout.PropertyField(smoothTime);
            EditorGUILayout.PropertyField(initialScale);
            EditorGUILayout.PropertyField(finalScale);
        }

        EditorGUILayout.Space(5);

        EditorGUILayout.LabelField("Sounds Settings", EditorStyles.boldLabel);
        myButton.canPlaySound = EditorGUILayout.Toggle("Can play sound", myButton.canPlaySound);
        if (myButton.canPlaySound)
        {
            EditorGUILayout.PropertyField(selectSounds);
            EditorGUILayout.PropertyField(interactSound);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
