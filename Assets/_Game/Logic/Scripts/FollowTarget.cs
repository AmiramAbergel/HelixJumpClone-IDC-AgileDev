using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    private Transform _target;
    private float _offsetY;
    private Transform _transform;


    void Start()
    {
        _transform = transform;

        _offsetY = _transform.position.y;

        BallControl.OnHitPlatform += OnHitPlatform;
        Platform.OnPlatformPassed += OnPassPlatform;
        GameManager.OnLevelStart += OnLevelStart;
    }

    private void OnDestroy()
    {
        BallControl.OnHitPlatform -= OnHitPlatform;
        Platform.OnPlatformPassed -= OnPassPlatform;
        GameManager.OnLevelStart -= OnLevelStart;
    }

    void Update()
    {
        if (_target == null)
            return;

        float targetPosY = _target.position.y + _offsetY;

        float y = Mathf.Lerp(transform.position.y, targetPosY, Time.deltaTime * GameManager.inst.gameConfig.cameraFollowSpeed);

        Vector3 pos = _transform.position;
        pos.y = y;

        transform.position = pos;
    }

    private void OnPassPlatform(Transform transform)
    {
        _target = transform;
    }

    private void OnHitPlatform(Transform transform)
    {
        _target = transform;
    }

    private void OnLevelStart(Level level)
    {
        _target = level.firstPlatform;
    }
}
