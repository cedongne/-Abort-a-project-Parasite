using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;

public class Test : MonoBehaviour
{
    public int count;
    public int lateCount;
    public static Test instance;
    public delegate float CalDele(float num1, float num2);

    Transform playerTransform;

    Vector2 dirVec;
    public int attack;
    [SerializeField]
    public static int hp;

    private void Awake()
    {
        playerTransform = GameObject.Find("Square").transform;
    }
    void MeasureTime()
    {
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();

        sw.Start();

        for (int i = 0; i < 1000000; i++)
        {

        }

        sw.Stop();

//        Debug.Log("Directly : " + sw.ElapsedMilliseconds.ToString() + "ms");

    }
    void LookAtPlayer()
    {
        dirVec = playerTransform.position - transform.position;
    }

    void Start()
    {
        MeasureTime();
    }
}