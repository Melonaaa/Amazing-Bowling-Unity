using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public enum State
    {
        Idle,
        Ready,
        Tracking
    }

    private State state
    {
        set
        {
            switch (value)
            {
                case State.Idle:
                    _targetZoomSize = _roundReadyZoomSize;
                    break;
                case State.Ready:
                    _targetZoomSize = _readyShotZoomSize;
                    break;
                case State.Tracking:
                    _targetZoomSize = _trackingZoomSize;
                    break;
            }
        }
    }

    private Transform _target;
    public float _SmoothTime = 0.2f;

    private Vector3 _lastMovingVelocity;
    private Vector3 _targetPosition;

    private Camera _cam;
    private float _targetZoomSize = 5f;
    private const float _roundReadyZoomSize = 14.5f;
    private const float _readyShotZoomSize = 5f;
    private const float _trackingZoomSize = 10f;
    private float _lastZoomSpeed;

    private void Awake()
    {
        _cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    private void Move()
    {
        _targetPosition = _target.transform.position;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, _targetPosition, ref _lastMovingVelocity, _SmoothTime);

        transform.position = smoothPosition;
    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(_cam.orthographicSize, _targetZoomSize, ref _lastZoomSpeed, _SmoothTime);

        _cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()
    {
        if(_target != null)
        {
            Move();
            Zoom();
        }
    }

    private void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        _target = newTarget;
        state = newState;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
