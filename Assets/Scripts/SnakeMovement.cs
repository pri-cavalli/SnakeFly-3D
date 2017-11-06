using System.Collections;
using UnityEngine;

public class SnakeMovement : MonoBehaviour {
    public int option = 2;
    public SpawnFood food;

	//static Vector3 init_pos = Vector3(0.0, 0.0, -3.0);

	float rotateVelocity = 180;
	float velocity = 2;

	private float inTime = 0.9f;   // fator para velocidade de rotacao: quanto maior, mais lenta serah a rotacao (funcao RotateMe)
    private bool waitMove = false; // evitar rotacao maior do que 90 ao pressionar "demais" a tecla 
    private bool snakeAlive = true; // flag to check if the snake is alive, when true snkae is alive 
	private bool pause = false;

    int numParts; // contar o tamanho da cobra

	public Color[] palette = new Color[]{ Color.white }; // paleta de cores para as novas partes da cobra, nao sei se tah funcionando!

	//******************************************************

	void Start(){
		pause = false;
		snakeAlive = true;
		numParts = 1;
	}

    void Update () {
		if(snakeAlive && !pause){
            //transform.Translate(Vector3.forward * Time.deltaTime * velocity);
            //moveSnake();
    		switch (option)
            {
                case 1: //MOVE-SE PELO TECLADO E SUBIDA E DECIDA SÃO REALIZADAS POR TRANSIÇÕES, SEM ROTACIONAR
					MovementByKeyRT();
                    break;
                case 2: //MOVE-SE PELO TECLADO E TODOS MOVIMENTOS SÃO POR ROTAÇÃO
					MovementByKeyR();
                    break;
                case 3: //MOVE-SE PELO MOUSE E TODOS MOVIMENTOS SÃO POR ROTAÇÃO
					MovementByMouse();
                    break;
                case 4:
                    Rotate90 ();
                    break;        
            }
            this.changeMoveConfig();
        }

		RiseUp ();

		if (Input.GetKey (KeyCode.Space)) {
			this.pause = !this.pause;
			if (this.pause)
				print ("PAUSED");
			else
				print ("PLAY");
		}

		if (Input.GetKey (KeyCode.G))
			GrowUp ();

		printPosition ();
    }
	//---------------------------------------------------------------------------

	private void changeMoveConfig()
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

	private void MovementByKeyRT()
	{
		if (Input.GetKey(KeyCode.UpArrow)) // W
		{
			transform.Translate(Vector3.up * Time.deltaTime * velocity);
		}
		if (Input.GetKey(KeyCode.DownArrow)) // S
		{
			transform.Translate(Vector3.down * Time.deltaTime * velocity);
		}
		if (Input.GetKey(KeyCode.RightArrow)) // D
		{
			transform.Rotate(Vector3.up * Time.deltaTime * rotateVelocity);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) // A
		{
			transform.Rotate(Vector3.down * Time.deltaTime * rotateVelocity);
		}
	}

	private void MovementByKeyR()
	{
		if (Input.GetKey(KeyCode.RightArrow)) // D
		{
			transform.Rotate(Vector3.up * Time.deltaTime * rotateVelocity);
		}
		if (Input.GetKey(KeyCode.LeftArrow)) // A
		{
			transform.Rotate(Vector3.down * Time.deltaTime * rotateVelocity);
		}
		if (Input.GetKey(KeyCode.DownArrow)) // S
		{
			transform.Rotate(Vector3.right * Time.deltaTime * rotateVelocity);
		}
		if (Input.GetKey(KeyCode.UpArrow)) // W
		{
			transform.Rotate(Vector3.left * Time.deltaTime * rotateVelocity);
		}
	}

	private void MovementByMouse()
	{
		float scalarSpeed = 1.5f;
		Vector3 mousePostion = Input.mousePosition;

		float deltaX = (mousePostion.x - Screen.width / 2) / Screen.width / 2;
		float deltaY = (mousePostion.y - Screen.width / 2) / Screen.width / 2;

		transform.Rotate(Vector3.left * Time.deltaTime * rotateVelocity*deltaY * scalarSpeed);
		transform.Rotate(Vector3.up * Time.deltaTime * rotateVelocity * deltaX  * scalarSpeed);
	}


