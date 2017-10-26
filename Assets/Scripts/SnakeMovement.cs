using System.Collections;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {
    public int option=2;
    public SpawnFood food;

	//static Vector3 init_pos = Vector3(0.0, 0.0, -3.0);

	float rotateVelocity = 180;
	float velocity = 2;

	private float inTime = 0.9f;   // fator para velocidade de rotacao: quanto maior, mais lenta serah a rotacao (funcao RotateMe)
    private bool waitMove = false; // evitar rotacao maior do que 90 ao pressionar "demais" a tecla 
    private bool snakeAlive = true; // flag to check if the snake is alive, when true snkae is alive 

	//******************************************************

	void Start(){
		
	}

    void Update () {
        if(snakeAlive){             
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
                case 4:
                    Rotate90 ();
                    break;        
            }

            RiseUp ();

            this.changeMoveConfig();
        }

       
    }

    void changeMoveConfig()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            this.option = 1;
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            this.option = 2;
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            this.option = 3;
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            this.option = 4;
        }
    }
	void Rotate90() 
	{
		if (Input.GetKey(KeyCode.DownArrow))
		{
			StartCoroutine (RotateMe (Vector3.right * 90));
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			StartCoroutine (RotateMe (Vector3.left * 90));
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			StartCoroutine (RotateMe (Vector3.down * 90));
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			StartCoroutine (RotateMe (Vector3.up * 90));
		}

		//RiseUp (); //já esta no outro 
	}

	IEnumerator RotateMe(Vector3 byAngles)
	{
		if(!waitMove)
		{
			waitMove = true;

			var fromAngle = transform.rotation;
			var toAngle = Quaternion.Euler (transform.eulerAngles + byAngles);

			for (var t = 0f; t <= 1; t += Time.deltaTime / inTime) 
			{
				transform.rotation = Quaternion.Slerp (fromAngle, toAngle, t);
				yield return null;
			}

			waitMove = false;
		}
	}


    void OriginalMove ()
    {
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

		RiseUp ();
    }
		

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "normalFood")
        {
            //ganha ponto
			Data.addPoints(30); // exemplo
         
		} else if (collision.gameObject.tag == "wall")
        {
			rotateVelocity = 0;
			velocity = 0;
			Data.died();
        }
        //if(collision.gameObject.tag == "specialFood")
        //if(collision.gameObject.tag == "rottenFood")
    }


	void RiseUp() // teste
	{
		if (Data.isAlive()==false && Input.GetKey (KeyCode.Space)) { // ressuscitar/reiniciar
			rotateVelocity = 90;
			velocity = 2;
			Data.rise ();
			// reposicionar
		}
	}
}
