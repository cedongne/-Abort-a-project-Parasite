using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public GameObject PlayerShadow;

    public static float defalutHitboxYPosition = -1.5f;
    public static float hitboxYPosition;

    private void Awake()
    {
        hitboxYPosition = defalutHitboxYPosition;
    }

    private void Update()
    {
        transform.localPosition = new Vector3(0, hitboxYPosition, 0);
    }
}
