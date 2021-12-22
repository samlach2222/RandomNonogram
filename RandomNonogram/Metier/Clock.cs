using System;
using System.Text;

namespace Logic
{
    /// <summary>
    /// Class to manage the game timer
    /// </summary>
    public class Clock
    {
        private DateTime dateStarted;
        private DateTime datePaused;
        private bool paused;

        /// <summary>
        /// Constructor of the Clock class
        /// </summary>
        public Clock()
        {
            IsChronoLaunched = false;
            paused = false;
        }

        /// <summary>
        /// Method to start the game timer
        /// </summary>
        public void Start()
        {
            IsChronoLaunched = true;
            if (paused)
            {
                TimeSpan t = DateTime.Now.Subtract(datePaused);
                dateStarted = dateStarted.Add(t);
            }
            else
            {
                dateStarted = DateTime.Now;
            }
        }

        /// <summary>
        /// Method to stop the game timer
        /// </summary>
        public void Stop()
        {
            IsChronoLaunched = false;
            datePaused = DateTime.Now;
            paused = true;
        }

        /// <summary>
        /// Method to reset the game timer
        /// </summary>
        public void Reset()
        {
            IsChronoLaunched = false;
            paused = false;
            dateStarted = DateTime.Now;
            datePaused = DateTime.Now;

        }
        
        /// <summary>
        /// Getter and Setter of the boolean to know if the clokc is launched
        /// </summary>
        public bool IsChronoLaunched
        {
            get; private set;
        }

        /// <summary>
        /// Override of the ToString() method to return the timer in the form HH:MM:SS
        /// </summary>
        /// <returns>The string that is displayed in the game</returns>
        public override string ToString()
        {
            DateTime DateNow = DateTime.Now;
            TimeSpan Difference = DateNow - dateStarted;

            StringBuilder builder = new StringBuilder(11);
            _ = builder.Append(Difference.Hours.ToString("d2"));
            _ = builder.Append(":");
            _ = builder.Append(Difference.Minutes.ToString("d2"));
            _ = builder.Append(":");
            _ = builder.Append(Difference.Seconds.ToString("d2"));

            return builder.ToString();
        }
    }
}
