using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Obacher.Framework.Common.SystemWrapper;
using Obacher.Framework.Common.SystemWrapper.Interface;
using Should.Fluent;

namespace Obacher.Framework.Common.UnitTest.SystemWrapper
{
    [TestClass]
    public class DateTimeWrapTest
    {
        [TestMethod]
        public void WhenDateTimeValuePassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenLongValuePassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            long expected = DateTime.Now.Ticks;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.DateTimeInstance.Ticks;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenLongAndKindValuePassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            long expected = DateTime.Now.Ticks;
            DateTimeKind expectedKind = DateTimeKind.Local;

            // Act
            var target = new DateTimeWrap(expected, expectedKind);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Ticks.Should().Equal(expected);
            actual.Kind.Should().Equal(expectedKind);
        }

        [TestMethod]
        public void WhenYearMonthDayPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            var expected = new DateTime(2012, 10, 15);

            // Act
            var target = new DateTimeWrap(expected.Year, expected.Month, expected.Day);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearMonthDayCalendarPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            var calendar = new HebrewCalendar();
            var expected = new DateTime(5776, 8, 17, calendar);

            // Act
            var target = new DateTimeWrap(5776, 8, 17, calendar);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearMonthDayHoursMinutesSecondsPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            var expected = new DateTime(2015, 10, 15, 23, 15, 15);

            // Act
            var target = new DateTimeWrap(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, expected.Second);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearMonthDayHoursMinutesSecondsKindPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            var calendar = new HebrewCalendar();
            var expected = new DateTime(5776, 8, 17, 23, 15, 15, calendar);

            // Act
            var target = new DateTimeWrap(5776, 8, 17, 23, 15, 15, calendar);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearMonthDayHoursMinutesSecondsCalendarPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            DateTimeKind expectedKind = DateTimeKind.Local;
            var expected = new DateTime(2015, 10, 15, 23, 15, 15, expectedKind);

