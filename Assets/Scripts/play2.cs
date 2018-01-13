﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class play2 : MonoBehaviour
{
    public int velocity = 2;
    private bool snakeAlive = true;
    private int numParts;
    private float time;

    //directions
    private bool pX = false; //positive x
    private bool nX = false; //negative x
    private bool pZ = true; //positive z
    private bool nZ = false; //negative z

    //moviments
    private List<string> moviments = new List<string>();
    private string currentMoviment;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    private float dragDistance;  //minimum distance for a swipe to be registered

    //position
    private float x;
    private float y;
    private float z;

    //BEFORE position
    private float bx;
    private float by;
    private float bz;

    //loop
    private IEnumerator coroutine;

    //color
    public Material rend;

    //jump
    private int cooldownJump = 12;
    private int currentCooldownJump;
    private bool canJump;
    private int jumpSituation;
    // 1=> começou a subir
    // 2=> no ápice do pulo
    // 3=> começou a descer
    // 4=> no chão


    //dive
    private int cooldownDive = 12;
    private int currentCooldownDive;
    private bool canDive;
    private int diveSituation;
    // 1=> começou a descer
    // 2=> no ápice do mergulho
    // 3=> começou a subir
    // 4=> no chão

    //points
    public int point;

    //texts
    public Text scoreText;
    public Outline jumpOutline, diveOutline;
    public Text jumpText, diveText;

    //sounds
    public AudioClip bite;
    public AudioClip getStar;
    public AudioClip gulp;
    public AudioClip music;
    public AudioClip lacucaracha;
    private bool badMusicIsPlaying;
    AudioSource audioSource;

    public play1 otherplay;

    // Use this for initialization
    void Start()
    {
        x = 1;
        y = 0;
        z = 1;
        time = 0.3f;
        currentMoviment = "pZ";
        numParts = 4;
        moviments = new List<string>();

        

        point = 0;
        setScoreText();

        dragDistance = Screen.height * 15 / 100; //dragDistance is 15% height of the screen

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = music;
        audioSource.Play();

        badMusicIsPlaying = false;

        setCanJump();
        setCanDive();
        StartCoroutine(moviment());
    }

    // Update is called once per frame
    void Update()
    {
        if (snakeAlive)
        {
#if UNITY_ANDROID
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                fp = touch.position;
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                lp = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                lp = touch.position;  //last touch position. Ommitted if you use list
 
                //Check if drag distance is greater than 20% of the screen height
                if (Mathf.Abs(lp.x - fp.x) > dragDistance || Mathf.Abs(lp.y - fp.y) > dragDistance)
                {//It's a drag
                 //check if the drag is vertical or horizontal
                    if (Mathf.Abs(lp.x - fp.x) > Mathf.Abs(lp.y - fp.y))
                    {   //If the horizontal movement is greater than the vertical movement...
                        if ((lp.x > fp.x))  //If the movement was to the right)
                        {   //Right swipe
                            moviments.Insert(0, keyD());
                        }
                        else
                        {   //Left swipe
                            moviments.Insert(0, keyA());
                        }
                    }
                    else
                    {   //the vertical movement is greater than the horizontal movement
                        if (lp.y > fp.y)  //If the movement was up
                        {   //Up swipe
                            if(canJump) {
                                currentCooldownJump = cooldownJump;
                                jumpSituation = 1;
                            }
                        }
                        else
                        {   //Down swipe
                            if(canDive) {
                                currentCooldownDive = cooldownDive;
                                diveSituation = 1;
                            }
                        }
                    }
                }
                else
                {   //It's a tap as the drag distance is less than 20% of the screen height
                    Debug.Log("Tap");
                }
            }
        }

#else
            if (Input.GetKeyDown(KeyCode.RightArrow)) // D direita
            {
                if (!appleSpawn.eatsBadApple)
                    moviments.Insert(0, keyD());
                else
                    moviments.Insert(0, keyA());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow)) // A esquerda
            {
                if (!appleSpawn.eatsBadApple)
                    moviments.Insert(0, keyA());
                else
                    moviments.Insert(0, keyD());
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && canDive) // S mergulha
            {
                if (!appleSpawn.eatsBadApple)
                    MoveDown();
                else
                    MoveUp();
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && canJump) // W pula
            {
                if (!appleSpawn.eatsBadApple)
                    MoveUp();
                else
                    MoveDown();
            }
