// This file was auto-generated based on version 1.0.0 of the canonical data.

using Xunit;

public class LeapTest
{
    [Fact]
    public void Year_not_divisible_by_4_is_common_year()
    {
        Assert.False(Leap.IsLeapYear(2015));
    }

    [Fact]
    public void Year_divisible_by_4_not_divisible_by_100_is_leap_year()
    {
        Assert.True(Leap.IsLeapYear(2016));
    }

    [Fact]
    public void Year_divisible_by_100_not_divisible_by_400_is_common_year()
    {
        Assert.False(Leap.IsLeapYear(2100));
    }

    [Fact]
    public void Year_divisible_by_400_is_leap_year()
    {
        Assert.True(Leap.IsLeapYear(2000));
    }
	
	[Fact]
	public void OtherYearsFromReadMe()
	{
		Assert.False(Leap.IsLeapYear(1997), "1997 should not be a leap year");
		Assert.True(Leap.IsLeapYear(1996), "1996 should be a leap year");
		Assert.False(Leap.IsLeapYear(1990), "1990 should not be a leap year");
		Assert.True(Leap.IsLeapYear(2000), "2000 should be a leap year");
	}
}