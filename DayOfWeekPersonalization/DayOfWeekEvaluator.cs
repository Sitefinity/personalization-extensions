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
        /// Gets or sets the current date time.
        /// </summary>
        /// <value>
        /// The current date time.
        /// </value>
        public DateTime CurrentDateTime
        {
            get
            {
                if (this.dateTime != DateTime.MinValue)
                {
                    return this.dateTime;
                }
                else
                {
                    return DateTime.Now;
                }
            }

            set
            {
                this.dateTime = value;
            }
        }

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
                DateTime currentDay = this.CurrentDateTime;
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

        #region Private members

        private DateTime dateTime;

        #endregion
    }
}
