using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadTarget : Target
{
    // Start is called before the first frame update
    public ParticleSystem deactivateParticle;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (gameManager.isGameActive)
            {
                if (gameObject.CompareTag("Bad"))
                {
                    Instantiate(deactivateParticle, transform.position, deactivateParticle.transform.rotation);
                    Destroy(gameObject);
                    gameManager.UpdateScore(pointValue);
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Good") & downBomb)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            ParticleSystem hitExplode = other.gameObject.GetComponent<Target>().explosionParticle;
            Instantiate(hitExplode, other.transform.position, hitExplode.transform.rotation);
        }
    }
}
