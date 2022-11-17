//Author: Daniel Akselrod
//File Name: EntertainmentItem.cs
//Project Name: AmazonInventoryManager
//Creation Date: March 8, 2020
//Modified Date: May 7, 2020
//Description: The purpose of this class is to store unique attributes and behaviours of a generic entertainment item

namespace AmazonInventoryManager
{
    class EntertainmentItem
    {
        //Stores the attributes of an entertainment item
        protected string title;
        protected string genre;
        protected string platform;
        protected int releaseYear;
        protected double cost;

        //The default constructor of an entertainment item with basic details
        public EntertainmentItem()
        {
            title = "Unknown Title";
            genre = "Unknown Genre";
            platform = "Unknown platform";
            releaseYear = 0;
            cost = 0;
        }
        //The overloaded constructor to create a new entertainmnent item
        public EntertainmentItem(string title, double cost,string genre, string platform, int releaseYear)
        {
            this.title = title;
            this.genre = genre;
            this.platform = platform;
            this.releaseYear = releaseYear;
            this.cost = cost;
        }

        //Pre: N/A
        //Post: Returns the title of the entertainment item
        //Description: Returns the title of the entertainment item
        public string GetTitle()
        {
            return title;
        }

        //Pre: N/A
        //Post: Returns the genre of the entertainment item
        //Description: Returns the genre of the entertainment item
        public string GetGenre()
        {
            return genre;
        }

        //Pre: N/A
        //Post: Returns the platform of the entertainment item
        //Description: Returns the platform of the entertainment item
        public string GetPlatform()
        {
            return platform;
        }

        //Pre: N/A
        //Post: Returns the release year of the entertainment item
        //Description: Returns the release year of the entertainment item
        public int GetReleaseYear()
        {
            return releaseYear;
        }

        //Pre: N/A
        //Post: Returns the cost of the entertainment item
        //Description: Returns the cost of the entertainment item
        public double GetCost()
        {
            return cost;
        }

        //Pre: N/A
        //Post: Returns the type of the entertainment item
        //Description: Returns type cost of the entertainment item
        public virtual string GetItemType()
        {
            return "Entertainment Item";
        }

        //Pre: The item's title
        //Post: N/A
        //Description: Sets the item title
        public void SetTitle(string title)
        {
            this.title = title;
        }

        //Pre: The item's genre
        //Post: N/A
        //Description: Sets the item genre
        public void SetGenre(string genre)
        {
            this.genre = genre;
        }

        //Pre: The item's paltform
        //Post: N/A
        //Description: Sets the item platform
        public void SetPlatform(string platform)
        {
            this.platform = platform;
        }

        //Pre: The item's release year
        //Post: N/A
        //Description: Sets the item release year
        public void SetReleaseYear(int releaseYear)
        {
            this.releaseYear = releaseYear;
        }

        //Pre: The item's cost
        //Post: N/A
        //Description: Sets the item cost
        public void SetCost(double cost)
        {
            this.cost = cost;
        }

        //Pre: N/A
        //Post: The item's information
        //Description: Returns the item information as a string
        public virtual string GetListData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear;
        }

        //Pre: N/A
        //Post: The item's information
        //Description: Returns the item information as a string
        public virtual string GetItemData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear;
        }
    }
}