            // Act
            var target = new DateTimeWrap(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, expected.Second, expectedKind);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
            expectedKind.Should().Equal(actual.Kind);
        }

        [TestMethod]
        public void WhenYearMonthDayHoursMinutesSecondsMillisecondPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            var expected = new DateTime(2015, 10, 15, 23, 15, 15, 151);

            // Act
            var target = new DateTimeWrap(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, expected.Second, expected.Millisecond);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearMonthDayHoursMinutesSecondsMillisecondKindPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            DateTimeKind expectedKind = DateTimeKind.Local;
            var expected = new DateTime(2015, 10, 15, 23, 15, 15, 151);

            // Act
            var target = new DateTimeWrap(expected.Year, expected.Month, expected.Day, expected.Hour, expected.Minute, expected.Second, expected.Millisecond, expectedKind);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
            expectedKind.Should().Equal(actual.Kind);
        }

        [TestMethod]
        public void WhenYearMonthDayHoursMinutesSecondsMillisecondCalendarPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            var calendar = new HebrewCalendar();
            var expected = new DateTime(5776, 8, 17, 23, 15, 15, 151, calendar);

            // Act
            var target = new DateTimeWrap(5776, 8, 17, 23, 15, 15, 151, calendar);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearMonthDayHoursMinutesSecondsMillisecondCalendarKindPassedIntoConstructor_ExpectSameValueReturned()
        {
            // Arrange
            var calendar = new HebrewCalendar();
            DateTimeKind expectedKind = DateTimeKind.Local;
            var expected = new DateTime(5776, 8, 17, 23, 15, 15, 151, calendar, expectedKind);

            // Act
            var target = new DateTimeWrap(5776, 8, 17, 23, 15, 15, 151, calendar, expectedKind);
            var actual = target.DateTimeInstance;

            // Assert
            actual.Should().Equal(expected);
            expectedKind.Should().Equal(actual.Kind);
        }

        [TestMethod]
        public void WhenDateCalled_ExpectDateReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Date;

            // Assert
            actual.Year.Should().Equal(expected.Year);
            actual.Month.Should().Equal(expected.Month);
            actual.Day.Should().Equal(expected.Day);
        }

        [TestMethod]
        public void WhenDayCalled_ExpectSameDayReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Day;

            // Assert
            actual.Should().Equal(expected.Day);
        }

        [TestMethod]
        public void WhenDayOfWeekCalled_ExpectSameDayOfWeekReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.DayOfWeek;

            // Assert
            actual.Should().Equal(expected.DayOfWeek);
        }

        [TestMethod]
        public void WhenDayOfYearCalled_ExpectSameDayOfYearReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.DayOfYear;

            // Assert
            actual.Should().Equal(expected.DayOfYear);
        }

        [TestMethod]
        public void WhenHourCalled_ExpectSameHourReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Hour;

            // Assert
            actual.Should().Equal(expected.Hour);
        }

        [TestMethod]
        public void WhenMillisecondCalled_ExpectSameMillisecondReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Millisecond;

            // Assert
            actual.Should().Equal(expected.Millisecond);
        }

        [TestMethod]
        public void WhenMinuteCalled_ExpectSameMinuteReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Minute;

            // Assert
            actual.Should().Equal(expected.Minute);
        }

        [TestMethod]
        public void WhenMonthCalled_ExpectSameMonthReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Month;

            // Assert
            actual.Should().Equal(expected.Month);
        }

        [TestMethod]
        public void WhenNowCalled_ExpectFakedDateReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            using (ShimsContext.Create())
            {
                ShimDateTime.NowGet = () => expected;

                // Act
                var target = new DateTimeWrap();
                var actual = target.Now;

                // Assert
                actual.DateTimeInstance.Should().Equal(expected);
            }
        }

        [TestMethod]
        public void WhenSecondCalled_ExpectSameSecondReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Second;

            // Assert
            actual.Should().Equal(expected.Second);
        }

        [TestMethod]
        public void WhenTicksCalled_ExpectSameTicksReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.Ticks;

            // Assert
            actual.Should().Equal(expected.Ticks);
        }

        [TestMethod]
        public void WhenTimeOfDayCalled_ExpectSameTimeOfDayReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.TimeOfDay;

            // Assert
            actual.Should().Equal(expected.TimeOfDay);
        }

        [TestMethod]
        public void WhenTodayCalled_ExpectFakedTodayReturned()
        {
            // Arrange
            DateTime expected = DateTime.Today;

            using (ShimsContext.Create())
            {
                ShimDateTime.TodayGet = () => expected;

                // Act
                var target = new DateTimeWrap();
                var actual = target.Today;

                // Assert
                actual.DateTimeInstance.Should().Equal(expected);
            }
        }

        [TestMethod]
        public void WhenUtcNowCalled_ExpectFakedUtcNowReturned()
        {
            // Arrange
            DateTime expected = DateTime.UtcNow;

            using (ShimsContext.Create())
            {
                ShimDateTime.UtcNowGet = () => expected;

                // Act
                var target = new DateTimeWrap();
                var actual = target.UtcNow;

                // Assert
                actual.DateTimeInstance.Should().Equal(expected);
            }
        }

        [TestMethod]
        public void WhenYearCalled_ExpectSameYearReturned()
        {
            // Arrange
            DateTime expected = DateTime.Now;

            // Act
            var target = new DateTimeWrap(expected);
            var actual = target.TimeOfDay;

            // Assert
            actual.Should().Equal(expected.TimeOfDay);
        }

        [TestMethod]
        public void WhenAddCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            var addedTimeSpan = TimeSpan.FromDays(15);
            var expected = now.Add(addedTimeSpan);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.Add(addedTimeSpan);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddDaysCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 15;
            var expected = now.AddDays(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddDays(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddHoursCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 15;
            var expected = now.AddHours(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddHours(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddMillisecondsCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 15;
            var expected = now.AddMilliseconds(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddMilliseconds(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddMinutesCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 15;
            var expected = now.AddMinutes(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddMinutes(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddMonthsCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 15;
            var expected = now.AddMonths(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddMonths(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddSecondsCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 15;
            var expected = now.AddSeconds(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddSeconds(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddTicksCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 1554566;
            var expected = now.AddTicks(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddTicks(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenAddYearsCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const int added = 15;
            var expected = now.AddYears(added);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.AddYears(added);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenCompareEqualDates_ExpectZeroReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            var date2 = new DateTime(2015, 10, 15);
            var expected = 0;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.Compare(target1, target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenDate1LessThanDate2_ExpectNegativeOneReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15).AddDays(-3);
            var date2 = new DateTime(2015, 10, 15);
            var expected = -1;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.Compare(target1, target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenDate1GreaterThanDate2_ExpectOneReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            var date2 = new DateTime(2015, 10, 15).AddDays(-3);
            var expected = 1;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.Compare(target1, target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenCompareToEqualDates_ExpectZeroReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            var date2 = new DateTime(2015, 10, 15);
            var expected = 0;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.CompareTo(target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenCompareToDate1LessThanDate2_ExpectNegativeOneReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15).AddDays(-3);
            var date2 = new DateTime(2015, 10, 15);
            var expected = -1;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.CompareTo(target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenCompareToDate1GreaterThanDate2_ExpectOneReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            var date2 = new DateTime(2015, 10, 15).AddDays(-3);
            const int expected = 1;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.CompareTo(target2);

            // Assert
            actual.Should().Equal(expected);
        }


        [TestMethod]
        public void WhenObjectCompareToEqualDates_ExpectZeroReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            object date2 = new DateTime(2015, 10, 15);
            var expected = 0;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = date2;
            var actual = target1.CompareTo(target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenObjectCompareToDate1LessThanDate2_ExpectNegativeOneReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15).AddDays(-3);
            object date2 = new DateTime(2015, 10, 15);
            var expected = -1;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = date2;
            var actual = target1.CompareTo(target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenObjectCompareToDate1GreaterThanDate2_ExpectOneReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            object date2 = new DateTime(2015, 10, 15).AddDays(-3);
            var expected = 1;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = date2;
            var actual = target1.CompareTo(target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenDaysInMonthCalled_ExpectSameYearReturned()
        {
            // Arrange
            var now = DateTime.Now;
            var expected = DateTime.DaysInMonth(now.Year, now.Month);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.DaysInMonth(now.Year, now.Month);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenEqualsCalled_ExpectFalseReturned()
        {
            // Arrange
            var now = DateTime.Now;
            const bool expected = false;

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.Equals(target);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenObjectEqualsDate1EqualsDate2_ExpectTrueReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            object date2 = new DateTime(2015, 10, 15);
            const bool expected = true;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = date2;
            var actual = target1.Equals(target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenObjectNotEqualsDate1EqualsDate2_ExpectFalseReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            object date2 = new DateTime(2015, 10, 15).AddDays(15);
            const bool expected = false;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = date2;
            var actual = target1.Equals(target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenEqualsDate1EqualsDate2_ExpectTrueReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            var date2 = new DateTime(2015, 10, 15);
            const bool expected = true;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.Equals(target1, target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenNotEqualsDate1EqualsDate2_ExpectTrueReturned()
        {
            // Arrange
            var date1 = new DateTime(2015, 10, 15);
            var date2 = new DateTime(2015, 10, 15).AddDays(15);
            const bool expected = false;

            // Act
            var target1 = new DateTimeWrap(date1);
            var target2 = new DateTimeWrap(date2);
            var actual = target1.Equals(target1, target2);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenFromBinaryCalled_ExpectSameValueReturned()
        {
            // Arrange
            const long fileTime = 12345578;
            var expected = DateTime.FromBinary(fileTime);

            // Act
            var target = new DateTimeWrap();
            var actual = target.FromBinary(fileTime);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenFromFileTimeCalled_ExpectSameValueReturned()
        {
            // Arrange
            const long fileTime = 12345578;
            var expected = DateTime.FromFileTime(fileTime);

            // Act
            var target = new DateTimeWrap();
            var actual = target.FromFileTime(fileTime);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenFromFileTimeUtcCalled_ExpectSameValueReturned()
        {
            // Arrange
            const long fileTime = 12345578;
            var expected = DateTime.FromFileTimeUtc(fileTime);

            // Act
            var target = new DateTimeWrap();
            var actual = target.FromFileTimeUtc(fileTime);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenFromOADateUtcCalled_ExpectSameValueReturned()
        {
            // Arrange
            const double oaTime = 30000;
            var expected = DateTime.FromOADate(oaTime);

            // Act
            var target = new DateTimeWrap();
            var actual = target.FromOADate(oaTime);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenGetDateTimeFormatsCalled_ExpectSameValueReturned()
        {
            // Arrange
            var today = DateTime.Today;
            var expected = today.GetDateTimeFormats();

            // Act
            var target = new DateTimeWrap(today);
            var actual = target.GetDateTimeFormats();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenGetDateTimeFormatsWithCharCalled_ExpectSameValueReturned()
        {
            // Arrange
            var today = DateTime.Today;
            char format = 'd';
            var expected = today.GetDateTimeFormats(format);

            // Act
            var target = new DateTimeWrap(today);
            var actual = target.GetDateTimeFormats(format);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenGetDateTimeFormatsWithFormatProviderCalled_ExpectSameValueReturned()
        {
            // Arrange
            var today = DateTime.Today;
            var formatProvider = new DateTimeFormatInfo();
            var expected = today.GetDateTimeFormats(formatProvider);

            // Act
            var target = new DateTimeWrap(today);
            var actual = target.GetDateTimeFormats(formatProvider);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenGetDateTimeFormatsWithFormatProviderCharCalled_ExpectSameValueReturned()
        {
            // Arrange
            var today = DateTime.Today;
            var formatProvider = new DateTimeFormatInfo();
            char format = 'd';
            var expected = today.GetDateTimeFormats(format, formatProvider);

            // Act
            var target = new DateTimeWrap(today);
            var actual = target.GetDateTimeFormats(format, formatProvider);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenGetHashCodeCalled_ExpectSameValueReturned()
        {
            // Arrange
            var today = DateTime.Today;
            var expected = today.GetHashCode();

            // Act
            var target = new DateTimeWrap(today);
            var actual = target.GetHashCode();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenGetTypeCodeCalled_ExpectSameValueReturned()
        {
            // Arrange
            var today = DateTime.Today;
            var expected = today.GetTypeCode();

            // Act
            var target = new DateTimeWrap(today);
            var actual = target.GetTypeCode();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenIsDaylightSavingTime_ExpectFalseReturned()
        {
            // Arrange
            var date = new DateTime(2015, 7, 1);
            const bool expected = true;

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.IsDaylightSavingTime();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenIsNotDaylightSavingTime_ExpectFalseReturned()
        {
            // Arrange
            var date = new DateTime(2015, 1, 1);
            const bool expected = false;

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.IsDaylightSavingTime();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearIsLeapYear_ExpectTrue()
        {
            // Arrange
            const bool expected = true;
            const int input = 2012;

            // Act
            var target = new DateTimeWrap();
            var actual = target.IsLeapYear(input);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenYearIsNotLeapYear_ExpectFalse()
        {
            // Arrange
            const bool expected = false;
            const int input = 2011;

            // Act
            var target = new DateTimeWrap();
            var actual = target.IsLeapYear(input);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenParseCalled_ExpectEquivalentDateTimeValue()
        {
            // Arrange
            var dateTimeString = "10/15/2015";
            var expected = DateTime.Parse(dateTimeString);

            // Act
            var target = new DateTimeWrap();
            var actual = target.Parse(dateTimeString);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenParseWithFormatProviderCalled_ExpectEquivalentValue()
        {
            // Arrange
            var dateTimeString = "10/15/2015";
            var formatProvider = new DateTimeFormatInfo();
            var expected = DateTime.Parse(dateTimeString, formatProvider);

            // Act
            var target = new DateTimeWrap();
            var actual = target.Parse(dateTimeString, formatProvider);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenParseWithFormatProviderAndDateTimeStylesCalled_ExpectEquivalentValue()
        {
            // Arrange
            var dateTimeString = "10/15/2015";
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            var expected = DateTime.Parse(dateTimeString, formatProvider, dateTimeStyles);

            // Act
            var target = new DateTimeWrap();
            var actual = target.Parse(dateTimeString, formatProvider, dateTimeStyles);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenParseExactCalled_ExpectEquivalentDateTimeValue()
        {
            // Arrange
            var dateTimeString = "10/15/2015";
            var format = "MM/dd/yyyy";
            var formatProvider = new DateTimeFormatInfo();
            var expected = DateTime.ParseExact(dateTimeString, format, formatProvider);

            // Act
            var target = new DateTimeWrap();
            var actual = target.ParseExact(dateTimeString, format, formatProvider);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenParseExactWithFormatProviderCalled_ExpectEquivalentValue()
        {
            // Arrange
            var dateTimeString = "10/15/2015";
            var format = "MM/dd/yyyy";
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            var expected = DateTime.ParseExact(dateTimeString, format, formatProvider, dateTimeStyles);

            // Act
            var target = new DateTimeWrap();
            var actual = target.ParseExact(dateTimeString, format, formatProvider, dateTimeStyles);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenParseExactWithFormatProviderAndDateTimeStylesCalled_ExpectEquivalentValue()
        {
            // Arrange
            var dateTimeString = "10/15/2015";
            var format = new[] { "MM/dd/yyyy" };
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            var expected = DateTime.ParseExact(dateTimeString, format, formatProvider, dateTimeStyles);

            // Act
            var target = new DateTimeWrap();
            var actual = target.ParseExact(dateTimeString, format, formatProvider, dateTimeStyles);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenSpecifyKindCalled_ExpectSameValueReturned()
        {
            // Arrange
            const DateTimeKind expected = DateTimeKind.Utc;

            // Act
            var target = new DateTimeWrap();
            var actual = target.SpecifyKind(target, expected);

            // Assert
            actual.Kind.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenSubtractDateTimeCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            var subtractedDate = new DateTime(2000, 1, 1);
            var expected = now.Subtract(subtractedDate);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.Subtract(new DateTimeWrap(subtractedDate));

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenSubtractTimeSpanCalled_ExpectSameValueReturned()
        {
            // Arrange
            var now = DateTime.Now;
            var subtractedTimeSpan = TimeSpan.FromDays(15);
            var expected = now.Subtract(subtractedTimeSpan);

            // Act
            var target = new DateTimeWrap(now);
            var actual = target.Subtract(subtractedTimeSpan);

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToBinaryCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15);
            var expected = date.ToBinary();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToBinary();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToFileTimeCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15);
            var expected = date.ToFileTime();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToFileTime();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToFileTimeUtcCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15);
            var expected = date.ToFileTimeUtc();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToFileTimeUtc();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToLocalTimeCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12, DateTimeKind.Utc);
            var expected = date.ToLocalTime();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToLocalTime();

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToLongDateStringCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var expected = date.ToLongDateString();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToLongDateString();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToLongTimeStringCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var expected = date.ToLongTimeString();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToLongTimeString();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToOADateCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var expected = date.ToOADate();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToOADate();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToShortDateStringCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var expected = date.ToShortDateString();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToShortDateString();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToShortTimeStringCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var expected = date.ToShortTimeString();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToShortTimeString();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToStringCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var expected = date.ToString();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToString();

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToStringWithFormatProviderCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var formatProvider = new DateTimeFormatInfo();
            var expected = date.ToString(formatProvider);

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToString(formatProvider);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToStringWithFormatCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var format = "MM/dd/yyyy";
            var expected = date.ToString(format);

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToString(format);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToStringWithFormatAndFormatProviderCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var format = "MM/dd/yyyy";
            var formatProvider = new DateTimeFormatInfo();
            var expected = date.ToString(format, formatProvider);

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToString(format, formatProvider);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenToUniversalTimeCalled_ExpectExpectedValueReturned()
        {
            // Arrange
            var date = new DateTime(2015, 10, 15, 12, 12, 12);
            var expected = date.ToUniversalTime();

            // Act
            var target = new DateTimeWrap(date);
            var actual = target.ToUniversalTime();

            // Assert
            actual.DateTimeInstance.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenTryParseCalledWithValidDate_ExpectTrue()
        {
            // Arrange
            const string dateTimeString = "10/15/2015";
            const bool expected = true;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParse(dateTimeString, out output);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenTryParseCalledWithInvalidDate_ExpectFalse()
        {
            // Arrange
            const string dateTimeString = "10/55/2015";
            const bool expected = false;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParse(dateTimeString, out output);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenTryParseWithFormatFormatProviderAndDateTimeStylesCalledWithValidDate_ExpectTrue()
        {
            // Arrange
            const string dateTimeString = "10/15/2015";
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            const bool expected = true;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParse(dateTimeString, formatProvider, dateTimeStyles, out output);

            // Assert
            actual.Should().Equal(expected);
        }


        [TestMethod]
        public void WhenTryParseWithFormatProviderAndDateTimeStylesCalledWithInvalidDate_ExpectFalse()
        {
            // Arrange
            const string dateTimeString = "10/55/2015";
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            const bool expected = false;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParse(dateTimeString, formatProvider, dateTimeStyles, out output);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenTryParseExactWithValidDate_ExpectTrue()
        {
            // Arrange
            const string dateTimeString = "10/15/2015";
            var format = "MM/dd/yyyy";
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            const bool expected = true;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParseExact(dateTimeString, format, formatProvider, dateTimeStyles, out output);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenTryParseExactWithInvalidDate_ExpectFalse()
        {
            // Arrange
            const string dateTimeString = "10/55/2015";
            var format = "MM/dd/yyyy";
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            const bool expected = false;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParseExact(dateTimeString, format, formatProvider, dateTimeStyles, out output);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenTryParseExactFormatsWithValidDate_ExpectTrue()
        {
            // Arrange
            const string dateTimeString = "10/15/2015";
            var formats = new[] { "MM/dd/yyyy" };
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            const bool expected = true;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParseExact(dateTimeString, formats, formatProvider, dateTimeStyles, out output);

            // Assert
            actual.Should().Equal(expected);
        }

        [TestMethod]
        public void WhenTryParseExactFormatsWithInvalidDate_ExpectFalse()
        {
            // Arrange
            const string dateTimeString = "10/55/2015";
            var formats = new[] { "MM/dd/yyyy" };
            var formatProvider = new DateTimeFormatInfo();
            var dateTimeStyles = DateTimeStyles.AdjustToUniversal;
            const bool expected = false;

            // Act
            IDateTime output;
            var target = new DateTimeWrap();
            var actual = target.TryParseExact(dateTimeString, formats, formatProvider, dateTimeStyles, out output);

            // Assert
            actual.Should().Equal(expected);
        }
    }
}
