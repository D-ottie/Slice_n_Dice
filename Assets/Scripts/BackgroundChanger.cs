using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundChanger : MonoBehaviour
{

    public Material[] backgroundMat;
    //MeshRenderer meshRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PartyTime());
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    IEnumerator PartyTime()
    {
        //int i = Random.Range(0,backgroundMat.Length);
        while(!FindObjectOfType<GameManager>().gameOver)
        {
            int i = Random.Range(0,backgroundMat.Length);
            yield return new WaitForSeconds(0.3f);
            gameObject.GetComponent<MeshRenderer>().material = backgroundMat[i];
            
        }  
    }
}
