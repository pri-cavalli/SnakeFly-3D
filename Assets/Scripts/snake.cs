using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://unity3d.com/pt/learn/tutorials/projects/roll-ball-tutorial/displaying-score-and-text
public class snake : MonoBehaviour
{
    public int velocity = 2;
    private bool snakeAlive = true;
    private int numParts;
    private float tempo;

    //directions
    private bool pX = false; //positive x
    private bool nX = false; //negative x
    private bool pZ = true; //positive z
    private bool nZ = false; //negative z

    //moviments
    private List<string> moviments = new List<string>();
    private string currentMoviment;

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
    public int cooldown;
    private int currentCooldown;
    private bool canJump;
    private int jumpSituation;
        // 0=> no chão
        // 1=> começou a subir
        // 2=> no ápice do pulo
        // 3=> começou a descer
        // 4=> no chão

    //points
    private int point;

    //texts
    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        numParts = 1;
        x = y = 0;
        z = 1;
        tempo = 0.3f;
        currentMoviment = "pZ";
        numParts = 4;
        moviments = new List<string>();

        point = 0;
        setScoreText();

        cooldown = 35;
        currentCooldown = 0;
        setCanJump();
        StartCoroutine(moviment());
    }

    // Update is called once per frame
    void Update()
    {
        if (snakeAlive)
        {
            if (Input.GetKeyDown(KeyCode.D)) // D direita
            {
                moviments.Insert(0, keyD());
            }
            else if (Input.GetKeyDown(KeyCode.A)) // A esquerda
            {
                moviments.Insert(0, keyA());
            }
            if (Input.GetKeyDown(KeyCode.S)) // S cava
            {
                numParts++;
            }
            if (Input.GetKeyDown(KeyCode.W) && canJump) // W pula
            {
                currentCooldown = cooldown;
                jumpSituation = 1;
            }
        }
    }

    private void setCanJump()
    {
        if (currentCooldown < 0)
            canJump = true;
        else
            canJump = false;
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
            body.tag = "body";
            body.GetComponent<Renderer>().material = rend;
            Destroy(body, tempo * numParts);


            yield return new WaitForSeconds(tempo);
            setScoreText();

            currentCooldown--;
            setCanJump();
            switch(jumpSituation)
            {
                case 0:
                    y = 0f;
                    attPositions();
                    break;
                case 1:
                    jumpSituation = 2;
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
                    y = 0f;
                    break;
                default:
                    break;
            }

            transform.LookAt(new Vector3(x, 0, z));
            transform.position = new Vector3(x, y, z);
            body.AddComponent<Rigidbody>();
            body.GetComponent<Rigidbody>().useGravity = false;
        }
    }

    private void setScoreText()
    {
        string message = "Score: " + point.ToString() + "\n";
        if (currentCooldown <= 0 )
        {
            message += "Jump is enable!";
        } else
        {
            message += "Jump cooldown: " + currentCooldown.ToString();
        }
        scoreText.text = message;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "normalFood")
        {
            numParts++;
            point++;
        }
        else if (collision.gameObject.tag == "wall" || collision.gameObject.tag == "body")
        {
            velocity = 0;
            snakeAlive = false;
            Application.LoadLevel(3);
        }
    }
}