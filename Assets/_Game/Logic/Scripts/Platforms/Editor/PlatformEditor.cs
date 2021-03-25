using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Platform))]
public class PlatformEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Platform script = (Platform)target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Platform"))
        {
            Create(script, script.platformPartPrefab, script.transform);
        }
    }

    public void Create(Platform script, GameObject prefab, Transform transform)
    {
        if(script.parts.Count > 0)
        {
            for (int i = 0; i < script.parts.Count; i++)
            {
                Destroy(script.parts[i].gameObject);
            }
            script.parts.Clear();
        }

        var angleStep = 360f / 12f;

        for (int i = 0; i < 12; i++)
        {
            GameObject part = (GameObject)PrefabUtility.InstantiatePrefab(prefab, transform);

            Vector3 eularRotation = new Vector3(part.transform.localEulerAngles.x, i * angleStep, part.transform.localEulerAngles.z);

            part.transform.localEulerAngles = eularRotation;

            script.parts.Add(part.GetComponent<PlatformPart>());
        }
    }
}
