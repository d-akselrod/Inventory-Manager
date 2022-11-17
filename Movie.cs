//Author: Daniel Akselrod
//File Name: Movie.cs
//Project Name: AmazonInventoryManager
//Creation Date: March 8, 2020
//Modified Date: May 7, 2020
//Description: The purpose of this class is to store unique attributes and behaviours of a movie

namespace AmazonInventoryManager
{
    class Movie : EntertainmentItem
    {
        //Stores the attributes of a movie
        private string director;
        private int duration;

        //The default contrustor. Sets the movies attributes with default values
        public Movie() : base()
        {
            director = "Unknown Director";
            duration = 90;
        }

        //The overloaded contrustor. Sets the movies attributes with inputed properties
        public Movie(string title, double cost, string genre, string platform, int releaseYear, string director, int duration)
            : base(title, cost, genre, platform, releaseYear)
        {
            this.director = director;
            this.duration = duration;
        }

        //Pre: N/A
        //Post: The books director
        //Description: Returns the movies director
        public string GetDirector()
        {
            return director;
        }

        //Pre: N/A
        //Post: The books duration
        //Description: Returns the movies duration
        public int GetDuration()
        {
            return duration;
        }

        //Pre: N/A
        //Post: The movies type
        //Description: Returns the movies type
        public override string GetItemType()
        {
            return "Movie";
        }

        //Pre: N/A
        //Post: A string composed of all the items data
        //Description: Returns a string composed of all the items data with a prefix on the unique information
        public override string GetListData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear + ",Director: " + director + ",Durection: " + duration + " Min";
        }

        //Pre: N/A
        //Post: A string composed of all the items data
        //Description: Returns a string composed of all the items data
        public override string GetItemData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear + "," + director + "," + duration;
        }


        //Pre: The director
        //Post: N/A
        //Description: Sets the director of the movie
        public void SetDirector(string director)
        {
            this.director = director;
        }

        //Pre: The duration
        //Post: N/A
        //Description: Sets the duration of the movie
        public void SetDuration(int duration)
        {
            this.duration = duration;
        }
    }
}
