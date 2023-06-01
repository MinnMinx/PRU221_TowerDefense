using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 minMaxOffsetX;
    [SerializeField]
    private Vector2 minMaxOffsetY;
    [SerializeField]
    private float moveSpeed = 5f;
    private Camera mainCam;
    private Vector3 originalPos, centerScreenPos;
    private static Vector2 CENTER_VIEWPORT_POS = new Vector2(0.5f, 0.5f);
    private Vector2 mouseStartPos;
    private bool _allowScroll = false;

    public static string CAMERA_SET_MOVEMENT = "CAMERA_SET_MOVEMENT";

    private void Start()
    {
        mainCam = Camera.main;
        originalPos = transform.position;
        _allowScroll = false;
        GameUiEventManager.Instance.RegisterEvent(CAMERA_SET_MOVEMENT, SetMovementEvent);
    }

    private void Update()
    {
//#if UNITY_EDITOR
//        if (Input.GetMouseButtonDown(0))
//        {
//            SetScrolling(!_allowScroll);
//        }
//#endif

        if (_allowScroll)
        {
            Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 distance = mouseWorldPos - centerScreenPos;
            distance = new Vector3
            {
                x = Mathf.Clamp(distance.x, minMaxOffsetX.x, minMaxOffsetX.y),
                y = Mathf.Clamp(distance.y, minMaxOffsetY.x, minMaxOffsetY.y),
                z = 0,
            };
            transform.position = Vector3.Lerp(transform.position, originalPos + distance, Time.deltaTime * moveSpeed);
        }
    }

    public void SetScrolling(bool scrolling = true)
    {
        _allowScroll = scrolling;
        if (_allowScroll)
        {
            mouseStartPos = mainCam.ScreenToWorldPoint(mouseStartPos);
            centerScreenPos = mainCam.ViewportToWorldPoint(CENTER_VIEWPORT_POS);
        }
    }

    public void InvertScrolling()
    {
        SetScrolling(!_allowScroll);
    }

    void SetMovementEvent(string evt, params object[] args)
    {
        if (string.IsNullOrEmpty(evt) || args == null || args.Length < 1)
            return;

        bool allowScroll = (bool)args[0];
        SetScrolling(allowScroll);
    }
}
