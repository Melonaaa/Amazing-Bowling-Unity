using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : MonoBehaviour
{
    public int _Score = 5;
    public ParticleSystem _ExplosionParticle;
    public float _Hp = 10f;

    public void TakeDamage(float damage)
    {
        _Hp -= damage;

        if(_Hp <= 0)
        {
            ParticleSystem instance = Instantiate(_ExplosionParticle, transform.position, transform.rotation);

            AudioSource explosionAudio = instance.GetComponent<AudioSource>();
            explosionAudio.Play();

            Destroy(instance.gameObject, instance.duration);
            gameObject.SetActive(false);
        }
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
