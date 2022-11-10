using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Targets : MonoBehaviour
{

    private Rigidbody targetRb;
    private float minSpeed = 14;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    public float zSpawnPos;
    private float ySpawnPos = 0;
    //private float zRange = Random.Range(;
    public int pointValue;
    public ParticleSystem explsosionParticle;

    
   

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce( RandomForce() , ForceMode.Impulse);
        //targetRb.AddTorque(RandomTorque(),RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = StartPos();

    }

    // Update is called once per frame
    void Update()
    {
        // if(FindObjectOfType<GameManager>().gameOver)
        // {
        //     Destroy(targetRb);
        // }
    }
    

    void OnMouseDown()
    {
        //FindObjectOfType<GameManager>().score += 5;
        // if(FindObjectOfType<GameManager>().gameOver == false)
        // {
        //     FindObjectOfType<GameManager>().UpdateScore(pointValue);
        //     Destroy(gameObject);
        //     Instantiate(explsosionParticle , transform.position , explsosionParticle.transform.rotation);
        // }

    }

    void OnTriggerEnter(Collider other) 
    {
        //FindObjectOfType<GameManager>().score -=2;
        Destroy(gameObject); 
        if(!gameObject.CompareTag("Bad"))
        {
            FindObjectOfType<GameManager>().UpdateLives(-1);
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed,maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque,maxTorque);
    }

    Vector3 StartPos()
    {
        return new Vector3(Random.Range(-xRange,xRange),ySpawnPos,zSpawnPos);
    }

/*    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.2f);
        FindObjectOfType<GameManager>().GameOver();
    }*/

    public void DestroyTarget()
    {
        if(!FindObjectOfType<GameManager>().gameOver)
        {
            //FindObjectOfType<GameManager>().DisplayScore(); Nk
            FindObjectOfType<GameManager>().UpdateScore(pointValue);
            Instantiate(explsosionParticle, transform.position, explsosionParticle.transform.rotation);

            if (gameObject.CompareTag("Bad"))
            {
                Destroy(gameObject);
                FindObjectOfType<GameManager>().GameOver();
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
