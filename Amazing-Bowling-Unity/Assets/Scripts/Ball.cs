using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public LayerMask _WhatIsProp;

    public ParticleSystem _ExplosionParticle;
    public AudioSource _ExplosionAudio;

    public float _MaxDamage = 100f;
    public float _ExplosionForce = 1000f;
    public float _LifeTime = 10f;
    public float _ExplosionRadius = 20f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, _LifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _ExplosionRadius, _WhatIsProp);

        for (int nIndex = 0; nIndex < colliders.Length; nIndex++)
        {
            Rigidbody targetRigidBody = colliders[nIndex].GetComponent<Rigidbody>();
            targetRigidBody.AddExplosionForce(_ExplosionForce, transform.position, _ExplosionRadius);

            Prop targetProp = colliders[nIndex].GetComponent<Prop>();

            float damage = CalculateDamage(colliders[nIndex].transform.position);

            targetProp.TakeDamage(damage);
        }

        _ExplosionParticle.transform.parent = null;

        _ExplosionParticle.Play();
        _ExplosionAudio.Play();

        Destroy(_ExplosionParticle.gameObject, _ExplosionParticle.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 explosionToTarget = targetPosition - transform.position;

        float distance = explosionToTarget.magnitude;

        float edgeToCenterDistance = _ExplosionRadius - distance;

        float percentage = edgeToCenterDistance / _ExplosionRadius;

        float damage = _MaxDamage * percentage;

        damage = Mathf.Max(0, damage);

        return damage;
    }

    private void OnDestroy()
    {
        GameManager._Instance.OnBallDestroy();
    }
}
