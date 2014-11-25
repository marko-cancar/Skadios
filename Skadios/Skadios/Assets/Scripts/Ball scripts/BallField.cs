using UnityEngine;
using System.Collections;

public class BallField
{
	public readonly int Row;
	public readonly int Column;
	public Ball Ball { get; set; }
	private GameObject ball;

	public BallField(int row, int column)
	{
		this.Row = row;
		this.Column = column;

		//TODO: GameObject positioning. All spawn on the same point
		this.ball = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BallPrefab")) as GameObject;
		this.Ball = ball.GetComponent<Ball>();
		this.Ball.Initialize(Color.red);
	}
}
