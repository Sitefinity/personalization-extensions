using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DayOfWeekPersonalization;
using Telerik.Sitefinity.Personalization.Impl;

namespace DayOfWeekPersonalizationUnitTests
{
    [TestClass]
    public class DayOfWeekEvaluatorTests
    {
        [TestMethod]
        public void IsMatch_WhenDateTimeMondayAndSettingsMonday_ReturnsTrue()
        {
            DateTime mondayDate = new DateTime(2014, 6, 2);
            string mondayString = ((int)mondayDate.DayOfWeek).ToString();
            DayOfWeekEvaluator dayOfWeekEvaluator = new DayOfWeekEvaluator();

            dayOfWeekEvaluator.CurrentDateTime = mondayDate;
            bool isMatch = dayOfWeekEvaluator.IsMatch(mondayString, new PersonalizationTestContext());

            Assert.IsTrue(isMatch);
        }

        [TestMethod]
        public void IsMatch_WhenDateTimeMondayAndSettingsTuesday_ReturnsFalse()
        {
            DateTime mondayDate = new DateTime(2014, 6, 2);
            DateTime tuesdayDate = mondayDate.AddDays(1);
            string tuesdayString = ((int)tuesdayDate.DayOfWeek).ToString();
            DayOfWeekEvaluator dayOfWeekEvaluator = new DayOfWeekEvaluator();

            dayOfWeekEvaluator.CurrentDateTime = mondayDate;
            bool isMatch = dayOfWeekEvaluator.IsMatch(tuesdayString, new PersonalizationTestContext());

            Assert.IsFalse(isMatch);
        }

        [TestMethod]
        public void IsMatch_WhenSettingsAreNull_ReturnsFalse()
        {
            DayOfWeekEvaluator dayOfWeekEvaluator = new DayOfWeekEvaluator();

            bool isMatch = dayOfWeekEvaluator.IsMatch(null, new PersonalizationTestContext());

            Assert.IsFalse(isMatch);
        }

        [TestMethod]
        public void IsMatch_WhenSettingsAreEmpty_ReturnsFalse()
        {
            DayOfWeekEvaluator dayOfWeekEvaluator = new DayOfWeekEvaluator();

            bool isMatch = dayOfWeekEvaluator.IsMatch(String.Empty, new PersonalizationTestContext());

            Assert.IsFalse(isMatch);
        }

        [TestMethod]
        public void IsMatch_WhenSettingsAreIncorrectDayOfWeek_ReturnsFalse()
        {
            string incorectDayOfWeek = "test day of week";

            DayOfWeekEvaluator dayOfWeekEvaluator = new DayOfWeekEvaluator();

            bool isMatch = dayOfWeekEvaluator.IsMatch(incorectDayOfWeek, new PersonalizationTestContext());

            Assert.IsFalse(isMatch);
        }
    }
}
