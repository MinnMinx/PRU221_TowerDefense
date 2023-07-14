using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2 minMaxOffsetX;
    [SerializeField]
    private Vector2 minMaxOffsetY;
    [SerializeField]
    private ParticleSystem clickFx;
    private float lerpVal = 0.5f;
    private Camera mainCam;
    private Vector3 lastFramePos, curMousePose, velocity;
    private bool _allowScroll = false;
    private int holdFrame = 0;

    public static string CAMERA_SET_MOVEMENT = "CAMERA_SET_MOVEMENT";

    private void Start()
    {
        mainCam = Camera.main;
        _allowScroll = false;
        GameUiEventManager.Instance.RegisterEvent(CAMERA_SET_MOVEMENT, SetMovementEvent);
    }

    private void Update()
    {
        float delta = Time.deltaTime;
        curMousePose = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCam.orthographicSize * 2));

        if (GameManager.instance.IsPlacingTower || GameManager.instance.IsRemovingTower)
        {
            _allowScroll = false;
            holdFrame = 0;
        }
        if (holdFrame > 0 && Input.GetMouseButton(0))
        {
            holdFrame++;
            if (Vector3.Distance(curMousePose, lastFramePos) > 0.001f)
            {
                velocity = (lastFramePos - curMousePose) / delta;
                lastFramePos = curMousePose;
            }
            //transform.position = new Vector3
            //{
            //    x = Mathf.Clamp(lastMove.x, minMaxOffsetX.x, minMaxOffsetX.y),
            //    y = Mathf.Clamp(lastMove.y, minMaxOffsetY.x, minMaxOffsetY.y),
            //    z = transform.position.z,
            //};
        }
        if (Input.GetMouseButtonUp(0))
        {
            holdFrame = 0;
        }

        if (Vector3.Distance(velocity, Vector3.zero) > 0.01f)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, lerpVal);
            transform.position = new Vector3
            {
                x = Mathf.Clamp(transform.position.x + velocity.x * delta, minMaxOffsetX.x, minMaxOffsetX.y),
                y = Mathf.Clamp(transform.position.y + velocity.y * delta, minMaxOffsetY.x, minMaxOffsetY.y),
                z = transform.position.z,
            };
        }
    }

    public void SetScrolling(bool scrolling = true)
    {
        _allowScroll = scrolling;
        holdFrame = 1;
        lastFramePos = curMousePose;
        clickFx.transform.position = curMousePose;
        clickFx.Play(true);
    }

    void SetMovementEvent(string evt, params object[] args)
    {
        if (string.IsNullOrEmpty(evt) || args == null || args.Length < 1)
            return;

        bool allowScroll = (bool)args[0];
        SetScrolling(allowScroll);
    }
}
