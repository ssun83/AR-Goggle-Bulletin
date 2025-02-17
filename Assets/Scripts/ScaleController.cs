﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleController : MonoBehaviour
{

    public bool moveX;
    public bool moveY;
    public bool moveZ;

    public float speed = 1.2f;

    public Vector3 amplitude = Vector3.one;

    private Transform tr;
    private float counter;
    private Vector3 initialOffsets;

    void Awake()
    {
        tr = GetComponent<Transform>();
        initialOffsets = tr.localScale;
        counter = 0f;
    }

    void Update()
    {
        counter += Time.deltaTime * speed;

        Vector3 newPosition = new Vector3
        (
            moveX ? initialOffsets.x + amplitude.x * Mathf.Sin(counter) : tr.localScale.x,
            moveY ? initialOffsets.y + amplitude.y * Mathf.Sin(counter) : tr.localScale.y,
            moveZ ? initialOffsets.z + amplitude.z * Mathf.Sin(counter) : tr.localScale.z
        );

        tr.localScale = newPosition;
    }
}
