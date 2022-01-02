namespace TokanPages.Tests.UnitTests.Services;

using Xunit;
using System;
using FluentAssertions;
using Backend.Core.Utilities.DateTimeService;

public class DateTimeServiceTest
{
    private const int Year = 2020;
    private const int Month = 10;
    private const int Day = 15;
    private const int Hour = 9;
    private const int Minute = 30;
    private const int Second = 30;
        
    [Fact]
    public void GivenDateTime_WhenInvokeGetStartOfDay_ShouldReturnSameDateAndTimeAtMidnight()
    {
        // Arrange
        var dateTime = new DateTime(Year, Month, Day, Hour, Minute, Second);
        var dateTimeService = new DateTimeService();

        // Act
        var result = dateTimeService.GetStartOfDay(dateTime);

        // Assert
        result.Should().HaveYear(Year);
        result.Should().HaveMonth(Month);
        result.Should().HaveDay(Day);
            
        result.Should().HaveHour(0);
        result.Should().HaveMinute(0);
        result.Should().HaveSecond(0);
    }
        
    [Fact]
    public void GivenDateTime_WhenInvokeGetEndOfDay_ShouldReturnSameDateAndTimeBeforeMidnight()
    {
        // Arrange
        var dateTime = new DateTime(Year, Month, Day, Hour, Minute, Second);
        var dateTimeService = new DateTimeService();

        // Act
        var result = dateTimeService.GetEndOfDay(dateTime);

        // Assert
        result.Should().HaveYear(Year);
        result.Should().HaveMonth(Month);
        result.Should().HaveDay(Day);
            
        result.Should().HaveHour(23);
        result.Should().HaveMinute(59);
        result.Should().HaveSecond(59);
    }
        
    [Fact]
    public void GivenDateTime_WhenInvokeGetFirstDayOfMonth_ShouldReturnSameFirstDayOfGivenDate()
    {
        // Arrange
        var dateTime = new DateTime(Year, Month, Day, Hour, Minute, Second);
        var dateTimeService = new DateTimeService();

        // Act
        var result = dateTimeService.GetFirstDayOfMonth(dateTime);

        // Assert
        result.Should().HaveYear(Year);
        result.Should().HaveMonth(Month);
        result.Should().HaveDay(1);
            
        result.Should().HaveHour(0);
        result.Should().HaveMinute(0);
        result.Should().HaveSecond(0);
    }

    [Fact]
    public void GivenTodayEndOfDayAndTodayStartOfDay_WhenMeasuringDifference_ShouldReturnSecondBeforeMidnight()
    {
        // Arrange
        var dateTimeService = new DateTimeService();

        // Act
        var todayStartOfDay = dateTimeService.TodayStartOfDay;
        var todayEndOfDay = dateTimeService.TodayEndOfDay;

        var difference = todayEndOfDay - todayStartOfDay;
            
        // Assert
        difference.Hours.Should().Be(23);
        difference.Minutes.Should().Be(59);
        difference.Seconds.Should().Be(59);
    }
}