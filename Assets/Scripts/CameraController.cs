using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private CinemachineVirtualCamera virturalcam;
    [SerializeField] private Transform camlookat;
    public Slider _slider;

    private Vector3 StartTouch;

    public bool _isMoveCam;

    void Start()
    {
        cam = Camera.main;
        //targetZoom = cam.orthographicSize;
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
        //  frameLimitCam = transform.position;
    }

    void Update()
    {

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float different = currentMagnitude - prevMagnitude;
            Zoom(different * 0.0005f);
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !GameManager.Instance.isFirstClick)
            {
                StartTouch = cam.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0) && !GameManager.Instance.isFirstClick && GameManager.Instance.canMoveCam)
            {
                Vector3 direction = StartTouch - cam.ScreenToWorldPoint(Input.mousePosition);
                camlookat.transform.position = ClampCamera(cam.transform.position + direction * 1.3f);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            Zoom(Input.GetAxis("Mouse ScrollWheel"));
        }
    }

    private void OnSliderValueChanged(float value)
    {
        virturalcam.m_Lens.OrthographicSize = Mathf.Lerp(GameManager.Instance.camMaxsize, 8, value);
        camlookat.transform.position = ClampCamera(cam.transform.position);
    }
    private Vector3 ClampCamera(Vector3 targerPosition)
    {
        float _camheight = cam.orthographicSize;
        float _camwidth = cam.orthographicSize * cam.aspect;
        float mixX = (GameManager.Instance.centerCam.x - GameManager.Instance.camMaxsize * cam.aspect) + _camwidth ;
        float maxX = (GameManager.Instance.centerCam.x + GameManager.Instance.camMaxsize * cam.aspect) - _camwidth ;
        float mixY = (GameManager.Instance.centerCam.y - GameManager.Instance.camMaxsize ) + _camheight ;
        float maxY = (GameManager.Instance.centerCam.y + GameManager.Instance.camMaxsize) - _camheight;

        float newX = Mathf.Clamp(targerPosition.x, mixX, maxX);

        float newY = Mathf.Clamp(targerPosition.y, mixY, maxY);

        return new Vector3(newX, newY, -10);
    }
    private void Zoom(float increment)
    {
        _slider.value += increment;
        _slider.value = Mathf.Clamp01(_slider.value);
    }
}
