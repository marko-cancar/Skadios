using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class BreadthFirstSearch<TVertex>
{
	private DirectedGraph<TVertex> graph;
	
	public BreadthFirstSearch(DirectedGraph<TVertex> graph)
	{
		this.graph = graph;
	}

	public List<TVertex> GetPath(TVertex source, TVertex target)
	{
		if (this.graph == null || source == null || target == null)
		{
			return new List<TVertex> ();
		}

		HashSet<TVertex> visited = new HashSet<TVertex> { source };
		Dictionary<TVertex, TVertex> predecessors = new Dictionary<TVertex, TVertex> ();		
		Queue<TVertex> queue = new Queue<TVertex> ();
		queue.Enqueue (source);
		while (queue.Count > 0)
		{
			TVertex nextVertexToExpand = queue.Dequeue();			
			foreach (TVertex vertex in graph.GetNeighbours(nextVertexToExpand).Where(vertex => !visited.Contains(vertex)))
			{
				visited.Add(vertex);
				queue.Enqueue(vertex);
				predecessors.Add (vertex, nextVertexToExpand);
				if (vertex.Equals (target))
				{
					break;
				}
			}
		}

		List<TVertex> path = new List<TVertex> ();
		if (predecessors.ContainsKey (target))
		{
			TVertex vertex = target;
			while (!vertex.Equals (source))
			{
				path.Insert(0, vertex);
				vertex = predecessors[vertex];
			}

			path.Insert(0, source);
		}

		return path;
	}
}
