using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    public int option;

    // Use this for initialization
    void Start () {

        Screen.lockCursor = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float rotateVelocity = 90;
        float velocity = 2;
        switch (option)
        {
            case 1: //MOVE-SE PELO TECLADO E SUBIDA E DECIDA SÃO REALIZADAS POR TRANSIÇÕES, SEM ROTECIONAR
                transform.Translate(Vector3.forward * Time.deltaTime * velocity);
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector3.up * Time.deltaTime * velocity);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector3.down * Time.deltaTime * velocity);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up * Time.deltaTime * rotateVelocity);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.down * Time.deltaTime * rotateVelocity);
                }
                break;
            case 2: //MOVE-SE PELO TECLADO E TODOS MOVIMENTOS SÃO POR ROTEÇÃO
                transform.Translate(Vector3.forward * Time.deltaTime * velocity);
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(Vector3.up * Time.deltaTime * rotateVelocity);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(Vector3.down * Time.deltaTime * rotateVelocity);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Rotate(Vector3.right * Time.deltaTime * rotateVelocity);
                }
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Rotate(Vector3.left * Time.deltaTime * rotateVelocity);
                }
                break;
        }

        /*
        if(Input.GetAxis("Mouse Y")!=0)
        {
            transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotateVelocity);
        }
        */
        /*
        else
        if (Input.mousePosition.x > Screen.width - 10)
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rotateVelocity);
        else
        if (Input.mousePosition.x < 10)
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rotateVelocity);
        */


    }
}
