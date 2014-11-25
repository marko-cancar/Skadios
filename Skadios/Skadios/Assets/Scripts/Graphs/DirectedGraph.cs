using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DirectedGraph<TVertex>
{
	private Dictionary<TVertex, List<DirectedEdge<TVertex>>> adjacencyList;
	
	public int VertexCount { get; protected set; }
	
	public int EdgeCount { get; protected set; }
	
	public DirectedGraph()
	{
		this.adjacencyList = new Dictionary<TVertex, List<DirectedEdge<TVertex>>>();
		this.VertexCount = 0;
		this.EdgeCount = 0;
	}
	
	public void AddVertex(TVertex vertex)
	{
		if (vertex == null)
		{
			throw new ArgumentNullException();
		}
		
		if (!this.ContainsVertex(vertex))
		{
			this.adjacencyList.Add(vertex, new List<DirectedEdge<TVertex>>());
			this.VertexCount++;
		}
	}
	
	public void RemoveVertex(TVertex vertex)
	{
		if (vertex == null)
		{
			throw new ArgumentNullException();
		}
		
		if (this.ContainsVertex(vertex))
		{
			foreach (List<DirectedEdge<TVertex>> edges in this.adjacencyList.Values)
			{
				edges.RemoveAll(e => e.Target.Equals(vertex));
			}
			
			this.adjacencyList.Remove(vertex);
			this.VertexCount--;
		}
	}
	
	public bool ContainsVertex(TVertex vertex)
	{
		if (vertex == null)
		{
			throw new ArgumentNullException();
		}
		
		return this.adjacencyList.ContainsKey(vertex);
	}
	
	public IEnumerable<TVertex> GetNeighbours(TVertex vertex)
	{
		if (vertex == null)
		{
			throw new ArgumentNullException();
		}
		
		return this.ContainsVertex(vertex) ? this.adjacencyList[vertex].Select(edge => edge.Target) : Enumerable.Empty<TVertex>();
	}
	
	public void AddEdge(TVertex source, TVertex target)
	{
		if (source == null || target == null)
		{
			throw new ArgumentNullException();
		}
		
		if (!this.ContainsVertex(source) || !this.ContainsVertex(target))
		{
			throw new ArgumentException();
		}
		
		if (!this.ContainsEdge(source, target))
		{
			DirectedEdge<TVertex> edge = new DirectedEdge<TVertex>(source, target);
			this.adjacencyList[source].Add(edge);
			this.EdgeCount++;
		}
	}
	
	public void RemoveEdge(TVertex source, TVertex target)
	{
		if (source == null || target == null)
		{
			throw new ArgumentNullException();
		}
		
		if (this.ContainsEdge(source, target))
		{
			this.adjacencyList[source].RemoveAll(e => e.Target.Equals(target));
			this.EdgeCount--;
		}
	}
	
	private bool ContainsEdge(TVertex source, TVertex target)
	{
		if (source == null || target == null)
		{
			throw new ArgumentNullException();
		}
		
		return this.ContainsVertex(source) && this.ContainsVertex(target)
			&& this.adjacencyList[source].Any(e => e.Target.Equals(target));
	}
}
