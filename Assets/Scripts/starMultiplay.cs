using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

public class starMultiplay : MonoBehaviour {
    private float time;
    private float counterTime;
    private float counting;
    private Vector3 outOfGame;
    private bool isInGame;
    private int lastShowed;

    private float sizeX, sizeZ;
    public play1 snake1;
    public play2 snake2;

    private int probability = 20; //a cada quantos pontos aparece a estrela

    //texts
    public Text text;

    // Use this for initialization
    void Start()
    {
        counterTime = 1f;
        time = 20f;
        sizeX = 20f;
        sizeZ = 20f;
        outOfGame = new Vector3(9999f, 9999f, 9999f);
        isInGame = false;
        lastShowed = 0;
        text.text = "";

        transform.position = outOfGame;
    }

    private void Update()
    {
        int points = snake1.point + snake2.point;
        transform.Rotate(Vector3.up * Time.deltaTime * 15);

        if (points != lastShowed && (points % probability == 0) && !isInGame)
        {
            lastShowed = points;
            counting = time;
            isInGame = true;
            Vector3 newPosition = new Vector3(
                (float)Math.Floor(UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeX / 2) + 1f, (float)Math.Floor(sizeX / 2) - 1f)),
                1.2f,
                (float)Math.Floor(UnityEngine.Random.Range(-1f * (float)Math.Floor(sizeZ / 2) + 1f, (float)Math.Floor(sizeZ / 2) - 1f))
            );
            transform.position = newPosition;
            StartCoroutine(spawn());
            StartCoroutine(counter());
        }
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
        yield return new WaitForSeconds(time);
        if (isInGame)
        {
            transform.position = outOfGame;
            isInGame = false;
        }
    }

    private IEnumerator counter()
    {
        string message = "";
        while (isInGame)
        {
            yield return new WaitForSeconds(counterTime);
            counting--;
            message = counting.ToString();
            text.text = message;
        }
        message = "";
        text.text = message;
    }
}

