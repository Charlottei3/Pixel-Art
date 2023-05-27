using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public Slider _slider;

    private Vector3 StartTouch;
    private Vector3 limitCam;
    void Start()
    {
        cam = Camera.main;

        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        limitCam = transform.position;
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

            Vector3 frameLimit = transform.position + direction;
            frameLimit.x = Mathf.Clamp(frameLimit.x, limitCam.x -10f, limitCam.x + 10f);
            frameLimit.y = Mathf.Clamp(frameLimit.y, limitCam.y -20f, limitCam.y + 20f);

            cam.transform.position += direction;
            transform.position = frameLimit;
        }
        _slider.value += Input.GetAxis("Mouse ScrollWheel");

    }

    private void OnSliderValueChanged(float value)
    {
        cam.orthographicSize = Mathf.Lerp(GameManager.Instance.camMaxsize, 4, value);
    }

}
