namespace TokanPages.Backend.Tests.Services
{
    using System;
    using TokanPages.Backend.Shared.Services.DateTimeService;
    using FluentAssertions;
    using Xunit;

    public class DateTimeServiceTest
    {
        private const int YEAR = 2020;
        private const int MONTH = 10;
        private const int DAY = 15;
        private const int HOUR = 9;
        private const int MINUTE = 30;
        private const int SECOND = 30;
        
        [Fact]
        public void GivenDateTime_WhenInvokeGetStartOfDay_ShouldReturnSameDateAndTimeAtMidnight()
        {
            // Arrange
            var LDateTime = new DateTime(YEAR, MONTH, DAY, HOUR, MINUTE, SECOND);
            var LDateTimeService = new DateTimeService();

            // Act
            var LResult = LDateTimeService.GetStartOfDay(LDateTime);

            // Assert
            LResult.Should().HaveYear(YEAR);
            LResult.Should().HaveMonth(MONTH);
            LResult.Should().HaveDay(DAY);
            
            LResult.Should().HaveHour(0);
            LResult.Should().HaveMinute(0);
            LResult.Should().HaveSecond(0);
        }
        
        [Fact]
        public void GivenDateTime_WhenInvokeGetEndOfDay_ShouldReturnSameDateAndTimeBeforeMidnight()
        {
            // Arrange
            var LDateTime = new DateTime(YEAR, MONTH, DAY, HOUR, MINUTE, SECOND);
            var LDateTimeService = new DateTimeService();

            // Act
            var LResult = LDateTimeService.GetEndOfDay(LDateTime);

            // Assert
            LResult.Should().HaveYear(YEAR);
            LResult.Should().HaveMonth(MONTH);
            LResult.Should().HaveDay(DAY);
            
            LResult.Should().HaveHour(23);
            LResult.Should().HaveMinute(59);
            LResult.Should().HaveSecond(59);
        }
        
        [Fact]
        public void GivenDateTime_WhenInvokeGetFirstDayOfMonth_ShouldReturnSameFirstDayOfGivenDate()
        {
            // Arrange
            var LDateTime = new DateTime(YEAR, MONTH, DAY, HOUR, MINUTE, SECOND);
            var LDateTimeService = new DateTimeService();

            // Act
            var LResult = LDateTimeService.GetFirstDayOfMonth(LDateTime);

            // Assert
            LResult.Should().HaveYear(YEAR);
            LResult.Should().HaveMonth(MONTH);
            LResult.Should().HaveDay(1);
            
            LResult.Should().HaveHour(0);
            LResult.Should().HaveMinute(0);
            LResult.Should().HaveSecond(0);
        }

        [Fact]
        public void GivenTodayEndOfDayAndTodayStartOfDay_WhenMeasuringDifference_ShouldReturnSecondBeforeMidnight()
        {
            // Arrange
            var LDateTimeService = new DateTimeService();

            // Act
            var LTodayStartOfDay = LDateTimeService.TodayStartOfDay;
            var LTodayEndOfDay = LDateTimeService.TodayEndOfDay;

            var LDifference = LTodayEndOfDay - LTodayStartOfDay;
            
            // Assert
            LDifference.Hours.Should().Be(23);
            LDifference.Minutes.Should().Be(59);
            LDifference.Seconds.Should().Be(59);
        }
    }
}