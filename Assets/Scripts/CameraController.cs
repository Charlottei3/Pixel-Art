using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Slider _slider;

    private Vector3 StartTouch;
    void Start()
    {
        cam = Camera.main;
        //targetZoom = cam.orthographicSize;
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        //  frameLimitCam = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameManager.Instance.isFirstClick)
        {
            StartTouch = cam.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(0) && !GameManager.Instance.isFirstClick)
        {
            Vector3 direction = StartTouch - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position += direction;

        }
        _slider.value += Input.GetAxis("Mouse ScrollWheel");

    }

    private void OnSliderValueChanged(float value)
    {
        cam.orthographicSize = Mathf.Lerp(GameManager.Instance.camMaxsize, 4, value);
    }

}
