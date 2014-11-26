using UnityEngine;
using System.Collections;

public class BallField
{
	public readonly int Row;
	public readonly int Column;
	public Ball Ball { get; set; }
	private GameObject ballObject;
	private Vector2 position;

	public BallField(int row, int column, bool shouldSpawnBall)
	{
		this.Row = row;
		this.Column = column;
		this.position = calculateBallPosition();

		if (shouldSpawnBall) SpawnBall();
	}

	public BallField(int row, int column)
	{
		this.Row = row;
		this.Column = column;
		this.position = calculateBallPosition();

		this.ballObject = null;
		this.Ball = null;
	}

	public void SpawnBall ()
	{
		this.ballObject = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BallPrefab")) as GameObject;
		this.Ball = ballObject.GetComponent<Ball>();
		this.Ball.Initialize(Color.red, position);
	}

	private Vector2 calculateBallPosition()
	{
		BallsBoard board = GameObject.Find("BallsMainBoard").GetComponent<BallsBoard> ();

		float xOffset = board.renderer.bounds.size.x / 2;
		float yOffset = board.renderer.bounds.size.y / 2;
		float fieldSizeWidth = board.renderer.bounds.size.x / BallsBoard.GridSize;
		float fieldSizeHeight = board.renderer.bounds.size.y / BallsBoard.GridSize;

		return new Vector2(Row*fieldSizeWidth - xOffset + fieldSizeWidth/2, Column*fieldSizeHeight - yOffset + fieldSizeHeight/2);
	}
}

