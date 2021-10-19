using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private float _minSpeed = 12;
    private float _maxSpeed = 16;
    private float _maxTorque = 10;
    private float _xRange = 4;
    private float _ySpawnPos = -2;
    private Rigidbody _rb;
    private GameManager _gm;
    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        _gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(RandomForce(), ForceMode.Impulse);
        _rb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(),ForceMode.Impulse);;
        transform.position = RandomSpawnPos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (_gm.isGameActive)
        {
            _gm.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Bad"))
        {
            _gm.GameOver();
        }
        Destroy(gameObject);
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(_minSpeed, _maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-_maxTorque, _maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-_xRange, _xRange), _ySpawnPos, 0);
    }
}
