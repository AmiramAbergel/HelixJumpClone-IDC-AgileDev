using UnityEngine;
using System.Collections.Generic;
using System;

public class Platform : MonoBehaviour
{
    public GameObject platformPartPrefab;

    public List<PlatformPart> parts = new List<PlatformPart>();

    public static Action<Transform> OnPlatformPassed = delegate { };

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BallControl>())
        {
            PassedPlatform();
        }
    }

    private void PassedPlatform()
    {
        OnPlatformPassed?.Invoke(_transform);

        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].meshCollider.enabled = false;
            parts[i].rb.isKinematic = false;
            parts[i].rb.AddRelativeForce(Vector3.up * GameManager.inst.gameConfig.platformPartPassedForce, ForceMode.Impulse);
            parts[i].rb.AddRelativeTorque(Vector3.right * UnityEngine.Random.Range(0.1f, 0.3f) * GameManager.inst.gameConfig.platformPartPassedForce, ForceMode.Impulse);

            Destroy(parts[i].gameObject, 2f);
        }
    }
}
