//Author: Daniel Akselrod
//File Name: Book.cs
//Project Name: AmazonInventoryManager
//Creation Date: March 8, 2020
//Modified Date: May 7, 2020
//Description: The purpose of this class is to store unique attributes and behaviours of a book

namespace AmazonInventoryManager
{
    class Book : EntertainmentItem
    {
        //Stores the attributes of a book
        private string author;
        private string publisher;

        //The default contrustor. Sets the books attributes with default values
        public Book() : base()
        {
            this.author = "Unknown Author";
            this.publisher = "Publisher";
        }

        //The overloaded contrustor. Sets the books attributes with inputed properties
        public Book(string title, double cost, string genre, string platform, int releaseYear, string author, string publisher)
            : base(title, cost, genre, platform, releaseYear)
        {
            this.author = author;
            this.publisher = publisher;
        }
        
        //Pre: N/A
        //Post: The books author
        //Description: Returns the books author
        public string GetAuthor()
        {
            return author;
        }

        //Pre: N/A
        //Post: The books publisher
        //Description: Returns the books publisher
        public string GetPublisher()
        {
            return publisher;
        }

        //Pre: N/A
        //Post: The books type
        //Description: Returns the books type
        public override string GetItemType()
        {
            return "Book";
        }

        //Pre: N/A
        //Post: A string composed of all the items data
        //Description: Returns a string composed of all the items data with a prefix on the unique information
        public override string GetListData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear + ",Author: " + author + ",Publisher: " + publisher;
        }

        //Pre: N/A
        //Post: A string composed of all the items data
        //Description: Returns a string composed of all the items data
        public override string GetItemData()
        {
            return title + "," + cost + "," + genre + "," + platform + "," + releaseYear + "," + author + "," + publisher;
        }

        //Pre: The author
        //Post: N/A
        //Description: Sets the author of a book
        public void SetAuthor(string author)
        {
            this.author = author;
        }

        //Pre: The author
        //Post: N/A
        //Description: Sets the publisher of a book
        public void SetPublisher(string publisher)
        {
            this.publisher = publisher;
        }
    }
}
