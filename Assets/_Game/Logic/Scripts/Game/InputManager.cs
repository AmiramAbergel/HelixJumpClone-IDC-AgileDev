using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static Action<Vector2> OnTouch = delegate { };
    public static Action<Vector2> OnHold = delegate { };
    public static Action<Vector2> OnRelease = delegate { };

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnTouch?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            OnHold?.Invoke(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnRelease?.Invoke(Input.mousePosition);
        }
    }
}
