﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Slider _slider;
    private float targetZoom;
    private float zoomFactor = 10f;
    private float zoomSpeed = 20f;

    private float moveSpeed = 1f;
    private float smoothTime = 0.2f;

    private Vector3 lastMousePosition;
    private Vector3 currentVelocity;
    private Vector3 frameLimitCam;
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        frameLimitCam = transform.position;
    }

    void Update()
    {
        float scrollCamera;

        scrollCamera = Input.GetAxis("Mouse ScrollWheel");
        targetZoom -= scrollCamera * zoomFactor;
        //cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomSpeed);

        // Di chuyển camera khi nhấp chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;
            Vector3 targetPosition = transform.position + new Vector3(-deltaMousePosition.x, -deltaMousePosition.y, 0) * moveSpeed * Time.deltaTime;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

            targetPosition.x = Mathf.Clamp(targetPosition.x, frameLimitCam.x - 5f, frameLimitCam.x + 5f);
            targetPosition.y = Mathf.Clamp(targetPosition.y , frameLimitCam.y - 5f, frameLimitCam.y + 5f);

            transform.position = targetPosition;
            lastMousePosition = Input.mousePosition;
        }
    }

    private void OnSliderValueChanged(float value)
    {
        cam.orthographicSize = 15 * (1 - 0.7f * value);
    }

}
