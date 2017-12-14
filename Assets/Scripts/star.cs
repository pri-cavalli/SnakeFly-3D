using System.Collections;
using UnityEngine;
using System;
using UnityEngine;
using UnityEngine.UI;

public class star : MonoBehaviour {
    private float time;
    private float counterTime;
    private float counting;
    private Vector3 outOfGame;
    private bool isInGame;

    private float sizeX, sizeZ;
    

    //texts
    public Text text;

    // Use this for initialization
    void Start ()
    {
        counterTime = 2f;
        time = 20f;
        sizeX = 20f;
        sizeZ = 20f;
        StartCoroutine(spawn());
        StartCoroutine(counter());
        outOfGame = new Vector3(9999f, 9999f, 9999f);
        isInGame = false;
        text.text = "";

        transform.position = outOfGame;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 15);
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
        while (true)
        {
            string message = "";

            yield return new WaitForSeconds(time);
            if (isInGame)
            {
                transform.position = outOfGame;
                isInGame = false;
                message = "";

            }
            else if (UnityEngine.Random.Range(0, 10) == 1)
            {
                counting = time;
                isInGame = true;
                Vector3 newPosition = new Vector3(
                    UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeX / 2) + 1f, (float)Math.Floor(sizeX / 2) - 1f),
                    1.2f,
                    UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeZ / 2) + 1f, (float)Math.Floor(sizeZ / 2) - 1f)
                );
                transform.position = newPosition;
            }
        }
    }

    private IEnumerator counter()
    {
        while (true)
        {

            yield return new WaitForSeconds(counterTime);
            string message = "";
            if (isInGame)
            {
                counting--;
                message = counting.ToString();
            }
            text.text = message;
        }
    }
}
