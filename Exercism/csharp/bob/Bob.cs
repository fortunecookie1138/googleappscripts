using System;
using System.Text.RegularExpressions;
using System.Diagnostics;

public static class Bob
{
	public static string Response(string statement)
    {		
		var trimmedStatement = statement.Trim();
		if (IsSilent(trimmedStatement))
		{
			return "Fine. Be that way!";
		}
		if (IsShouting(trimmedStatement))
		{
			return "Whoa, chill out!";
		}
		if (IsQuestion(trimmedStatement))
		{
			return "Sure.";
		}
		return "Whatever.";	
    }	
	
	private static bool IsSilent(string statement)
	{
		return statement.Length == 0;
	}
	
	private static bool IsShouting(string statement)
	{
		var lowerAlphaCharsRegex = new Regex("[a-z]");
		var upperAlphaCharsRegex = new Regex("[A-Z]");
		
		return upperAlphaCharsRegex.IsMatch(statement) && !lowerAlphaCharsRegex.IsMatch(statement);			
	}
	
	private static bool IsQuestion(string statement)
	{
		return statement.EndsWith("?");
	}	
}