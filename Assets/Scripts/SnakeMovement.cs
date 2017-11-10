using System.Collections;
using UnityEngine;
using System.Collections.Generic;

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

    private ArrayForSnakeMoviment listOfPositions = new ArrayForSnakeMoviment(); 

    int numParts; // contar o tamanho da cobra


    //********************************************************************
    // For coding purposes ONLY
    // If you wanna see the snake growing and moving like as an grown up snake set this attribute as true
    private bool moveSnakeAsGrow = true;
    //****************************************************************/
	public Color[] palette = new Color[]{ Color.white }; // paleta de cores para as novas partes da cobra, nao sei se tah funcionando!

	//******************************************************

	void Start(){
		pause = false;
		snakeAlive = true;
		numParts = 1;
        listOfPositions.addPart(new NodeForSnakeMoviment("Body1", transform.position, transform.rotation));
	}

    void Update () {
		if(snakeAlive && !pause){
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
            if (moveSnakeAsGrow){
                moveSnake();
            }
            else{
                transform.Translate(Vector3.forward * Time.deltaTime * velocity);

            }
        }

		RiseUp ();

		if (Input.GetKey (KeyCode.Space)) {
			this.pause = !this.pause;
			if (this.pause)
				print ("PAUSED");
			else
				print ("PLAY");
		}

		if (Input.GetKeyDown (KeyCode.G))
			GrowUp ();

		//printPosition ();
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
			//Data.addPoints(30); // exemplo
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
        
        //  print (this.transform.name +" , grow: body1 (x,y,z) = " + x + ", " + y + ", " + z);

		GameObject newBody = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
        if (!this.moveSnakeAsGrow) { 
            newBody.transform.SetParent(snake.transform);
        }
        

        
        newBody.transform.SetPositionAndRotation(snake.transform.position, snake.transform.rotation);
        newBody.transform.Translate(Vector3.back);
        
		Color color = Color.green;
		newBody.GetComponent<Renderer> ().material.color = color;

		numParts++;
		newBody.name = "Body" + numParts;
		newBody.transform.SetAsLastSibling ();
        //newBody.collid
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

        float time = Time.deltaTime;
        for (int i = 1; i<=this.numParts; i++) { 
            head = GameObject.Find("Body" + i.ToString());
            NodeForSnakeMoviment thisNode = new NodeForSnakeMoviment(head.name, head.transform.position, head.transform.rotation);
                head.transform.Translate(Vector3.forward * time * velocity);
            NodeForSnakeMoviment nextMove = this.listOfPositions.getNextMove(thisNode, "Body" + (i - 1).ToString());

            //print(i + "origi" + head.transform.position);
            if ( nextMove != null )
            {
                //print(i + "Falas" + nextMove.getPosition());
                
                head.transform.rotation = nextMove.getRotation();
                head.transform.position = nextMove.getPosition();
                this.listOfPositions.delete(nextMove);
            }
            
            thisNode = new NodeForSnakeMoviment(head.name, head.transform.position, head.transform.rotation);
            this.listOfPositions.addPart(thisNode);

        }
        


        
    }















    private class NodeForSnakeMoviment
    {
        string headName;
        Vector3 position;
        Quaternion rotation;
        int count;
      
        public NodeForSnakeMoviment(string name, Vector3 position, Quaternion rotation)
        {
            
            this.headName = name;
            this.position = position;
            this.rotation = rotation;
            this.count = 20;
        }
        public string getName()
        {
            return this.headName;
        }

        public Vector3 getPosition()
        {
            return this.position;
        }

        public Quaternion getRotation()
        {
            return this.rotation;
        }

        public int getCount()
        {
            return this.count;
        }

        public void decCount()
        {
            this.count--;
        }
    }
    private class ArrayForSnakeMoviment
    {
        private List<NodeForSnakeMoviment> arrayForMove;

        public ArrayForSnakeMoviment()
        {
            this.arrayForMove = new List<NodeForSnakeMoviment>();
        }

        public int count()
        {
            return this.arrayForMove.Count;
        }

        public void addPart(NodeForSnakeMoviment newPart)
        {
            this.arrayForMove.Add(newPart);
        }
        public NodeForSnakeMoviment getNextMove(NodeForSnakeMoviment part, string partFatherName)
        {
            int minIndex = -1;
            float minValue = 0, auxValue;
            NodeForSnakeMoviment aux;
            for (int i = this.arrayForMove.Count - 1; i >= 0; i--    )
            {
                aux = this.arrayForMove[i];
                if (aux.getName() == partFatherName)
                {
                    auxValue = distVector(aux.getPosition(), part.getPosition());
                    if (minIndex == -1 && auxValue <=1.0f)
                    {
                        minIndex = i;
                        minValue = auxValue;
                    }
                    else
                    {
                        
                        if (auxValue < minValue && auxValue <= 1.0f )
                        {
                            minIndex = i;
                            minValue = auxValue;
                        }
                        if(minValue == 2.0f)
                        {
                            print("SSSSSSSSSS");
                            //return this.arrayForMove[minIndex];
                        }
                    }


                }
                else if (aux.getCount() == 0)
                {
                    minIndex--;
                    if (!this.arrayForMove.Remove(aux))
                    {
                        print("ERRRO");
                    }
                }
                else
                {

                    aux.decCount();
                }
            }
            if(minIndex <= -1)
            {
                return null;

            }
            else
            {
                //print("foiiiiiiiiiiiiiiiiii " + this.arrayForMove[minIndex].getPosition());
                //print(minIndex);
                //print("max "  + this.arrayForMove.Count);
                return this.arrayForMove[minIndex];
            }
        }

        public void delete(NodeForSnakeMoviment x)
        {
            this.arrayForMove.Remove(x);
        }
        private float distVector(Vector3 v1, Vector3 v2)
        {
            //print("V1: " + v1+ " v2: " +  v2);
            return Mathf.Sqrt(Mathf.Pow((v1.x - v2.x), 2) + Mathf.Pow((v1.y - v2.y), 2) + Mathf.Pow((v1.z - v2.z), 2));
        }

        private bool vectorsCloseEnough(Vector3 v1, Vector3 v2)
        {
            print("V1: " + v1);
            print("v2: " + v2);
            float diff = 0.05f;
            if(v1.x - v2.x > -diff && v1.x - v2.x < diff)
            {
                if (v1.y - v2.y > -diff && v1.y - v2.y < diff)
                {
                    if (v1.z - v2.z > -diff && v1.z - v2.z < diff)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


    }

}