using System.Collections;
using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class star : MonoBehaviour {
    private float time;
    private Vector3 outOfGame;
    private bool isInGame;

    private float sizeX, sizeZ;


    //texts
    public Text text;

    // Use this for initialization
    void Start ()
    {
        time = 10f;
        StartCoroutine(spawn());
        outOfGame = new Vector3(9999f, 9999f, 9999f);
        isInGame = false;
        text.text = "";

        transform.position = outOfGame;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "head")
        {
            transform.position = outOfGame;
            isInGame = false;
            text.text = "";
        }
    }

    private IEnumerator spawn()
    {
        string message = "";
        text.text = message;

        yield return new WaitForSeconds(time);
        if (isInGame)
        {
            transform.position = outOfGame;
            isInGame = false;
        } else if (UnityEngine.Random.Range(0,10) < 8)
        {
            message = "Star is at the game!\n";
            isInGame = true;
            Vector3 newPosition = new Vector3(
                UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeX / 2) + 1f, (float)Math.Floor(sizeX / 2) - 1f),
                1.2f,
                UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeZ / 2) + 1f, (float)Math.Floor(sizeZ / 2) - 1f)
            );
            transform.position = newPosition;

            text.text = message;

        }
    }
}
