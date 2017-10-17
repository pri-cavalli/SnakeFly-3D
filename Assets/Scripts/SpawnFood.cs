using UnityEngine;

public class SpawnFood : MonoBehaviour {
    public GameObject Foodprefab;
    public float size=26;

    void Start ()
    {
        transform.position = new Vector3(Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "snake")
        {
            transform.position = new Vector3(Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2), Random.Range(-size / 2, size / 2)); 
        }
    }
}
