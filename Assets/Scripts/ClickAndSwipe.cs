using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private GameManager gameManager;
    private Camera camera;
    private Vector3 mousePos;
    private TrailRenderer trailRenderer;
    private BoxCollider collider;
    private bool swiping = false;
    private AudioSource playerAudio;
    public AudioClip crashMonsterSound;
    public AudioClip clockSound;

    // Start is called before the first frame update
    void Awake()
    {
        camera = Camera.main;
        trailRenderer = GetComponent<TrailRenderer>();
        collider = GetComponent<BoxCollider>();
        trailRenderer.enabled = false;
        collider.enabled = false;
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameHasStarted)
        {
            if(!gameManager.gameOver)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    swiping = true;
                    UpdateComponents();
                }
                else if(Input.GetMouseButtonUp(0))
                {
                    swiping = false;
                    UpdateComponents();
                }
                if(swiping)
                {
                    UpdateMousePosition();
                }
            
            }
        }

    }

    void UpdateMousePosition()
    {
        mousePos = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,Input.mousePosition.y,25f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trailRenderer.enabled = swiping;
        collider.enabled = swiping;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponent<Targets>())
        {
            playerAudio.PlayOneShot(crashMonsterSound, 1.0f);
            collision.gameObject.GetComponent<Targets>().DestroyTarget();
        }

    }
}
