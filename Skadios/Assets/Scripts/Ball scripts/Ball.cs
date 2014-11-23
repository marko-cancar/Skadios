using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
	public enum MoveCommand { UP, DOWN, LEFT, RIGHT };
	private static float moveOffsetX = 0f;
	private static float moveOffsetY = 0f;
	private const float animationDuration = 0.3f;

	private Color ballColor;
	public Color BallColor
	{
		get{
			return ballColor;
		}
		private set{
			//note: base texture should be white 
			ballColor = value;
			GetComponent<SpriteRenderer>().color = value;
		}
	}

	/* REMAINDER: NEVER USE CONSTRUCTORS IF THE CLASS INHERITS MONOBEHAVIOUR
	 * PROOF: http://docs.unity3d.ru/ScriptReference/index.Writing_Scripts_in_Csharp.html
	 * public Ball ()
	{
		BallColor = Color.yellow;
	}

	public Ball (Color initialColor)
	{
		BallColor = initialColor;
	}*/

	public void Initialize (Color color)
	{
		BallColor = color;
	}

	void Start ()
	{

	}

	void Update () 
	{
		if (isClicked ())
		{

		}
	}
	
	private bool isClicked ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			float ballRadius = renderer.bounds.size.x / 2;
			float clickOffset = Mathf.Sqrt(Mathf.Pow(mousePos.x - transform.position.x, 2) + Mathf.Pow(mousePos.y - transform.position.y, 2));
			return (clickOffset <= ballRadius);
		}
		
		return false;
	}

	public void ExecuteMove (MoveCommand command)
	{
		switch (command)
		{
			case MoveCommand.UP: moveUp (); break;
			case MoveCommand.DOWN: moveDown (); break;
			case MoveCommand.LEFT: moveLeft (); break;
			case MoveCommand.RIGHT: moveRight (); break;
			default: Debug.Log ("Something is rotten in the state of Denmark!"); break;
		}
	}

	public void ExecuteMove (IEnumerable<MoveCommand> commands)
	{
		StartCoroutine(ExecuteMoveCoroutine (commands));
	}

	public IEnumerator ExecuteMoveCoroutine (IEnumerable<MoveCommand> commands)
	{
		yield return new WaitForSeconds (0.2f); //initial wait time for testing
		foreach(MoveCommand command in commands)
		{
			Debug.Log ("Next command : " + command);
			ExecuteMove (command);
			yield return new WaitForSeconds (animationDuration); // extract constant
		}
	}

	private void moveUp ()
	{
		Vector3 pos = transform.position;
		pos.y += getMoveOffsetY ();
		transform.position = pos;
	}
	
	private void moveDown ()
	{
		Vector3 pos = transform.position;
		pos.y -= getMoveOffsetY ();
		transform.position = pos;
	}
	
	private void moveLeft ()
	{
		Vector3 pos = transform.position;
		pos.x -= getMoveOffsetX ();
		transform.position = pos;
	}
	
	private void moveRight ()
	{
		Vector3 pos = transform.position;
		pos.x += getMoveOffsetX ();
		transform.position = pos;
	}
	
	private float getMoveOffsetX ()
	{
		if (moveOffsetX == 0f)
		{
			BallsBoard board = GameObject.Find("BallsMainBoard").GetComponent<BallsBoard> ();
			moveOffsetX = board.renderer.bounds.size.x / BallsBoard.GridSize;
		}
		
		return moveOffsetX;
	}

	private float getMoveOffsetY ()
	{
		if (moveOffsetY == 0f)
		{
			BallsBoard board = GameObject.Find("BallsMainBoard").GetComponent<BallsBoard> ();
			moveOffsetY = board.renderer.bounds.size.y / BallsBoard.GridSize;
		}
		
		return moveOffsetY;
	}

}
