using System.Collections;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {
    public int option=1;
    public SpawnFood food;

    void Update ()
    {
        float rotateVelocity = 90;
        float velocity = 2;

        transform.Translate(Vector3.forward * Time.deltaTime * velocity);
        switch (option)
        {
            case 1: //MOVE-SE PELO TECLADO E SUBIDA E DECIDA SÃO REALIZADAS POR TRANSIÇÕES, SEM ROTECIONAR
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
            case 3: //MOVE-SE PELO MOUSE E TODOS MOVIMENTOS SÃO POR ROTEÇÃO
                float scalarSpeed = 2.5f;
                Vector3 mousePostion = Input.mousePosition;

                float deltaX = (mousePostion.x - Screen.width / 2) / Screen.width / 2;
                float deltaY = (mousePostion.y - Screen.width / 2) / Screen.width / 2;

                transform.Rotate(Vector3.left * Time.deltaTime * rotateVelocity*deltaY * scalarSpeed);
                transform.Rotate(Vector3.up * Time.deltaTime * rotateVelocity * deltaX  * scalarSpeed);
                
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "normalFood")
        {
            //ganha ponto
         
        } else if (collision.gameObject.tag == "wall")
        {
            //perde jogo
        }
        //if(collision.gameObject.tag == "specialFood")
        //if(collision.gameObject.tag == "rottenFood")
    }
}
