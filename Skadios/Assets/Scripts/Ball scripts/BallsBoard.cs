 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallsBoard : MonoBehaviour
{
	public const int GridSize = 10;
	private DirectedGraph<BallField> connectityGraph;
	public BallField[,] fields;
	private float fieldSize;
	private float leftX;
	private float rightX;
	private float topY;
	private float bottomY;
	
	void Start ()
	{
		this.initializeBallFields ();
		this.initializeConnectivityGraph ();
		this.fieldSize = renderer.bounds.size.x;
		this.leftX = transform.position.x - (fieldSize * (GridSize / 2));
		this.topY = transform.position.x + (fieldSize * (GridSize / 2));
		this.rightX = transform.position.y + (fieldSize * (GridSize / 2));
		this.bottomY = transform.position.y - (fieldSize * (GridSize / 2));
	}

	void Update ()
	{

	}

	private void initializeBallFields ()
	{
		this.fields = new BallField[GridSize, GridSize];
		for (int i = 0; i < GridSize; i++) {
			for (int j = 0; j < GridSize; j++) {
				this.fields [i, j] = new BallField (i, j);
			}
		}
	}

	private void initializeConnectivityGraph ()
	{
		this.connectityGraph = new DirectedGraph<BallField> ();
		foreach (BallField field in this.fields)
		{
			this.updateConnections (field);
		}
	}

	private void updateConnections (BallField field)
	{
		List<BallField> neighbours = this.getNeighbours (field);
		foreach (BallField neighbour in neighbours)
		{
			if (neighbour.Ball == null)
			{
				this.connectityGraph.AddEdge (field, neighbour);
			}
			else
			{
				this.connectityGraph.RemoveEdge (field, neighbour);
			}

			if (field.Ball == null)
			{
				this.connectityGraph.AddEdge (neighbour, field);
			}
			else
			{
				this.connectityGraph.RemoveEdge (neighbour, field);
			}
		}
	}
	
	List<BallField> getNeighbours (BallField field)
	{
		int i = field.Row;
		int j = field.Column;
		List<BallField> neighbours = new List<BallField> ();
		BallField topNeighbour = (i > 0) ? this.fields[i - 1, j] : null;
		BallField bottomNeighbour = (i < GridSize - 1) ? this.fields[i + 1, j] : null;
		BallField leftNeighbour = (j > 0) ? this.fields[i, j - 1] : null;
		BallField rightNeighbour = (j < GridSize - 1) ? this.fields[i, j + 1] : null;

		if (topNeighbour != null)
		{
			neighbours.Add(topNeighbour);
		}

		if (bottomNeighbour != null)
		{
			neighbours.Add(bottomNeighbour);
		}

		if (leftNeighbour != null)
		{
			neighbours.Add(leftNeighbour);
		}

		if (rightNeighbour != null)
		{
			neighbours.Add(rightNeighbour);
		}

		return neighbours;
	}
	
	private List<Ball.MoveCommand> getMoveCommands(BallField source, BallField target)
	{
		BreadthFirstSearch<BallField> bfs = new BreadthFirstSearch<BallField> (connectityGraph);
		List<BallField> path = bfs.GetPath (source, target);
		List<Ball.MoveCommand> commands = new List<Ball.MoveCommand> ();
		for (int i = 0; i < path.Count - 1; i++)
		{
			switch (path[i].Row - path[i + 1].Row)
			{
			case -1:
				commands.Add (Ball.MoveCommand.DOWN);
				break;
			case 0:
				switch (path[i].Column - path[i + 1].Column)
				{
				case -1: commands.Add (Ball.MoveCommand.RIGHT); break;
				case 1: commands.Add (Ball.MoveCommand.LEFT); break;
				default: Debug.Log ("Something is rotten in the state of Denmark!"); break;
				}
				break;
			case 1:
				commands.Add (Ball.MoveCommand.UP);
				break;
			default: Debug.Log ("Something is rotten in the state of Denmark!"); break;
			}
		}
		
		return commands;
	}

	BallField getClickedField()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		if (clickOutOfBounds (mousePosition))
		{
			return null;
		}

	    int row = (int)((topY - mousePosition.y) / fieldSize);
		int column = (int)((mousePosition.x - leftX) / fieldSize);
		return fields[row, column];
	}

	private bool clickOutOfBounds (Vector3 mousePosition)
	{
		return mousePosition.y > topY || mousePosition.y < bottomY || mousePosition.x < leftX || mousePosition.x > rightX;
	}
}
