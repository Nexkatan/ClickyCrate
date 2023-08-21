using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 15;
    private float maxSpeed = 17;
    private float maxTorque = 100;
    private float xRange = 4;
    private float ySpawnPos = -6;
    public int pointValue;
    public float yValueCut;
    private float yRandomRotation = 0.3f;

    private float songModeForce = 17;

    public bool downBomb = false;

    public ParticleSystem explosionParticle;

    public GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.transform.position = RandomSpawnPos();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque());
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

    Vector3 RandomDirection ()
    {
        if (transform.position.x < -2.5)
        {
            return new Vector3(Random.Range(0, yRandomRotation), 1, 0);
        }
        else if (transform.position.x > 2.5)
        {
            return new Vector3(Random.Range(-yRandomRotation, 0), 1, 0);
        }
        else
        {
            return new Vector3(Random.Range(-yRandomRotation, yRandomRotation), 1, 0);
        }
        
    }

    Vector3 RandomForce()
    {
            return RandomDirection() * Random.Range(minSpeed, maxSpeed);
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
