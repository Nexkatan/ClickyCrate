using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public int pointValue;
    public float yValueCut;

    public bool downBomb = false;

    public ParticleSystem explosionParticle;

    public GameManager gameManager;

    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
        targetRb.transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (transform.position.y < yValueCut)
        {
            Destroy(gameObject);
        }
        if (targetRb.velocity.y < 0)
        {
            if (!gameManager.isGameActive)
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                downBomb = true;
            }
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
            if (gameObject.CompareTag("Bad"))
            {
                gameManager.GameOver();
            }
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            Destroy(gameObject);
            gameManager.UpdateScore(pointValue);
        }
    }
}
