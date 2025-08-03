namespace MyTimeClass
{
    /// <summary>
    /// MyTime class models a time instance with hour, minute, and second components.
    /// Provides methods for time manipulation and validation.
    /// </summary>
    public class MyTime
    {
        private int hour;
        private int minute;
        private int second;

        /// <summary>
        /// Default constructor - initializes time to 00:00:00
        /// </summary>
        public MyTime()
        {
            hour = 0;
            minute = 0;
            second = 0;
        }

        /// <summary>
        /// Parameterized constructor - initializes time with given values
        /// </summary>
        /// <param name="hour">Hour value (0-23)</param>
        /// <param name="minute">Minute value (0-59)</param>
        /// <param name="second">Second value (0-59)</param>
        public MyTime(int hour, int minute, int second)
        {
            this.hour = 0;
            this.minute = 0;
            this.second = 0;

            SetTime(hour, minute, second);
        }

        /// <summary>
        /// Sets all time components at once with validation
        /// </summary>
        /// <param name="hour">Hour value (0-23)</param>
        /// <param name="minute">Minute value (0-59)</param>
        /// <param name="second">Second value (0-59)</param>
        public void SetTime(int hour, int minute, int second)
        {
            SetHour(hour);
            SetMinute(minute);
            SetSecond(second);
        }

        /// <summary>
        /// Mutator method for hour with validation
        /// </summary>
        /// <param name="hour">Hour value (0-23)</param>
        public void SetHour(int hour)
        {
            if (IsValidHour(hour))
            {
                this.hour = hour;
            }
            else
            {
                throw new ArgumentException("Invalid hour, minute, or second!");
            }
        }

        /// <summary>
        /// Mutator method for minute with validation
        /// </summary>
        /// <param name="minute">Minute value (0-59)</param>
        public void SetMinute(int minute)
        {
            if (IsValidMinute(minute))
            {
                this.minute = minute;
            }
            else
            {
                throw new ArgumentException("Invalid hour, minute, or second!");
            }
        }

        /// <summary>
        /// Mutator method for second with validation
        /// </summary>
        /// <param name="second">Second value (0-59)</param>
        public void SetSecond(int second)
        {
            if (IsValidSecond(second))
            {
                this.second = second;
            }
            else
            {
                throw new ArgumentException("Invalid hour, minute, or second!");
            }
        }

        /// <summary>
        /// Accessor method for hour
        /// </summary>
        /// <returns>Current hour value</returns>
        public int GetHour()
        {
            return hour;
        }

        /// <summary>
        /// Accessor method for minute
        /// </summary>
        /// <returns>Current minute value</returns>
        public int GetMinute()
        {
            return minute;
        }

        /// <summary>
        /// Accessor method for second
        /// </summary>
        /// <returns>Current second value</returns>
        public int GetSecond()
        {
            return second;
        }

        /// <summary>
        /// Returns string representation of time in HH:MM:SS format
        /// </summary>
        /// <returns>Formatted time string</returns>
        public override string ToString()
        {
            return $"{hour:D2}:{minute:D2}:{second:D2}";
        }

        /// <summary>
        /// Advances time by one second, handling rollovers
        /// </summary>
        /// <returns>This MyTime instance</returns>
        public MyTime NextSecond()
        {
            second++;
            if (second > 59)
            {
                second = 0;
                NextMinute();
            }
            return this;
        }

        /// <summary>
        /// Advances time by one minute, handling rollovers
        /// </summary>
        /// <returns>This MyTime instance</returns>
        public MyTime NextMinute()
        {
            minute++;
            if (minute > 59)
            {
                minute = 0;
                NextHour();
            }
            return this;
        }

        /// <summary>
        /// Advances time by one hour, handling 24-hour rollover
        /// </summary>
        /// <returns>This MyTime instance</returns>
        public MyTime NextHour()
        {
            hour++;
            if (hour > 23)
            {
                hour = 0;
            }
            return this;
        }

        /// <summary>
        /// Moves time back by one second, handling rollovers
        /// </summary>
        /// <returns>This MyTime instance</returns>
        public MyTime PreviousSecond()
        {
            second--;
            if (second < 0)
            {
                second = 59;
                PreviousMinute();
            }
            return this;
        }

        /// <summary>
        /// Moves time back by one minute, handling rollovers
        /// </summary>
        /// <returns>This MyTime instance</returns>
        public MyTime PreviousMinute()
        {
            minute--;
            if (minute < 0)
            {
                minute = 59;
                PreviousHour();
            }
            return this;
        }

        /// <summary>
        /// Moves time back by one hour, handling 24-hour rollover
        /// </summary>
        /// <returns>This MyTime instance</returns>
        public MyTime PreviousHour()
        {
            hour--;
            if (hour < 0)
            {
                hour = 23;
            }
            return this;
        }

        private static bool IsValidHour(int hour)
        {
            return hour >= 0 && hour <= 23;
        }

        private static bool IsValidMinute(int minute)
        {
            return minute >= 0 && minute <= 59;
        }

        private static bool IsValidSecond(int second)
        {
            return second >= 0 && second <= 59;
        }
    }
}
