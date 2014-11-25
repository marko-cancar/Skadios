using System;
using System.Collections.Generic;

public class BallGenerator
{
	public  List<Ball> GenerateBalls (int count)
	{
		List<Ball> balls = new List<Ball>();

		for (int i=0; i<count; i++)
		{
			balls.Add( new Ball());
			// edit ball properties on random
		}

		return balls;
	}
}


