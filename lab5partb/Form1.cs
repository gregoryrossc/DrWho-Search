/*I, Gregory Carroll, 000101968 certify that this material is my original work.
 * No other person's work has been used without due acknowledgement.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace lab5partb
{
    /// <summary>
    /// Main form class holds tables as fields and connection to the SQL database
    /// </summary>
    public partial class MainForm : Form
    {
        private List<Doctor> doctors = new List<Doctor>(); //doctors
        private List<Companion> companions = new List<Companion>(); //companions
        private List<Episode> episodes = new List<Episode>(); //episodes

        /// <summary>
        /// conection to SQL database
        /// </summary>
        public SqlConnection Connection { get; }
        string connectionString = @"Data Source=.\SQLEXPRESS2;Initial Catalog=COMP10204_Lab5;Integrated Security=True"; //connection to DB
        public MainForm()
        {

            InitializeComponent();
            try
            {
                Connection = new SqlConnection(); //create new instance of SQLconnection
                Connection.ConnectionString = connectionString;
                Connection.Open(); //open the connection
                statusLabel.Text = "Connected to Database Successfully"; //show message if connection is successful
            }
            catch (Exception ex) //catches any exceptions if connection fails
            {
                statusLabel.Text = "Database Connection failed - check Connection String : " +
                ex.Message;
            }

            ///while datareader is reading selects all doctors and populates the class with data from the doctor table then adds the doctor to the list
            SqlCommand addDoctors = new SqlCommand("Select * FROM DOCTOR", Connection);
            SqlDataReader reader2 = addDoctors.ExecuteReader();
            while (reader2.Read())
            {
                Doctor doctor = new Doctor((int)reader2["DoctorID"], (string)reader2["Actor"], (int)reader2["Series"], (int)reader2["Age"], (string)reader2["Debut"], (byte[])reader2["Picture"]);
                doctors.Add(doctor);
            }
            reader2.Close();//closes reader once all data from table has been read

            ///while datareader is reading selects all companions and populates the class with data from the companions table then adds the companion to the list
            SqlCommand addCompanions = new SqlCommand("Select * FROM COMPANION", Connection);
            SqlDataReader reader3 = addCompanions.ExecuteReader();
            while (reader3.Read())
            {
                Companion companion = new Companion((string)reader3["Name"], (string)reader3["Actor"], (int)reader3["DoctorID"], (string)reader3["StoryID"]);
                companions.Add(companion);
            }
            reader3.Close(); //closes reader once all data from table has been read

            ///while datareader is reading selects all episodes and populates the class with data from the episodes table then adds the episode to the list
            SqlCommand addEpisodes = new SqlCommand("Select * FROM EPISODE", Connection);
            SqlDataReader reader4 = addEpisodes.ExecuteReader();
            while (reader4.Read())
            {
                Episode episode = new Episode((string)reader4["StoryID"], (int)reader4["Season"], (int)reader4["Seasonyear"], (string)reader4["title"]);
                episodes.Add(episode);
            }
            reader4.Close(); //closes reader once all data from table has been read

            ///adds all doctor ID's to dropdown box
            foreach (Doctor x in doctors)
            {
                if(x != null)
                {
                     doctorComboBox.Items.Add(x.DoctorID);
                }
            }
        }

        private void doctorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (linqRadio.Checked == true){ //if linq radio button is checked search using linq

                resultsListBox.Items.Clear(); //clears boxes after each search

                ///selects doctor based on selected index of the doctorID from the dropdown / combo box
                IEnumerable<Doctor> resultsActor =
                    from testdoctor in doctors
                    where testdoctor.DoctorID.Equals(doctorComboBox.SelectedIndex)
                    select testdoctor;

                //populates the text boxes with corresponding information
                foreach (var Doctor in resultsActor)
                {
                    playedByTextBox.Text = Doctor.Actor; //sets the actor name
                    ageTextBox.Text = Doctor.Age.ToString(); //sets the doctors age
                    seriesLabelBox.Text = Doctor.Series.ToString(); //sets the series the doctor was in

                    //sets image to the doctors
                    byte[] photo = Doctor.Photo;
                    MemoryStream stream = new MemoryStream(photo);
                    Image image = Image.FromStream(stream);
                    myPictureBox.Image = Image.FromStream(stream);
                }

                //selects the companion based on the selected index of the DoctorID from the dropdown / combo box
                IEnumerable<Companion> resultsCompanions =
                    from testCompanion in companions
                    where testCompanion.DoctorID.Equals(doctorComboBox.SelectedIndex)
                    select testCompanion;

                //populates companions into the designated list box
                foreach (var companion in resultsCompanions)
                {
                    resultsListBox.Items.Add($"{companion.CompanionName}");  
                }

                //joins tables of companions and episodes to get the season year of the selected doctor
                var resultsEpisodes =
                    from companion in companions
                    join episode in episodes on companion.StoryID
                    equals episode.StoryID
                    where companion.DoctorID.Equals(doctorComboBox.SelectedIndex)
                    select episode.SeasonYear;

                yearTextBox.Text = resultsEpisodes.FirstOrDefault().ToString();

                //joins the companion, doctor and episode tables to get the first episode title the selected doctor was in
                var resultsFirstEpisode =
                    from episode in episodes
                    join companion in companions on episode.StoryID
                    equals companion.StoryID
                    join doctor in doctors on companion.DoctorID
                    equals doctor.DoctorID
                    where companion.DoctorID.Equals(doctorComboBox.SelectedIndex)
                    select episode.Title;

                episodeBox.Text = resultsFirstEpisode.FirstOrDefault().ToString();

            }
            else if (sqlRadio.Checked == true) //if SQL button checked Do SQL Query instead of linq
            {
                resultsListBox.Items.Clear(); //clears items from boxes upon new search

                //builds query string for a Doctor based on the selected doctorID
                string SqlSelectQuery = "Select * FROM DOCTOR WHERE DOCTORID =" + int.Parse(doctorComboBox.SelectedIndex.ToString());
                
                SqlCommand cmd = new SqlCommand(SqlSelectQuery, Connection);
                SqlDataReader reader = cmd.ExecuteReader();


                if (reader.Read())//if data reader reads successfully
                {
                    playedByTextBox.Text = (reader["ACTOR"].ToString()); //sets actor name
                    ageTextBox.Text = (reader["AGE"].ToString()); //sets the age of the actor
                    seriesLabelBox.Text = (reader["SERIES"].ToString()); //sets the series

                    byte[] photo = ((byte[])reader["PICTURE"]);
                    MemoryStream stream = new MemoryStream(photo);
                    Image image = Image.FromStream(stream);
                    myPictureBox.Image = Image.FromStream(stream); //sets the doctors picture
                }
                reader.Close(); 

                //the query string for a Companion based on the selected doctorID
                string SqlSelectQuery2 = "Select * FROM COMPANION WHERE DOCTORID =" + int.Parse(doctorComboBox.SelectedIndex.ToString());

                SqlCommand cmd2 = new SqlCommand(SqlSelectQuery2, Connection);
                SqlDataReader reader2 = cmd2.ExecuteReader();

                //adds the companion names to the list box until the data reader is done reading
                while (reader2.Read())
                {
                    resultsListBox.Items.Add(reader2["NAME"].ToString()); 
                }
                reader2.Close();

                //the query string for a Episode based on the selected doctorID
                string SqlSelectQuery3 = "Select EPISODE.SEASONYEAR FROM EPISODE LEFT JOIN COMPANION ON COMPANION.STORYID = EPISODE.STORYID WHERE COMPANION.DOCTORID =" + int.Parse(doctorComboBox.SelectedIndex.ToString());

                SqlCommand cmd3 = new SqlCommand(SqlSelectQuery3, Connection);
                SqlDataReader reader3 = cmd3.ExecuteReader();

                if (reader3.Read())
                {
                    yearTextBox.Text = (reader3["SEASONYEAR"].ToString()); //sets the year
                }
                reader3.Close();

                //the query for the Episode Title based on the selected doctorID
                string SqlSelectQuery4 = "Select EPISODE.TITLE FROM EPISODE JOIN COMPANION on COMPANION.STORYID = EPISODE.STORYID JOIN DOCTOR ON COMPANION.DOCTORID = DOCTOR.DOCTORID WHERE DOCTOR.DOCTORID =" + int.Parse(doctorComboBox.SelectedIndex.ToString());

                SqlCommand cmd4 = new SqlCommand(SqlSelectQuery4, Connection);
                SqlDataReader reader4 = cmd4.ExecuteReader();

                if (reader4.Read())
                {
                    episodeBox.Text = (reader4["TITLE"].ToString()); //sets the title
                }
                reader4.Close();
            }
        }

        /// <summary>
        /// Exits the Program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitCtrlQToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        
        /// <summary>
        /// Linq default selected radio button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            linqRadio.Checked = true;
        }
    }
}