	private void Rotate90() 
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			tentativa (Vector3.left);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			tentativa (Vector3.right);
		}
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			tentativa (Vector3.down);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			tentativa (Vector3.up);
		}
	}

	private void tentativa(Vector3 vetor)
	{
		waitMove = true;

		int opcaoMov = 2;
		switch (opcaoMov) {
		case 1:
			transform.Rotate (vetor * 90*Time.deltaTime * rotateVelocity);
			break;
		case 2:
			transform.rotation = Quaternion.AngleAxis (90, vetor);
			break;
		case 3:
			StartCoroutine (RotateMe (vetor * 90));
			break;
		}
		waitMove = false;
	}

	IEnumerator RotateMe(Vector3 byAngles)
	{
		if(!waitMove)
		{
			waitMove = true;

			var fromAngle = transform.rotation;
			var toAngle = Quaternion.Euler (transform.eulerAngles + byAngles);
			//float incremento = 0;

			for (var t = 0f; t <= 1; t += Time.deltaTime / inTime) 
			{
				transform.rotation = Quaternion.Lerp (fromAngle, toAngle, t);
				//incremento++;
				//print ("inc = "+incremento);
				yield return null;
			}

			waitMove = false;
		}
	}


	//***********************************************************************

	private void RiseUp() // teste
	{
		if (!snakeAlive && Input.GetKey (KeyCode.R)) { // ressuscitar/reiniciar
			snakeAlive = true;
			transform.position = new Vector3(0, 0, 0); // reposicionar
			rotateVelocity = 90;
			velocity = 2;
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "normalFood")
        {
            //ganha ponto
			Data.addPoints(30); // exemplo
			GrowUp();
         
		} else if (collision.gameObject.tag == "wall")
        {
			rotateVelocity = 0;
			velocity = 0;
			//Data.died();
			snakeAlive = false;
        }
        //if(collision.gameObject.tag == "specialFood")
        //if(collision.gameObject.tag == "rottenFood")
    }

	private void GrowUp()
	{
		GameObject snake = GameObject.Find ("Body" + this.numParts.ToString());
		float x = snake.transform.position.x;
		float y = snake.transform.position.y;
		float z = snake.transform.position.z;
        
        print (this.transform.name +" , grow: body1 (x,y,z) = " + x + ", " + y + ", " + z);

		GameObject newBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		newBody.transform.SetParent(snake.transform);
		newBody.transform.position = new Vector3 (x, y, z);
        newBody.transform.SetPositionAndRotation(snake.transform.position, snake.transform.rotation);
        newBody.transform.Translate(Vector3.back);

		Color color = Color.green;
		newBody.GetComponent<Renderer> ().material.color = color;

		numParts++;
		newBody.name = "Body" + numParts;
		newBody.transform.SetAsLastSibling ();

		/*
		newBody.AddComponent<Animation>();
		newBody.AddComponent<MeshFilter>();
		newBody.AddComponent<MeshCollider> ();*/
	}

	private void printPosition()
	{
		if(Input.GetKey(KeyCode.P))
		{
			GameObject snake = GameObject.Find ("Snake");
			float x = snake.transform.position.x;
			float y = snake.transform.position.y;
			float z = snake.transform.position.z;
			print (this.transform.name +" , snake: (x,y,z) = " + x + ", " + y + ", " + z);
		}
	}

    private void moveSnake()
    {

        GameObject head;
        GameObject tail;
        for (int i =1; i<this.numParts -1; i++) { 
            head = GameObject.Find("Body" + i.ToString());
            tail = GameObject.Find("Body" + (i + 1).ToString());
            tail.transform.SetPositionAndRotation(head.transform.position, head.transform.rotation);
        }
        GameObject snake = GameObject.Find("Body1");
        snake.transform.Translate(Vector3.forward);


        
    }

    private class arrayForMoveSnake
    {

    }

}