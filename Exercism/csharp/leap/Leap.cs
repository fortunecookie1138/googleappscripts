using System;

public static class Leap
{
    public static bool IsLeapYear(int year)
    {
		return (IsEvenlyDivisbleByInt(400, year) || (!IsEvenlyDivisbleByInt(100, year) && IsEvenlyDivisbleByInt(4, year)));
    }
	
	private static bool IsEvenlyDivisbleByInt(int divisor, int year)
	{
		return year % divisor == 0;
	}
}