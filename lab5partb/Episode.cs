/*I, Gregory Carroll, 000101968 certify that this material is my original work.
 * No other person's work has been used without due acknowledgement.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5partb
{
    /// <summary>
    /// Episode class holds all the information on the episode
    /// </summary>
    class Episode
    {
        public string StoryID { get; set; } //Episodes story ID
        public int Season { get; set; } //Season of the episode
        public int SeasonYear { get; set; } //Year in which the season episode was released
        public string Title { get; set; } //Title of the episode

        /// <summary>
        /// Initializes a new instance of the episode class.
        /// </summary>
        /// <param name="storyID">The story identifier.</param>
        /// <param name="season">The season.</param>
        /// <param name="seasonYear">The season year.</param>
        /// <param name="title">The title.</param>
        public Episode(string storyID, int season, int seasonYear, string title)
        {
            StoryID = storyID;
            Season = season;
            SeasonYear = seasonYear;
            Title = title;
        }
    }
}
