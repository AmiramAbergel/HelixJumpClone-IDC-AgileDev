using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPart : MonoBehaviour
{
    public PlatformPartType type;
    public Material regularMaterial;
    public Material killerMaterial;
    public Rigidbody rb;
    public Collider meshCollider;

    public enum PlatformPartType
    {
        Regular,
        Killer,
        Goal
    };

    private void OnValidate()
    {
        SetMaterial(type);
    }

    private void SetMaterial(PlatformPartType type)
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();

        switch (type)
        {
            case PlatformPartType.Regular:
                renderer.material = regularMaterial;
                break;
            case PlatformPartType.Killer:
                renderer.material = killerMaterial;
                break;
        }
    }
}
