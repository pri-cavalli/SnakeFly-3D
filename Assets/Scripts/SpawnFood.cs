using UnityEngine;

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
        transform.position = new Vector3(Random.Range(-1 * sizeX / 2, sizeX / 2), 2f, Random.Range(-1 * sizeZ / 2, sizeZ / 2));
    }
}
