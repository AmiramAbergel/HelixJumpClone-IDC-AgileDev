using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixControl : MonoBehaviour
{
    private Vector2 _lastPos;
    private Vector2 _delta;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform; // cache ternsform to avoid memory allocation at run-time
    }

    void Start()
    {
        InputManager.OnTouch += StartRotatation;
        InputManager.OnHold += Rotate;
    }

    private void OnDestroy()
    {
        InputManager.OnTouch -= StartRotatation;
        InputManager.OnHold -= Rotate;
    }

    private void StartRotatation(Vector2 mousePos)
    {
        _lastPos = mousePos;
    }

    private void Rotate(Vector2 mousePos)
    {
        _delta = _lastPos - mousePos;
        _lastPos = mousePos;

        // multiply by Time.deltaTime to make the rotation independent from the frame rate;
        Vector3 eular = new Vector3(0f, _delta.x, 0f) * GameManager.inst.gameConfig.rotationSensitivity * Time.deltaTime;
        transform.Rotate(eular);
    }
}
