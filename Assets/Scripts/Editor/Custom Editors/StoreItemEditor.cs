using UnityEditor;

namespace PsychedelicGames.RotorBall.EditorScripts
{
    /// <summary>
    /// Custom editor for store item.
    /// </summary>
    [CustomEditor(typeof(StoreItem))]
    public class StoreItemEditor : Editor
    {
        private SerializedProperty itemSchemeProp;
        private SerializedProperty itemPrefabProp;
        private SerializedProperty itemSpriteProp;
        private SerializedProperty itemPreviewProp;
        private SerializedProperty itemPriceProp;
        private SerializedProperty itemNameProp;
        private SerializedProperty itemDescriptionProp;
        private SerializedProperty itemTypeProp;

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(itemTypeProp);

            int index = itemTypeProp.enumValueIndex;

            if (index == 3)
            {
                EditorGUILayout.PropertyField(itemSchemeProp);
            }
            else if (index == 0)
            {
                EditorGUILayout.PropertyField(itemPrefabProp);
                EditorGUILayout.PropertyField(itemSpriteProp);
            }
            else
            {
                EditorGUILayout.PropertyField(itemPrefabProp);
                EditorGUILayout.PropertyField(itemPreviewProp);
            }

            EditorGUILayout.PropertyField(itemPriceProp);
            EditorGUILayout.PropertyField(itemNameProp);
            EditorGUILayout.PropertyField(itemDescriptionProp);

            serializedObject.ApplyModifiedProperties();
        }

        private void OnEnable()
        {
            itemSchemeProp = serializedObject.FindProperty("itemScheme");
            itemPrefabProp = serializedObject.FindProperty("itemPrefab");
            itemSpriteProp = serializedObject.FindProperty("itemSprite");
            itemPreviewProp = serializedObject.FindProperty("itemPreview");
            itemPriceProp = serializedObject.FindProperty("itemPrice");
            itemNameProp = serializedObject.FindProperty("itemName");
            itemDescriptionProp = serializedObject.FindProperty("itemDescription");
            itemTypeProp = serializedObject.FindProperty("itemType");
        }
    }
}
