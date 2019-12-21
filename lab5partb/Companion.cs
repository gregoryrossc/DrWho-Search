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
    /// Companion class holds Doctors companion information
    /// </summary>
    class Companion
    {

        public string CompanionName { get; set; } //doctors companions name
        public string Actor { get; set; } //the name of the actor playing the companion
        public int DoctorID { get; set; } //the doctor ID of the companion 
        public string StoryID { get; set; } //the story ID of the companion

        /// <summary>
        /// Initializes a new instance of the companion class.
        /// </summary>
        /// <param name="companionName">Name of the companion.</param>
        /// <param name="actor">The actor.</param>
        /// <param name="doctorID">The doctor identifier.</param>
        /// <param name="storyID">The story identifier.</param>
        public Companion(string companionName, string actor, int doctorID, string storyID)
        {
            CompanionName = companionName;
            Actor = actor;
            DoctorID = doctorID;
            StoryID = storyID;

        }
    }
}
