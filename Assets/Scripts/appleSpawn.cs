using UnityEngine;
using System;

public class appleSpawn : MonoBehaviour
{
    private float sizeX, sizeZ;


    void Start()
    {
        sizeX = 20;
        sizeZ = 20;
        changePos();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "head")
        {
            changePos();
        }
    }

    private void changePos()
    {
        Vector3 newPosition = new Vector3(
			(float)Math.Floor(UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeX / 2) + 1f, (float)Math.Floor(sizeX / 2) - 1f)),
            -0.2f,
			(float)Math.Floor(UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeZ / 2) + 1f, (float)Math.Floor(sizeZ / 2) - 1f))
       );
        transform.position = newPosition;
        if ( transform.GetComponent<Collider>().gameObject.tag == "head" || transform.GetComponent<Collider>().gameObject.tag == "body")
        {
            this.changePos();   
        }
    }
}
