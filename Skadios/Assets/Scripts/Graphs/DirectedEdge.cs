using UnityEngine;
using System.Collections;

class DirectedEdge<TVertex>
{
	public TVertex Source { get; private set; }
	public TVertex Target { get; private set; }
	
	public DirectedEdge(TVertex source, TVertex target)
	{
		this.Source = source;
		this.Target = target;
	}
}
