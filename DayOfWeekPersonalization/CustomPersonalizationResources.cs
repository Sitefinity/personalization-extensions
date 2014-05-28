using Telerik.Sitefinity.Localization;

namespace DayOfWeekPersonalization
{
    [ObjectInfo(typeof(CustomPersonalizationResources), Title = "CustomPersonalizationResourcesTitle", Description = "CustomPersonalizationResourcesDescription")]
    public class CustomPersonalizationResources : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPersonalizationResources"/> class.
        /// </summary>
        public CustomPersonalizationResources()
        {
        }

        /// <summary>
        /// Day of week message.
        /// </summary>
        [ResourceEntry("Day",
            Value = "Day",
            Description = "The day of the week that will apply for this criterion.",
            LastModified = "2014/05/23")]
        public string Day
        {
            get { return this["Day"]; }
        }

        /// <summary>
        /// Error message when user enters invalid day.
        /// </summary>
        [ResourceEntry("DayError",
            Value = "Invalid Day",
            Description = "Error message when user enters invalid day.",
            LastModified = "2014/05/23")]
        public string DayError
        {
            get { return this["DayError"]; }
        }

        /// <summary>
        /// Monday resource entry.
        /// </summary>
        [ResourceEntry("Monday",
            Value = "Monday",
            Description = "The day of the week after Sunday but before Tuesday.",
            LastModified = "2014/05/27")]
        public string Monday
        {
            get { return this["Monday"]; }
        }

        /// <summary>
        /// Tuesday resource entry.
        /// </summary>
        [ResourceEntry("Tuesday",
            Value = "Tuesday",
            Description = "The day of the week after Monday but before Wednesday.",
            LastModified = "2014/05/27")]
        public string Tuesday
        {
            get { return this["Tuesday"]; }
        }

        /// <summary>
        /// Wednesday resource entry.
        /// </summary>
        [ResourceEntry("Wednesday",
            Value = "Wednesday",
            Description = "The day of the week after Tuesday but before Thursday.",
            LastModified = "2014/05/27")]
        public string Wednesday
        {
            get { return this["Wednesday"]; }
        }

        /// <summary>
        /// Thursday resource entry.
        /// </summary>
        [ResourceEntry("Thursday",
            Value = "Thursday",
            Description = "The day of the week after Wednesday but before Friday.",
            LastModified = "2014/05/27")]
        public string Thursday
        {
            get { return this["Thursday"]; }
        }

        /// <summary>
        /// Friday resource entry.
        /// </summary>
        [ResourceEntry("Friday",
            Value = "Friday",
            Description = "The day of the week after Thursday but before Saturday.",
            LastModified = "2014/05/27")]
        public string Friday
        {
            get { return this["Friday"]; }
        }

        /// <summary>
        /// Saturday resource entry.
        /// </summary>
        [ResourceEntry("Saturday",
            Value = "Saturday",
            Description = "The day of the week after Friday but before Sunday.",
            LastModified = "2014/05/27")]
        public string Saturday
        {
            get { return this["Saturday"]; }
        }

        /// <summary>
        /// Sunday resource entry.
        /// </summary>
        [ResourceEntry("Sunday",
            Value = "Sunday",
            Description = "The day of the week after Saturday but before Monday.",
            LastModified = "2014/05/27")]
        public string Sunday
        {
            get { return this["Sunday"]; }
        }
    }
}
