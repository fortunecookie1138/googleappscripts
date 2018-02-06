using System;
using System.Text;

public static class Raindrops
{
    public static string Convert(int number)
    {
        var raindropString = $"{AddPling(number)}{AddPlang(number)}{AddPlong(number)}";
		return raindropString == string.Empty
			? raindropString = number.ToString()
			: raindropString;
    }
	
	private static string AddPling(int number)
	{
		return number % 3 == 0 ? "Pling" : string.Empty;
	}
	
	private static string AddPlang(int number)
	{
		return number % 5 == 0 ? "Plang" : string.Empty;
	}
	
	private static string AddPlong(int number)
	{
		return number % 7 == 0 ? "Plong" : string.Empty;
	}
}