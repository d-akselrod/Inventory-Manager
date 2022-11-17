//Author: Daniel Akselrod
//File Name: VideoGame.cs
//Project Name: AmazonInventoryManager
//Creation Date: March 8, 2020
//Modified Date: May 7, 2020
//Description: The purpose of this class is to store unique attributes and behaviours of a video game

namespace AmazonInventoryManager
{
    class VideoGame : EntertainmentItem
    {
        //Stores the attributes of a video game
        private string developer;
        private double rating;

        //The default contrustor. Sets the video games attributes with default values
        public VideoGame() : base()
        {
            developer = "Unknown Developer";
            rating = 0;
        }

        //The overloaded contrustor. Sets the video games attributes with inputed properties
        public VideoGame (string title, double cost, string genre, string platform, int releaseYear, string developer, double rating)
            : base(title, cost, genre, platform, releaseYear)
        {
            this.developer = developer;
            this.rating = rating;
        }

        //Pre: N/A
        //Post: The video games developer
        //Description: Returns the video games developer
        public string GetDeveloper()
        {
            return developer;
        }

        //Pre: N/A
        //Post: The video games IGN.com Rating
        //Description: Returns the video games IGN.com rating
        public double GetRating()
        {
            return rating;
        }

        //Pre: N/A
        //Post: The video games type
        //Description: Returns the video games type
        public override string GetItemType()
        {
            return "Game";
        }

        //Pre: N/A
        //Post: A string composed of all the items data
        //Description: Returns a string composed of all the items data with a prefix on the unique information
        public override string GetListData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear + ",Developer: " + developer + ",IGN Rating: " + rating;
        }

        //Pre: N/A
        //Post: A string composed of all the items data
        //Description: Returns a string composed of all the items data
        public override string GetItemData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear + "," + developer + "," + rating; ;
        }

        //Pre: The developer
        //Post: N/A
        //Description: Sets the video games developer
        public void SetDeveloper(string developer)
        {
            this.developer = developer;
        }

        //Pre: The IGN.com Rating
        //Post: N/A
        //Description: Sets the IGN.com rating if the video game
        public void SetRating(double rating)
        {
            this.rating = rating;
        }
    }
}
