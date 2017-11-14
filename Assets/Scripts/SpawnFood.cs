using UnityEngine;
using System;

public class SpawnFood : MonoBehaviour {
    private float sizeX, sizeZ;


    void Start ()
    {
        sizeX = 30f;
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
        transform.position = new Vector3(
            UnityEngine.Random.Range( (float)Math.Floor((-1 * sizeX / 2)+1), (float)Math.Floor(sizeX / 2) ),
            2f,
            UnityEngine.Random.Range((float)Math.Floor(-1 * sizeZ / 2) + 1, (float)Math.Floor(sizeZ / 2) ));
    }
}
