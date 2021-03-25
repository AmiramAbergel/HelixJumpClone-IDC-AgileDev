using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Config")]
public class GameConfig : ScriptableObject
{
    public float jumpForce = 80f;
    public float rotationSensitivity = 10f;
    public float platformPartPassedForce = 10f;
    public float cameraFollowSpeed = 3f;
}
