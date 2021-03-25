using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class BallControl : MonoBehaviour
{
    public UnityEvent KillPlayerEvent;

    public static Action OnKillPlayer = delegate { };
    public static Action OnGoalReached = delegate { };
    public static Action<Transform> OnHitPlatform = delegate { };

    private Rigidbody _rb;
    private bool _collisionActive;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_collisionActive)
            return;

        PlatformPart platformPart = collision.gameObject.GetComponent<PlatformPart>();
        if (platformPart)
        {
            OnHitPlatform.Invoke(platformPart.transform);

            if (platformPart.type == PlatformPart.PlatformPartType.Killer)
                KillPlayer();
            else if(platformPart.type == PlatformPart.PlatformPartType.Goal)
                OnGoalReached.Invoke();
            else
                Jump();
        }
        _collisionActive = true;

        Invoke(nameof(ResetCollistion), 0.2f);
    }

    private void Jump()
    {
        _rb.AddForce(GameManager.inst.gameConfig.jumpForce * Vector3.up, ForceMode.Impulse);
    }

    private void KillPlayer()
    {
        KillPlayerEvent.Invoke();
        OnKillPlayer.Invoke();
    }

    private void ResetCollistion()
    {
        _collisionActive = false;
    }
}
