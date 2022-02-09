using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour
{
    public CamFollow _Cam;

    public Rigidbody _Ball;
    public Transform _FirePos;
    public Slider _PowerSlider;

    public AudioSource _ShootingAudio;
    public AudioClip _FireClip;
    public AudioClip _ChargingClip;

    public float _MinForce = 15f;
    public float _MaxForce = 30f;
    public float _ChargingTime = 0.75f;

    private float _currentForce;
    private float _chargeSpeed;
    private bool _fired;

    private void OnEnable()
    {
        _currentForce = _MinForce;
        _PowerSlider.value = _MinForce;
        _fired = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _chargeSpeed = (_MaxForce - _MinForce) / _ChargingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_fired)
            return;

        if(_currentForce >= _MaxForce && !_fired)
        {
            _currentForce = _MaxForce;
            Fire();
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            _currentForce = _MinForce;
            _ShootingAudio.clip = _ChargingClip;
            _ShootingAudio.Play();
        }
        else if(Input.GetButton("Fire1") && !_fired)
        {
            _currentForce += _chargeSpeed * Time.deltaTime;
            _PowerSlider.value = _currentForce;
        }
        else if(Input.GetButtonUp("Fire1") && !_fired)
        {
            Fire();
        }
    }

    private void Fire()
    {
        _fired = true;

        Rigidbody ballInstance = Instantiate(_Ball, _FirePos.position, _FirePos.rotation);

        ballInstance.velocity = _currentForce * _FirePos.forward;

        _ShootingAudio.clip = _FireClip;
        _ShootingAudio.Play();

        _currentForce = _MinForce;

        _Cam.SetTarget(ballInstance.transform, CamFollow.State.Tracking);
    }
}
