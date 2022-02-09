using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterRotator : MonoBehaviour
{
    enum RotateState
    {
        Idle,
        Vertical,
        Horizontal,
        Ready
    }

    private RotateState _state = RotateState.Idle;
    public float _VerticalRotateSpeed = 360f;
    public float _HorizontalRotateSpeed = 360f;

    public BallShooter _BallShooter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (_state)
        {
            case RotateState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    _state = RotateState.Horizontal;
                }
                break;
            case RotateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, _HorizontalRotateSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    _state = RotateState.Vertical;
                }
                break;
            case RotateState.Vertical:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-_VerticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    _state = RotateState.Ready;
                    _BallShooter.enabled = true;
                }
                break;
            case RotateState.Ready:
                break;
        }
    }

    private void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        _state = RotateState.Idle;
        _BallShooter.enabled = false;
    }
}
