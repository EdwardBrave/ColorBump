﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1F;

    public float border = 0F;

    private Vector2 startTouchPos;
    private Vector3 startPos;
    private Vector3 nextPos;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }

    private void OnEnable()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
            startTouchPos = Input.mousePosition;
#endif
        if (Input.touchCount > 0)
            startTouchPos = Input.touches[0].position;
    }

        private void OnDisable()
    {
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPos = Input.mousePosition;
            startPos = transform.position;
        }
#endif
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                startTouchPos = Input.touches[0].position;
                startPos = transform.position;
            }
        }
    }

    void FixedUpdate()
    {
        Update();
        startPos += new Vector3(0, 0, speed * Time.fixedDeltaTime);

        if (Input.touchCount > 0)
            OnTouch(Input.touches[0].position);
#if UNITY_EDITOR
        else if (Input.GetMouseButton(0))
            OnTouch(Input.mousePosition);
#endif
        else
            nextPos = transform.position + new Vector3(0, 0, speed * 2.57F * Time.fixedDeltaTime);

        if (border != 0)
        {
            if (nextPos.x > border)
                nextPos.x = border;
            else if (nextPos.x < -border)
                nextPos.x = -border;
        }

        rb.velocity = (nextPos - transform.position) * 20;

    }

    void OnTouch(Vector2 position)
    {
        nextPos = new Vector3(
            startPos.x - (startTouchPos.x - position.x) / Screen.width * 14,
            startPos.y,
            startPos.z - (startTouchPos.y - position.y) / Screen.height * 25); 
    }
}
