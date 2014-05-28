using System;
using Telerik.Sitefinity.Personalization;

namespace DayOfWeekPersonalization
{
    /// <summary>
    /// Evaluator for the day of the week criterion.
    /// </summary>
    public class DayOfWeekEvaluator : ICriterionEvaluator
    {
        #region Properties

        /// <summary>
        /// Gets the integer representation of the current day of the week.
        /// </summary>
        /// <value>
        /// The current day of week as an integer value.
        /// </value>
        protected virtual int CurrentDayOfWeek
        {
            get
            {
                DateTime currentDay = DateTime.Now;
                int currentDayOfWeek = (int)currentDay.DayOfWeek;
                return currentDayOfWeek;
            }
        }

        #endregion

        #region ICriterionEvaluator members

        /// <summary>
        /// Determines if the current user is a match for the given criterion.
        /// </summary>
        /// <param name="settings">The serialized settings with which the criterion has been configured.</param>
        /// <param name="testContext">Personalization test context populated by the personalization test console.</param>
        /// <returns>
        /// True if the criterion is matched; otherwise false.
        /// </returns>
        public bool IsMatch(string settings, IPersonalizationTestContext testContext)
        {
            if (settings == this.CurrentDayOfWeek.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