#endif
        }
        else
        {
            SceneManager.LoadScene("lost-win-multiplay", LoadSceneMode.Single);
        }
    }


    private void MoveDown()
    {
        currentCooldownDive = cooldownDive;
        diveSituation = 1;
    }

    private void MoveUp()
    {
        currentCooldownJump = cooldownJump;
        jumpSituation = 1;
    }

    private void setCanJump()
    {
        if (currentCooldownJump < 0)
            canJump = true;
        else
            canJump = false;
    }

    private void setCanDive()
    {
        if (currentCooldownDive < 0)
            canDive = true;
        else
            canDive = false;
    }

    private string keyD()
    {
        string switcher;
        if (moviments.Count != 0)
            switcher = moviments[0];
        else
            switcher = currentMoviment;
        switch (switcher)
        {
            case "pX":
                nZ = true;
                pZ = nX = pX = false;
                return "nZ";
            case "nX":
                pZ = true;
                nZ = nX = pX = false;
                return "pZ";
            case "pZ":
                pX = true;
                nZ = nX = pZ = false;
                return "pX";
            case "nZ":
                nX = true;
                nZ = pZ = pX = false;
                return "nX";
            default:
                return "erro";
        }

    }

    private string keyA()
    {
        string switcher;
        if (moviments.Count != 0)
            switcher = moviments[0];
        else
            switcher = currentMoviment;
        switch (switcher)
        {
            case "pX":
                pZ = true;
                nZ = nX = pX = false;
                return "pZ";
            case "nX":
                nZ = true;
                pZ = nX = pX = false;
                return "nZ";
            case "pZ":
                nX = true;
                nZ = pX = pZ = false;
                return "nX";
            case "nZ":
                pX = true;
                nZ = pZ = nX = false;
                return "pX";
            default:
                return "erro";
        }
    }

    private void attPositions()
    {
        if (moviments.Count != 0)
        {
            currentMoviment = moviments[moviments.Count - 1];
            moviments.RemoveAt(moviments.Count - 1);
        }

        switch (currentMoviment)
        {
            case "pX":
                x++;
                break;
            case "nX":
                x--;
                break;
            case "pZ":
                z++;
                break;
            case "nZ":
                z--;
                break;
            default:
                break;
        }
    }

    private IEnumerator moviment()
    {
        while (snakeAlive)
        {
            bx = x;
            by = y;
            bz = z;
            GameObject body = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            body.transform.position = new Vector3(bx, by, bz);
            body.transform.localScale = new Vector3(0.9f, 1f, 0.9f);
            body.GetComponent<Renderer>().material = rend;
            Destroy(body, time * numParts);

            yield return new WaitForSeconds(time);
            setScoreText();

            currentCooldownJump--;
            setCanJump();
            currentCooldownDive--;
            setCanDive();

            if (jumpSituation != 0)
            {
                switch (jumpSituation)
                {
                    case 1:
                        jumpSituation = 2;
                        attPositions();
                        y = 1.1f;
                        break;
                    case 2:
                        jumpSituation = 3;
                        attPositions();
                        break;
                    case 3:
                        jumpSituation = 4;
                        attPositions();
                        break;
                    case 4:
                        jumpSituation = 0;
                        attPositions();
                        y = 0f;
                        break;
                    default:
                        break;
                }

            }
            else if (diveSituation != 0)
            {
                switch (diveSituation)
                {
                    case 1:
                        diveSituation = 2;
                        attPositions();
                        y = -1.1f;
                        break;
                    case 2:
                        diveSituation = 3;
                        attPositions();
                        break;
                    case 3:
                        diveSituation = 4;
                        attPositions();
                        break;
                    case 4:
                        diveSituation = 0;
                        attPositions();
                        y = 0f;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                attPositions();
            }

            transform.LookAt(new Vector3(x, 0, z));
            transform.position = new Vector3(x, y, z);
            body.AddComponent<Rigidbody>();
            body.GetComponent<Rigidbody>().useGravity = false;
            body.tag = "body";
        }
    }

    private void setScoreText()
    {
        scoreText.text = point.ToString();
        if (currentCooldownJump <= 0)
        {
            jumpOutline.effectColor = Color.green;
            jumpText.text = "JUMP";
        }
        else
        {
            jumpOutline.effectColor = Color.red;
            jumpText.text = "JUMP: " + currentCooldownJump.ToString();
        }
        if (currentCooldownDive <= 0)
        {
            diveOutline.effectColor = Color.green;
            diveText.text = "DIVE";
        }
        else
        {
            diveOutline.effectColor = Color.red;
            diveText.text = "DIVE: " + currentCooldownDive.ToString();
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "apple")
        {
            if (appleSpawn.nextAppleIsBad)
            {
                audioSource.Stop();
                audioSource.clip = lacucaracha;
                audioSource.loop = true;
                audioSource.Play();
                badMusicIsPlaying = true;

                audioSource.PlayOneShot(gulp, 1f);
            }
            else
            {
                if (badMusicIsPlaying)
                {
                    audioSource.Stop();
                    audioSource.clip = music;
                    audioSource.loop = true;
                    audioSource.Play();
                    badMusicIsPlaying = false;
                }

                audioSource.PlayOneShot(bite, 1f);

            }

            numParts += 2;
            point += 2;
        }
        else if (collision.gameObject.tag == "star")
        {
            audioSource.PlayOneShot(getStar, 1f);
            numParts += 3;
            point += 8;
        }
        else if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "body")
        {
            velocity = 0;
            snakeAlive = false;
            saveScoreToLostScene();
            SceneManager.LoadScene("lost-win-multiplay", LoadSceneMode.Single);
        }
    }

    private void saveScoreToLostScene()
    {
        if (System.IO.File.Exists("multiplay.txt"))
        {
            System.IO.File.Delete("multiplay.txt");
        }
        System.IO.File.WriteAllText("multiplay.txt", otherplay.point.ToString() + "-" + this.point.ToString() + "-2");// play1 points - play2 points - who die

    }
}

