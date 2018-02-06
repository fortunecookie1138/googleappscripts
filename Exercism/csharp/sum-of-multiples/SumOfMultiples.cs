using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        if (multiples.All(m => m >= max))
		{
			return 0;
		}
		
		var numbersToSum = new HashSet<int>();
		foreach(var multiple in multiples)
		{
			var currentNumber = multiple;
			for(var i = 2; currentNumber < max; i++)
			{
				numbersToSum.Add(currentNumber);
				currentNumber = multiple * i;
			}
		}
		
		return numbersToSum.Sum();
    }
}