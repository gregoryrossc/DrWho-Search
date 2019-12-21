using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*I, Gregory Carroll, 000101968 certify that this material is my original work.
 * No other person's work has been used without due acknowledgement.
*/

namespace lab5partb
{
    /// <summary>
    /// Doctor class holds all of the doctors information
    /// </summary>
    class Doctor
    {
        public int DoctorID { get; set; } //the doctor ID
        public string Actor { get; set; } //the actors name playing the doctor
        public int Series { get; set; } //the series 
        public int Age { get; set; } //the age of the actor playing the doctor
        public string Debut { get; set; } //the doctors debut 

        public byte[] Photo { get; set; } //photo of the doctor

        /// <summary>
        /// Initializes a new instance of the Doctor class.
        /// </summary>
        /// <param name="doctorID">The doctor identifier.</param>
        /// <param name="actor">The actor.</param>
        /// <param name="series">The series.</param>
        /// <param name="age">The age.</param>
        /// <param name="debut">The debut.</param>
        /// <param name="photo">The photo.</param>
        public Doctor(int doctorID, string actor, int series, int age, string debut, byte[] photo)
        {
            DoctorID = doctorID;
            Actor = actor;
            Series = series;
            Age = age;
            Debut = debut;
            Photo = photo;


        }
    }
}
