using UnityEngine;
using System;

public class SpawnFood : MonoBehaviour {
    private float sizeX, sizeZ;


    void Start ()
    {
        sizeX = 20f;
        sizeZ = 20f;
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
            UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeX / 2) + 1f, (float)Math.Floor(sizeX / 2)-1f ),
            -0.2f,
            UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeZ / 2) + 1f, (float)Math.Floor(sizeZ / 2)-1f )
       );
        transform.position = newPosition;
    }
}
