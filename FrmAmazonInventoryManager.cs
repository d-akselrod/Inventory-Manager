//Author: Daniel Akselrod
//File Name: FrmAmazonInventoryManager.cs
//Project Name: AmazonInventoryManager
//Creation Date: March 8, 2020
//Modified Date: May 7, 2020
//Description: The purpose of this application to act as an inventory manager for amazon that stores entertainment items including books, movies and video games.
//Users of the application will have the ability to add, remove, and modify items in the inventory which are all saved in a file named "inventory.txt." 

using System;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AmazonInventoryManager
{
    public partial class FrmAmazonInventoryManager : Form
    {
        //Stores the file input/output variables
        private StreamWriter outFile;
        private StreamReader inFile;
        private string filePath = "inventory.txt";

        //Stores the entertainment inventory
        private EntertainmentInventory entertainmentInventory = new EntertainmentInventory();

        //Stores the type of item the user is currently handling
        private Type addItemType;
        private Type displayItemType;
        private Type searchItemType;

        //Stores the item the user currently has selected
        private EntertainmentItem selectedItem;

        //Stores the amount of items displayed per list view
        private int numItemsPerView;

        //Stores the type of sorting methods a user may use
        private const int SORT_TYPE = 0;
        private const int SORT_COST = 1;
        private const int SORT_AZ = 2;
        private const int SORT_RELEASE_YEAR = 3;

        //Stores which property the user would want to modify
        private int modifyProperty;
        private const int MODIFY_TITLE = 0;
        private const int MODIFY_COST = 1;
        private const int MODIFY_GENRE = 2;
        private const int MODIFY_PLATFORM = 3;
        private const int MODIFY_RELEASE_YEAR = 4;
        private const int MODIFY_INFO_ONE = 5;
        private const int MODIFY_INFO_TWO = 6;

        //Stores which list views to update
        private const int UPDATE_ALL_ITEM_LISTVIEW = 0;
        private const int UPDATE_SPECIFIC_ITEM_LISTVIEW = 1;
        private const int UPDATE_ADD_ITEM_LISTVIEW = 2;
        private const int UPDATE_SEARCH_ITEM_LISTVIEW = 3;
        private const int UPDATE_ALL_LIST_VIEWS = 4;

        public FrmAmazonInventoryManager()
        {
            InitializeComponent();          
        }

        //Initialize Application Components
        private void FrmAmazonInventoryManager_Activated(object sender, EventArgs e)
        {
            //Initiallizes the application graphics
            InitializeGraphics();

            //Reads the inventory file and creates a list of entertainment items
            ReadFile();

            //Pre: N/A
            //Post: N/A
            //Description: Initialzes sertain graphics and controls for the user upon start of the application
            void InitializeGraphics()
            {
                //Initializes the main tab control
                tbAppControl.SelectedTab = tbMainMenu;
                tbAppControl.Update();

                tbAppControl.Appearance = TabAppearance.FlatButtons;
                tbAppControl.ItemSize = new Size(0, 1);
                tbAppControl.SizeMode = TabSizeMode.Fixed;
                foreach (TabPage tab in tbAppControl.TabPages)
                {
                    tab.ResetText();
                }

                //Initializes the sub
                tbSearchItem.Appearance = TabAppearance.FlatButtons;
                tbSearchItem.ItemSize = new Size(0, 1);
                tbSearchItem.SizeMode = TabSizeMode.Fixed;
                foreach (TabPage tab in tbSearchItem.TabPages)
                {
                    tab.ResetText();

                }

                //Sets certain controls to be transparent
                lblSearchFeedback.ResetText();
                lblModifiedInfo.Visible = false;
                txtModifiedInfo.Visible = false;
                btnConfirmModification.Visible = false;

                //Initializes the settings box with its values for the user to choose
                cmbMenuSettings.Items.Add(10);
                cmbMenuSettings.Items.Add(100);
                cmbMenuSettings.Items.Add(1000);
                cmbMenuSettings.Items.Add(10000);
                cmbMenuSettings.Items.Add(100000);

                //Sets the default value of the items per listview to 1000
                cmbMenuSettings.SelectedIndex = 2;
            }
        }

        //Main Menu
        private void tsmMainMenu_Click_1(object sender, EventArgs e)
        {
            tbAppControl.SelectedTab = tbMainMenu;
            tbAppControl.Update();
        }

        //Save Inventory
        private void tsmSaveInventory_Click(object sender, EventArgs e)
        {
            //Rewrites the inventory file
            WriteFile();
        }
        private void tsmSubSaveInventory_Click(object sender, EventArgs e)
        {
            //Activates the click on the save inventory file
            tsmSaveInventory.PerformClick();
        }

        //Add Item
        private void tsmAddBook_Click(object sender, EventArgs e)
        {
            //Sets the tab control to the add item tab
            tbAppControl.SelectedTab = tbAddItem;

            //Sets the add item text to add book and shifts the label to be horizontally centered
            lblAddItemType.Text = "Add Book";
            lblAddItemType.Location = new Point(91, 24);

            //Sets the feedback text to enter the items parameters
            lblAddFeedback.Text = "Please enter the Book's details above.";

            //Sets the two specific item properties to their respective names
            lblAddItemInfoOne.Text = "Author:";
            lblAddtemInfoTwo.Text = "Publisher:";

            //Changes the add item button text to the coresponding item type
            btnAddItem.Text = "Add Book";

            //Sets the type of item being added to a book type
            addItemType = typeof(Book);

            //Sets the column text on the add list view to their respective property names
            lstAddItemListView.Columns[6].Text = "Author";
            lstAddItemListView.Columns[7].Text = "Publisher";

            //Rests the text on the add item property texxt boxes
            txtAddItemTitle.ResetText();
            txtAddItemCost.ResetText();
            txtAddItemGenre.ResetText();
            txtAddItemPlatform.ResetText();
            txtAddItemReleaseYear.ResetText();
            txtAddItemInfoOne.ResetText();
            txtAddItemInfoTwo.ResetText();

            //Updates the tab control and the list view
            tbAppControl.Update();
            UpdateListViews(UPDATE_ADD_ITEM_LISTVIEW);
        }
        private void tsmAddMovie_Click(object sender, EventArgs e)
        {
            //Sets the tab control to the add item tab
            tbAppControl.SelectedTab = tbAddItem;

            //Sets the add item text to add movie and shifts the label to be horizontally centered
            lblAddItemType.Text = "Add Movie";
            lblAddItemType.Location = new Point(80, 24);

            //Sets the feedback text to enter the items parameters
            lblAddFeedback.Text = "Please enter the Movie's details above.";

            //Sets the two specific item properties to their respective names
            lblAddItemInfoOne.Text = "Director:";
            lblAddtemInfoTwo.Text = "Duration:";

            //Changes the add item button text to the coresponding item type
            btnAddItem.Text = "Add Movie";

            //Rests the text on the add item property texxt boxes
            txtAddItemTitle.ResetText();
            txtAddItemCost.ResetText();
            txtAddItemGenre.ResetText();
            txtAddItemPlatform.ResetText();
            txtAddItemReleaseYear.ResetText();
            txtAddItemInfoOne.ResetText();
            txtAddItemInfoTwo.ResetText();

            //Sets the type of item being added to a movie type
            addItemType = typeof(Movie);

            //Sets the column text on the add list view to their respective property names
            lstAddItemListView.Columns[6].Text = "Director";
            lstAddItemListView.Columns[7].Text = "Duration";

            //Updates the tab control and the list view
            tbAppControl.Update();
            UpdateListViews(UPDATE_ADD_ITEM_LISTVIEW);
        }
        private void tsmAddVideoGame_Click(object sender, EventArgs e)
        {
            //Sets the tab control to the add item tab
            tbAppControl.SelectedTab = tbAddItem;

            //Sets the add item text to Add Video Game and shifts the label to be horizontally centered
            lblAddItemType.Text = "Add Video Game";
            lblAddItemType.Location = new Point(50, 24);

            //Sets the feedback text to enter the items parameters
            lblAddFeedback.Text = "Please enter the Video Game's details above.";

            //Sets the two specific item properties to their respective names
            lblAddItemInfoOne.Text = "Developer:";
            lblAddtemInfoTwo.Text = "Rating:";

            //Changes the add item button text to the coresponding item type
            btnAddItem.Text = "Add Game";

            //Sets the type of item being added to a video game type
            addItemType = typeof(VideoGame);

            //Sets the column text on the add list view to their respective property names
            lstAddItemListView.Columns[6].Text = "Developer";
            lstAddItemListView.Columns[7].Text = "IGN Rating";

            //Rests the text on the add item property texxt boxes
            txtAddItemTitle.ResetText();
            txtAddItemCost.ResetText();
            txtAddItemGenre.ResetText();
            txtAddItemPlatform.ResetText();
            txtAddItemReleaseYear.ResetText();
            txtAddItemInfoOne.ResetText();
            txtAddItemInfoTwo.ResetText();

            //Updates the tab control and the list view
            tbAppControl.Update();
            UpdateListViews(UPDATE_ADD_ITEM_LISTVIEW);
        }
        private void tsmSubAddBook_Click(object sender, EventArgs e)
        {
            //Preforms a click to add a book
            tsmAddBook.PerformClick();
        }
        private void tsmSubAddVideoGame_Click(object sender, EventArgs e)
        {
            //Preforms a click to add a vdieo game
            tsmAddMovie.PerformClick();
        }
        private void tsmSubAddMovie_Click(object sender, EventArgs e)
        {
            //Preforms a click to add a movie
            tsmAddVideoGame.PerformClick();
        }
        private void btnLinkAddItem_Click(object sender, EventArgs e)
        {
            if (displayItemType == typeof(Book))
            {
                tsmAddBook.PerformClick();
            }
            else if (displayItemType == typeof(Movie))
            {
                tsmAddMovie.PerformClick();
            }
            else if (displayItemType == typeof(VideoGame))
            {
                tsmAddVideoGame.PerformClick();
            }
        }
        private void btnMenuAddBook_Click(object sender, EventArgs e)
        {
            //Preforms a click to add a book
            tsmAddBook.PerformClick();
        }
        private void btnMenuAddMovie_Click(object sender, EventArgs e)
        {
            //Preforms a click to add a movie
            tsmAddMovie.PerformClick();
        }
        private void btnMenuAddVideoGame_Click(object sender, EventArgs e)
        {
            //Preforms a click to add a video game
            tsmAddVideoGame.PerformClick();
        }
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            //Checks if the user has entered text for all item properties
            if (ScanAddInputs())
            {
                //Adds an item to the entertainment inventory for the quanitity of items to add
                for (int quantity = 0; quantity < numItemQuantity.Value; quantity++)
                {
                    entertainmentInventory.AddItem(CreateItem(addItemType, txtAddItemTitle.Text.ToLower(), Math.Round(Convert.ToDouble(txtAddItemCost.Text), 2).ToString(), txtAddItemGenre.Text.ToLower(),
                    txtAddItemPlatform.Text.ToLower(), txtAddItemReleaseYear.Text.ToLower(),
                    txtAddItemInfoOne.Text.ToLower(), txtAddItemInfoTwo.Text.ToLower()));
                }

                //Resets the control to set the quantity
                numItemQuantity.Value = 1;

                //Resets the add item text boxes
                txtAddItemTitle.ResetText();
                txtAddItemCost.ResetText();
                txtAddItemGenre.ResetText();
                txtAddItemPlatform.ResetText();
                txtAddItemReleaseYear.ResetText();
                txtAddItemInfoOne.ResetText();
                txtAddItemInfoTwo.ResetText();

                //Updates the add item list view
                UpdateListViews(UPDATE_ADD_ITEM_LISTVIEW);
            }
            else
            {
                //Tell the user to fill in all the item's properties if they have not
                lblAddFeedback.Text = "Please fill in all item properties";
            }
        }

        //Display Item
        private void tsmDisplayInventory_Click(object sender, EventArgs e)
        {
            //Sets the app control to the display inventory tab page and updates the tab control
            tbAppControl.SelectedTab = tbListView;
            tbAppControl.Update();

            //Updates the list view that holds all items
            UpdateListViews(UPDATE_ALL_ITEM_LISTVIEW);
        }
        private void tsmDisplayBooks_Click(object sender, EventArgs e)
        {
            //Sets the tab control tab to the tab that displays specific item types
            tbAppControl.SelectedTab = tbViewSpecificItems;

            //Sets the columns on the display list views to their respective property names
            lstItemSpecificListView.Columns[6].Text = "Author";
            lstItemSpecificListView.Columns[7].Text = "Publisher";
            lstTopTwoSpecificItem.Columns[6].Text = "Author";
            lstTopTwoSpecificItem.Columns[7].Text = "Publisher";

            //Sets the title page labels to tell the user what item type they are displaying
            lblDisplaySpecificFirstTwo.Text = "First Two Books";
            lblDisplaySpecificAll.Text = "All Books";

            //Changes the text on the buttons that allows them to add or locate an item type
            btnLinkAddItem.Text = "Add Book";
            btnLinkLocateItem.Text = "Locate Book";

            //Update the tab control
            tbAppControl.Update();

            //Sets the display item type to the type the user has selected and updates the list view
            displayItemType = typeof(Book);
            UpdateListViews(UPDATE_SPECIFIC_ITEM_LISTVIEW);
        }
        private void tsmDisplayMovies_Click(object sender, EventArgs e)
        {
            //Sets the tab control tab to the tab that displays specific item types
            tbAppControl.SelectedTab = tbViewSpecificItems;

            //Sets the columns on the display list views to their respective property names
            lstItemSpecificListView.Columns[6].Text = "Director";
            lstItemSpecificListView.Columns[7].Text = "Duration";
            lstTopTwoSpecificItem.Columns[6].Text = "Director";
            lstTopTwoSpecificItem.Columns[7].Text = "Duration";

            //Sets the title page labels to tell the user what item type they are displaying
            lblDisplaySpecificFirstTwo.Text = "First Two Movies";
            lblDisplaySpecificAll.Text = "All Movies";

            //Changes the text on the buttons that allows them to add or locate an item type
            btnLinkAddItem.Text = "Add Movie";
            btnLinkLocateItem.Text = "Locate Movie";

            //Update the tab control
            tbAppControl.Update();

            //Sets the display item type to the type the user has selected and updates the list view
            displayItemType = typeof(Movie);
            UpdateListViews(UPDATE_SPECIFIC_ITEM_LISTVIEW);
        }
        private void tsmDisplayVideoGames_Click(object sender, EventArgs e)
        {
            //Sets the tab control tab to the tab that displays specific item types
            tbAppControl.SelectedTab = tbViewSpecificItems;

            //Sets the columns on the display list views to their respective property names
            lstItemSpecificListView.Columns[6].Text = "Developer";
            lstItemSpecificListView.Columns[7].Text = "IGN Rating";
            lstTopTwoSpecificItem.Columns[6].Text = "Developer";
            lstTopTwoSpecificItem.Columns[7].Text = "IGN Rating";

            //Sets the title page labels to tell the user what item type they are displaying
            lblDisplaySpecificFirstTwo.Text = "First Two Video Games";
            lblDisplaySpecificAll.Text = "All Video Games";

            //Changes the text on the buttons that allows them to add or locate an item type
            btnLinkAddItem.Text = "Add Video Game";
            btnLinkLocateItem.Text = "Locate Video Game";

            //Update the tab control
            tbAppControl.Update();

            //Sets the display item type to the type the user has selected and updates the list view
            displayItemType = typeof(VideoGame);
            UpdateListViews(UPDATE_SPECIFIC_ITEM_LISTVIEW);

        }
        private void tsmSubDisplayFullInventory_Click(object sender, EventArgs e)
        {
            //Preforms a click to display the full inventory
            tsmDisplayInventory.PerformClick();
        }
        private void tsmSubDisplayBooks_Click(object sender, EventArgs e)
        {
            //Preforms a click to display all books
            tsmDisplayBooks.PerformClick();
        }
        private void tsmSubDisplayMovies_Click(object sender, EventArgs e)
        {
            //Preforms a click to display all movies
            tsmDisplayMovies.PerformClick();
        }
        private void tsmSubDisplayVideoGames_Click(object sender, EventArgs e)
        {
            //Preforms a click to display all video games
            tsmDisplayVideoGames.PerformClick();
        }
        private void btnMenuDisplayAllItems_Click(object sender, EventArgs e)
        {
            //Preforms a click to display the full inventory
            tsmDisplayInventory.PerformClick();
        }
        private void btnMenuDisplayBooks_Click(object sender, EventArgs e)
        {
            //Preforms a click to display all books
            tsmDisplayBooks.PerformClick();
        }
        private void btnMenuDisplayMovies_Click(object sender, EventArgs e)
        {
            //Preforms a click to display all movies
            tsmDisplayMovies.PerformClick();
        }
        private void btnMenuDisplayVideoGames_Click(object sender, EventArgs e)
        {
            //Preforms a click to display all video games
            tsmDisplayVideoGames.PerformClick();
        }

        //Locate Item
        private void tsmLocateBook_Click(object sender, EventArgs e)
        {
            //Sets the tab control to the item locate tab and updates the tab control
            tbAppControl.SelectedTab = tbItemSearch;
            tbAppControl.Update();

            //Sets certain parameters specific to the item type and updates the list view to the locate item type
            tbSearchItem.SelectedTab = tbItemInfo;
            lstSearchItemsListView.Columns[6].Text = "Author";
            lstSearchItemsListView.Columns[7].Text = "Publisher";
            searchItemType = typeof(Book);
            UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);
        }
        private void tsmLocateMovie_Click(object sender, EventArgs e)
        {
            //Sets the tab control to the item locate tab and updates the tab control
            tbAppControl.SelectedTab = tbItemSearch;
            tbAppControl.Update();

            //Sets certain parameters specific to the item type and updates the list view to the locate item type
            tbSearchItem.SelectedTab = tbItemInfo;
            lstSearchItemsListView.Columns[6].Text = "Director";
            lstSearchItemsListView.Columns[7].Text = "Duration";
            searchItemType = typeof(Movie);
            UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);

        }
        private void tsmLocateVideoGame_Click(object sender, EventArgs e)
        {
            //Sets the tab control to the item locate tab and updates the tab control
            tbAppControl.SelectedTab = tbItemSearch;
            tbAppControl.Update();

            //Sets certain parameters specific to the item type and updates the list view to the locate item type
            tbSearchItem.SelectedTab = tbItemInfo;
            lstSearchItemsListView.Columns[6].Text = "Developer";
            lstSearchItemsListView.Columns[7].Text = "IGN Rating";
            searchItemType = typeof(VideoGame);
            UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);
        }
        private void tsmSubLocateBook_Click(object sender, EventArgs e)
        {
            //Preforms a click to locate a book
            tsmLocateBook.PerformClick();
        }
        private void tsmSubLocateMovie_Click(object sender, EventArgs e)
        {
            //Preforms a click to locate a movie
            tsmLocateMovie.PerformClick();
        }
        private void tsmSubLocateVideoGame_Click(object sender, EventArgs e)
        {
            //Preforms a click to locate a video game
            tsmLocateVideoGame.PerformClick();
        }
        private void btnMenuLocateBook_Click(object sender, EventArgs e)
        {
            //Preforms a click to locate a book
            tsmLocateBook.PerformClick();
        }
        private void btnMenuLocateMovie_Click(object sender, EventArgs e)
        {
            //Preforms a click to locate a movie
            tsmLocateMovie.PerformClick();
        }
        private void btnMenuLocateVideoGame_Click(object sender, EventArgs e)
        {
            //Preforms a click to locate a video game
            tsmLocateVideoGame.PerformClick();
        }
        private void btnLinkLocateItem_Click(object sender, EventArgs e)
        {
            //Preforms a click to locate a specific item type depending on the item type the user has chosen to display
            if (displayItemType == typeof(Book))
            {
                tsmLocateBook.PerformClick();
            }
            else if (displayItemType == typeof(Movie))
            {
                tsmLocateMovie.PerformClick();
            }
            else if (displayItemType == typeof(VideoGame))
            {
                tsmLocateVideoGame.PerformClick();
            }
        }
        private void lstSearchItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //stores the default item selected to be false
            bool itemSelected = false;
            
            //Checks if an item index is selected for every itemIndex in the locate item list view
            for (int itemIndex = 0; itemIndex < lstSearchItemsListView.Items.Count; itemIndex++)
            {
                //Checks if an item is selected
                if (lstSearchItemsListView.Items[itemIndex].Selected)
                {
                    //Sets the boolean value that indicates if an item is selected to true
                    itemSelected = true;

                    //Creates and stores the item properties from the locate ittem list view
                    string itemTitle = lstSearchItemsListView.Items[itemIndex].SubItems[1].Text;
                    string itemCost = lstSearchItemsListView.Items[itemIndex].SubItems[2].Text.Substring(1);
                    string itemGenre = lstSearchItemsListView.Items[itemIndex].SubItems[3].Text;
                    string itemPlatform = lstSearchItemsListView.Items[itemIndex].SubItems[4].Text;
                    string itemReleaseYear = lstSearchItemsListView.Items[itemIndex].SubItems[5].Text;
                    string itemInfoOne = lstSearchItemsListView.Items[itemIndex].SubItems[6].Text;
                    string itemInfoTwo = lstSearchItemsListView.Items[itemIndex].SubItems[7].Text;

                    //Sets the display item info text for the user to see individual data in a more simplistic way
                    lblItemTitle.Text = "Title: " + itemTitle;
                    lblItemCost.Text = "Cost: $" + itemCost;
                    lblItemGenre.Text = "Genre: " + itemGenre;
                    lblItemPlatform.Text = "Platform: " + itemPlatform;
                    lblItemReleaseYear.Text = "Release Year: " + itemReleaseYear;

                    //Sets the labels and buttons text to relect the item type
                    if (searchItemType == typeof(Book))
                    {
                        lblItemInfoOne.Text = "Author: ";
                        lblItemInfoTwo.Text = "Publisher: ";
                        btnModifyInfoOne.Text = "Author";
                        btnModifyInfoTwo.Text = "Publisher";
                    }
                    else if (searchItemType == typeof(Movie))
                    {
                        lblItemInfoOne.Text = "Director: ";
                        lblItemInfoTwo.Text = "Duration: ";
                        btnModifyInfoOne.Text = "Director";
                        btnModifyInfoTwo.Text = "Duration";
                    }
                    else if (searchItemType == typeof(VideoGame))
                    {
                        lblItemInfoOne.Text = "Developer: ";
                        lblItemInfoTwo.Text = "IGN Rating: ";
                        btnModifyInfoOne.Text = "Developer";
                        btnModifyInfoTwo.Text = "IGN Rating";
                    }

                    lblItemInfoOne.Text += itemInfoOne;
                    lblItemInfoTwo.Text += itemInfoTwo;

                    //Creates a new item with properties that the user has selected
                    selectedItem = CreateItem(searchItemType, itemTitle, itemCost, itemGenre, itemPlatform, itemReleaseYear, itemInfoOne, itemInfoTwo);
                }
            }
            //Checks if an item is not selected
            if (!itemSelected)
            {
                //Sets the modified info text and label to be invisible if an item is not selected
                lblModifiedInfo.Visible = false;
                txtModifiedInfo.Visible = false;
            }
        }
        private void txtSearchItemTitle_TextChanged(object sender, EventArgs e)
        {
            //Updates the locate item list view to match the entered item information
            UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);
        }
        private void txtSearchItemPlatform_TextChanged(object sender, EventArgs e)
        {
            //Updates the locate item list view to match the entered item 
            UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);
        }

        //Sort Inventory
        private void tsmSortCostIncreasing_Click(object sender, EventArgs e)
        {
            //Sorts the inventory by the cost from lowest to highest and updates the list view
            entertainmentInventory.SortInventory(SORT_COST, false);
            UpdateListViews(UPDATE_ALL_LIST_VIEWS);
        }
        private void tsmSortCostDecreasing_Click(object sender, EventArgs e)
        {
            //Sorts the inventory by the cost from highest to lowest and updates the list view
            entertainmentInventory.SortInventory(SORT_COST, true);
            UpdateListViews(UPDATE_ALL_LIST_VIEWS);
        }
        private void tsmSortAlphaneticallyAZ_Click(object sender, EventArgs e)
        {
            //Sorts the inventory alpohabetically from A-Z
            entertainmentInventory.SortInventory(SORT_AZ, false);
            UpdateListViews(UPDATE_ALL_LIST_VIEWS);
        }
        private void tsmSortAlphabeticallyZA_Click(object sender, EventArgs e)
        {
            //Sorts the inventory alpohabetically from Z-A
            entertainmentInventory.SortInventory(SORT_AZ, true);
            UpdateListViews(UPDATE_ALL_LIST_VIEWS);
        }
        private void tsmSortReleaseYearIncreasing_Click(object sender, EventArgs e)
        {
            //Sorts the inventory by release year from oldest to most recent
            entertainmentInventory.SortInventory(SORT_RELEASE_YEAR, false);
            UpdateListViews(UPDATE_ALL_LIST_VIEWS);
        }
        private void tsmSortReleaseYearDecreasing_Click(object sender, EventArgs e)
        {
            //Sorts the inventory by release year from most recent to lowest
            entertainmentInventory.SortInventory(SORT_RELEASE_YEAR, true);
            UpdateListViews(UPDATE_ALL_LIST_VIEWS);
        }
        private void tsmSubSortCostIncreasing_Click(object sender, EventArgs e)
        {
            //Preforms a click to sort the inventory by cost increasing
            tsmSortCostIncreasing.PerformClick();
        }
        private void tsmSubSortCostDecreasing_Click(object sender, EventArgs e)
        {
            //Preforms a click to sort the inventory by cost decreasing
            tsmSortCostDecreasing.PerformClick();
        }
        private void tsmSubSortAlphabeticallyAZ_Click(object sender, EventArgs e)
        {
            //Preforms a click to sort the inventory alphabetically from A-Z
            tsmSortAlphaneticallyAZ.PerformClick();
        }
        private void tsmSubSortAlphabeticallyZA_Click(object sender, EventArgs e)
        {
            //Preforms a click to sort the inventory alphabetically from Z-A
            tsmSortAlphabeticallyZA.PerformClick();
        }
        private void tsmSubSortReleaseYearIncreasing_Click(object sender, EventArgs e)
        {
            //Preforms a click to sort the inventory by release year from oldest to recent
            tsmSortReleaseYearIncreasing.PerformClick();
        }
        private void tsmSubSortReleaseYearDecreasing_Click(object sender, EventArgs e)
        {
            //Preforms a click to sort the inventory by release year from recent to oldest
            tsmSortReleaseYearDecreasing.PerformClick();
        }

        //Modify Item
        private void txtModifiedInfo_TextChanged(object sender, EventArgs e)
        {
            //Sets the confirm modification button to be visible
            btnConfirmModification.Visible = true;
        }
        private void btnConfirmModification_Click(object sender, EventArgs e)
        {
            //Creates a new entertainment item to store the modefied item and sets it equal to the original item before modification
            EntertainmentItem modifiedItem = selectedItem;
            
            //Checks which item property the user has changed and modifies the original item
            switch (modifyProperty)
            {
                case MODIFY_TITLE:
                    string originalTitle = selectedItem.GetTitle();
                    modifiedItem.SetTitle(txtModifiedInfo.Text.ToLower());
                    break;
                case MODIFY_COST:
                    modifiedItem.SetCost(Convert.ToDouble(txtModifiedInfo.Text));
                    break;
                case MODIFY_GENRE:
                    modifiedItem.SetGenre(txtModifiedInfo.Text.ToLower());
                    break;
                case MODIFY_PLATFORM:
                    modifiedItem.SetPlatform(txtModifiedInfo.Text.ToLower());
                    break;
                case MODIFY_RELEASE_YEAR:
                    modifiedItem.SetReleaseYear(Convert.ToInt32(txtModifiedInfo.Text));
                    break;
                case MODIFY_INFO_ONE:
                    if (searchItemType == typeof(Book))
                    {
                        ((Book)(modifiedItem)).SetAuthor(txtModifiedInfo.Text.ToLower());
                    }
                    else if (searchItemType == typeof(Movie))
                    {
                        ((Movie)(modifiedItem)).SetDirector(txtModifiedInfo.Text.ToLower());
                    }
                    else if (searchItemType == typeof(VideoGame))
                    {
                        ((VideoGame)(modifiedItem)).SetDeveloper(txtModifiedInfo.Text.ToLower());
                    }
                    break;
                case MODIFY_INFO_TWO:
                    if (searchItemType == typeof(Book))
                    {
                        ((Book)(modifiedItem)).SetPublisher(txtModifiedInfo.Text.ToLower());
                    }
                    else if (searchItemType == typeof(Movie))
                    {
                        ((Movie)(modifiedItem)).SetDuration(Convert.ToInt32(txtModifiedInfo.Text));
                    }
                    else if (searchItemType == typeof(VideoGame))
                    {
                        ((VideoGame)(modifiedItem)).SetRating(Convert.ToDouble(txtModifiedInfo.Text));
                    }
                    break;
            }

            //Resets the selectedItem to bypass a bug that changed the value of selectedItem
            for (int itemIndex = 0; itemIndex < lstSearchItemsListView.Items.Count; itemIndex++)
            {
                if (lstSearchItemsListView.Items[itemIndex].Selected)
                {
                    string itemTitle = lstSearchItemsListView.Items[itemIndex].SubItems[1].Text;
                    string itemCost = lstSearchItemsListView.Items[itemIndex].SubItems[2].Text.Substring(1);
                    string itemGenre = lstSearchItemsListView.Items[itemIndex].SubItems[3].Text;
                    string itemPlatform = lstSearchItemsListView.Items[itemIndex].SubItems[4].Text;
                    string itemReleaseYear = lstSearchItemsListView.Items[itemIndex].SubItems[5].Text;
                    string itemInfoOne = lstSearchItemsListView.Items[itemIndex].SubItems[6].Text;
                    string itemInfoTwo = lstSearchItemsListView.Items[itemIndex].SubItems[7].Text;

                    selectedItem = CreateItem(searchItemType, itemTitle, itemCost, itemGenre, itemPlatform, itemReleaseYear, itemInfoOne, itemInfoTwo);
                }
            }

            //Replaces the old item with the modified item and updates the search list
            entertainmentInventory.ReplaceItem(entertainmentInventory.GetItemIndex(selectedItem, entertainmentInventory.GetEntertainmentItems()), modifiedItem);
            UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);

            //Resets the text on the modified info text box
            txtModifiedInfo.ResetText();

            //Resets the text on the labels to nothing
            lblItemTitle.ResetText();
            lblItemCost.ResetText();
            lblItemGenre.ResetText();
            lblItemPlatform.ResetText();
            lblItemReleaseYear.ResetText();
            lblItemInfoOne.ResetText();
            lblItemInfoTwo.ResetText();

            //Sets the selected tab on the search tab control to the menu tab and display inventory information
            tbSearchItem.SelectedTab = tbSearchMenu;
            lblNumItems.Text = "Total Number of Items: " + (entertainmentInventory.GetNumItems(typeof(Book)) + entertainmentInventory.GetNumItems(typeof(Movie)) + entertainmentInventory.GetNumItems(typeof(VideoGame)));
            lblNumBooks.Text = "Total Number of Books: " + entertainmentInventory.GetNumItems(typeof(Book));
            lblNumMovies.Text = "Total Number of Movies: " + entertainmentInventory.GetNumItems(typeof(Movie));
            lblNumVideoGames.Text = "Total Number of Games: " + entertainmentInventory.GetNumItems(typeof(VideoGame));

        }
        private void btnModifyItem_Click(object sender, EventArgs e)
        {
            //Sets the locate item tab control to be visible and sets the tab to the modify item tab
            tbSearchItem.SelectedTab = tbModifyItem;
            tbSearchItem.Visible = true;
        }
        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            //Checks if the tab has returned to the item info tab
            if (tbSearchItem.SelectedTab == tbItemInfo)
            {
                //Removes the selected item from the entertainment inventory
                entertainmentInventory.RemoveItem(entertainmentInventory.GetItemIndex(selectedItem, entertainmentInventory.GetEntertainmentItems()));

                //Updates the locate item list view and sets the tab to be visible
                UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);
                tbSearchItem.Visible = true;

                //Informs the user that the item has been successfully removed
                lblSearchFeedback.Text = selectedItem.GetTitle() + " has been successfully removed.";

                //Sets the tab to be the menu tab in the search main tab and updates the inventory information
                tbSearchItem.SelectedTab = tbSearchMenu;
                lblNumItems.Text = "Total Number of Items: " + (entertainmentInventory.GetNumItems(typeof(Book)) 
                    + entertainmentInventory.GetNumItems(typeof(Movie)) 
                    + entertainmentInventory.GetNumItems(typeof(VideoGame)));
                lblNumBooks.Text = "Total Number of Books: " + entertainmentInventory.GetNumItems(typeof(Book));
                lblNumMovies.Text = "Total Number of Movies: " + entertainmentInventory.GetNumItems(typeof(Movie));
                lblNumVideoGames.Text = "Total Number of Games: " + entertainmentInventory.GetNumItems(typeof(VideoGame));
            }
            //Checks if the locate item tab control is on the modify item tab page
            else if (tbSearchItem.SelectedTab == tbModifyItem)
            {
                //Sets the tab to the info page
                tbSearchItem.SelectedTab = tbItemInfo;
            }

            lblItemTitle.ResetText();
            lblItemCost.ResetText();
            lblItemGenre.ResetText();
            lblItemPlatform.ResetText();
            lblItemReleaseYear.ResetText();
            lblItemInfoOne.ResetText();
            lblItemInfoTwo.ResetText();

            txtSearchItemTitle.ResetText();
            txtSearchItemPlatform.ResetText();

            tbSearchItem.SelectedTab = tbSearchMenu;
        }
        private void btnModifyTitle_Click(object sender, EventArgs e)
        {
            //Sets the modified info to reflect the item title
            modifyProperty = MODIFY_TITLE;
            lblModifiedInfo.Visible = true;
            txtModifiedInfo.Visible = true;

            //Moves the controls to be graphically leveled
            txtModifiedInfo.Top = btnModifyTitle.Top;
            lblModifiedInfo.Top = btnModifyTitle.Top - 16;
            btnConfirmModification.Top = btnModifyTitle.Top + 26;

            //Sets the label and text box where the user will enter a value to the default values
            txtModifiedInfo.Text = selectedItem.GetTitle();
            lblModifiedInfo.Text = "New Title";
        }
        private void btnModifyCost_Click(object sender, EventArgs e)
        {
            //Sets the modified info to reflect the item cost
            modifyProperty = MODIFY_COST;
            lblModifiedInfo.Visible = true;
            txtModifiedInfo.Visible = true;

            //Moves the controls to be graphically leveled
            txtModifiedInfo.Top = btnModifyCost.Top;
            lblModifiedInfo.Top = btnModifyCost.Top - 16;
            btnConfirmModification.Top = btnModifyCost.Top + 26;

            //Sets the label and text box where the user will enter a value to the default values
            txtModifiedInfo.Text = selectedItem.GetCost().ToString();
            lblModifiedInfo.Text = "New Cost ($)";
        }
        private void btnModifyGenre_Click(object sender, EventArgs e)
        {
            //Sets the modified info to reflect the item genre
            modifyProperty = MODIFY_GENRE;
            lblModifiedInfo.Visible = true;
            txtModifiedInfo.Visible = true;

            //Moves the controls to be graphically leveled
            txtModifiedInfo.Top = btnModifyGenre.Top;
            lblModifiedInfo.Top = btnModifyGenre.Top - 16;
            btnConfirmModification.Top = btnModifyGenre.Top + 26;

            //Sets the label and text box where the user will enter a value to the default values
            lblModifiedInfo.Text = "New Genre";
            txtModifiedInfo.Text = selectedItem.GetGenre();
        }
        private void btnModifyPlatform_Click(object sender, EventArgs e)
        {
            //Sets the modified info to reflect the item platform
            modifyProperty = MODIFY_PLATFORM;
            lblModifiedInfo.Visible = true;
            txtModifiedInfo.Visible = true;

            //Moves the controls to be graphically leveled
            txtModifiedInfo.Top = btnModifyPlatform.Top;
            lblModifiedInfo.Top = btnModifyPlatform.Top - 16;
            btnConfirmModification.Top = btnModifyPlatform.Top + 26;

            lblModifiedInfo.Text = "New Platform";
            txtModifiedInfo.Text = selectedItem.GetPlatform();
        }
        private void btnModifyReleaseYear_Click(object sender, EventArgs e)
        {
            //Sets the modified info to reflect the item release year
            modifyProperty = MODIFY_RELEASE_YEAR;
            lblModifiedInfo.Visible = true;
            txtModifiedInfo.Visible = true;

            //Moves the controls to be graphically leveled
            txtModifiedInfo.Top = btnModifyReleaseYear.Top;
            lblModifiedInfo.Top = btnModifyReleaseYear.Top - 16;
            btnConfirmModification.Top = btnModifyReleaseYear.Top + 26;

            //Sets the label and text box where the user will enter a value to the default 
            lblModifiedInfo.Text = "New Release Year";
            txtModifiedInfo.Text = selectedItem.GetReleaseYear().ToString();
        }
        private void btnModifyInfoOne_Click(object sender, EventArgs e)
        {
            //Sets the modified info to reflect the item's first info
            modifyProperty = MODIFY_INFO_TWO;
            lblModifiedInfo.Visible = true;
            txtModifiedInfo.Visible = true;

            //Moves the controls to be graphically leveled
            txtModifiedInfo.Top = btnModifyInfoOne.Top;
            lblModifiedInfo.Top = btnModifyInfoOne.Top - 16;
            btnConfirmModification.Top = btnModifyInfoOne.Top + 26;

            //Sets the label and text box where the user will enter a value to the default values
            if (searchItemType == typeof(Book))
            {
                lblModifiedInfo.Text = "New Author";
                txtModifiedInfo.Text = ((Book)selectedItem).GetAuthor();
            }
            else if (searchItemType == typeof(Movie))
            {
                lblModifiedInfo.Text = "New Director";
                txtModifiedInfo.Text = ((Movie)selectedItem).GetDirector();
            }
            else if (searchItemType == typeof(VideoGame))
            {
                lblModifiedInfo.Text = "New Developer";
                txtModifiedInfo.Text = ((VideoGame)selectedItem).GetDeveloper();
            }
        }
        private void btnModifyInfoTwo_Click(object sender, EventArgs e)
        {
            //Sets the modified info to reflect the item's second info
            modifyProperty = MODIFY_INFO_TWO;
            lblModifiedInfo.Visible = true;
            txtModifiedInfo.Visible = true;

            //Moves the controls to be graphically leveled
            txtModifiedInfo.Top = btnModifyInfoTwo.Top;
            lblModifiedInfo.Top = btnModifyInfoTwo.Top - 16;
            btnConfirmModification.Top = btnModifyInfoTwo.Top + 26;

            //Sets the label and text box where the user will enter a value to the default 
            if (searchItemType == typeof(Book))
            {
                lblModifiedInfo.Text = "New Publisher";
                txtModifiedInfo.Text = ((Book)selectedItem).GetPublisher();
            }
            else if (searchItemType == typeof(Movie))
            {
                lblModifiedInfo.Text = "New Duration";
                txtModifiedInfo.Text = ((Movie)selectedItem).GetDuration().ToString();
            }
            else if (searchItemType == typeof(VideoGame))
            {
                lblModifiedInfo.Text = "New IGN Rating";
                txtModifiedInfo.Text = ((VideoGame)selectedItem).GetRating().ToString();
            }
        }

        //Restrict User Entry
        private void txtAddItemTitle_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Restricts the item information from having commas
            e.Handled = (e.KeyChar == ',');
        }
        private void txtAddItemCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Restricts the user entry to only numbers and one decimal place
            if (txtAddItemCost.Text.Contains("."))
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
            else
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.');
            }
        }
        private void txtAddItemGenre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Restricts the item information to letters
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }
        private void txtAddItemPlatform_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Restricts the item information from having commas
            e.Handled = (e.KeyChar == ',');
        }
        private void txtAddItemReleaseYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Restricts the release year to a integer
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }
        private void txtAddItemInfoOne_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Restricts the item information from having commas
            e.Handled = (e.KeyChar == ',');
        }
        private void txtAddItemInfoTwo_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Restricts the user input based on the item type and property
            if (addItemType == typeof(Movie))
            {
                e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
            }
            else if (addItemType == typeof(VideoGame))
            {
                if (txtAddItemInfoTwo.Text.Contains("."))
                {
                    e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
                }
                else
                {
                    e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.');
                }
            }
            else if (addItemType == typeof(Book))
            {
                e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
            }
        }

        //Combo Button
        private void cmbMenuSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            numItemsPerView = Convert.ToInt32(cmbMenuSettings.Items[cmbMenuSettings.SelectedIndex]);
        }
        private void cmbMenuSettings_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //Pre: N/A
        //Post: N/A
        //Description: Reads the inventory.txt file and stores it in the entertainment inventory
        private void ReadFile()
        {
            //Creates a list of the lines that had format errors
            List<int> errorLines = new List<int>();

            //Opens the file to read
            inFile = File.OpenText(filePath);

            //Sets the maximum value of the progress bar to the amount of lines on the list
            pgbLoadItems.Maximum = GetNumItems(inFile);

            //Closes the file
            inFile.Close();

            //Opens the file to read
            inFile = File.OpenText(filePath);

            //Stores the individual segments of the line
            string[] lineData;

            //Stores the line the application is reading
            int lineNum = 0;

            //Computes the following while the line has not reached the end of the file
            while (inFile.EndOfStream == false)
            {
                //Creates a new entertainment item
                EntertainmentItem item = new EntertainmentItem();

                //Tries to add the item
                try
                {
                    //Increments the line number
                    lineNum += 1;

                    //Reads a line and stores it
                    string line = inFile.ReadLine().ToLower();

                    //Splits the line by commas into its individual properties
                    lineData = line.Split(',');

                    //Checks if the item is a movie
                    if (lineData[0].ToLower() == "movie")
                    {
                        //Creates a new movie and stores it
                        item = new Movie(lineData[1], Convert.ToDouble(lineData[2]), lineData[3], lineData[4], Convert.ToInt32(lineData[5]), lineData[6], Convert.ToInt32(lineData[7]));
                    }
                    //Checks if the item is a book
                    else if (lineData[0].ToLower() == "book")
                    {
                        //Creates a new book and stores it
                        item = new Book(lineData[1], Convert.ToDouble(lineData[2]), lineData[3], lineData[4], Convert.ToInt32(lineData[5]), lineData[6], lineData[7]);
                    }
                    //Checks if the item is a video game
                    else if (lineData[0].ToLower() == "game")
                    {
                        //Creates a new video game and stores it
                        item = new VideoGame(lineData[1], Convert.ToDouble(lineData[2]), lineData[3], lineData[4], Convert.ToInt32(lineData[5]), lineData[6], Convert.ToDouble(lineData[7]));
                    }
                    //Checks if the item is a movie
                    else
                    {
                        //Forces the application to encounter an error and move to the catch segment if the item is neither a book, movie, or entertainment item
                        int i = Convert.ToInt32("a");
                    }

                    //Adds the entertainment item to the entertainment inventory
                    entertainmentInventory.AddItem(item);
                    //Increments the loading bar by one
                    pgbLoadItems.Value += 1;
                }
                catch (Exception)
                {
                    errorLines.Add(lineNum);
                    pgbLoadItems.Maximum -= 1;
                }
            }
            inFile.Close();

            if (errorLines.Count > 0)
            {
                lblMenuFeedback.Text = "There was an error loading " + errorLines.Count + " items.";
            }
            else
            {
                if (entertainmentInventory.GetEntertainmentItems().Count > 0)
                {
                    lblMenuFeedback.Text = "All items were loaded correctly.";
                }
                else
                {
                    lblMenuFeedback.Text = "No items are in the inventory. Please load items";
                }
            }

            int GetNumItems(StreamReader inFile)
            {
                int numItems = 0;

                while (inFile.EndOfStream == false)
                {
                    inFile.ReadLine();
                    numItems++;
                }
                return numItems;
            }

        }

        //Pre: N/A
        //Post: N/A
        //Description: Write the inventory.txt file from the entertainment inventory
        private void WriteFile()
        {
            //Sets the progress bar to be visible
            pgbLoadItems.Visible = true;

            //Opens the inventory file
            outFile = File.CreateText(filePath);

            //Sets the progress bar value to 0
            pgbLoadItems.Value = 0;

            //Sets the maximum progress bar value to the amount of entertainment items
            pgbLoadItems.Maximum = entertainmentInventory.GetEntertainmentItems().Count;

            //For each item in the entertainment inventory, write the item info to the inventory file in the correct order
            for (int itemIndex = 0; itemIndex < entertainmentInventory.GetEntertainmentItems().Count; itemIndex++)
            {
                //Writes the item type
                outFile.Write(entertainmentInventory.GetEntertainmentItems()[itemIndex].GetItemType() + ",");

                //Writes the items properties
                outFile.WriteLine(entertainmentInventory.GetEntertainmentItems()[itemIndex].GetItemData());

                //Increment the progress bar value by one
                pgbLoadItems.Value += 1;
            }
            outFile.Close();
        }

        //Pre: The list view to update
        //Post: N/A
        //Description: Updates the list views on the application which are used to display data
        private void UpdateListViews(int specificList)
        {
            //Stores the number of items on the list view
            int numFilteredItems = 0;

            //Sets the progress bar value to 0
            pgbLoadItems.Value = 0;

            //Checks what the specific list view to update is
            switch (specificList)
            {
                case UPDATE_ALL_ITEM_LISTVIEW:
                    {
                        //Updates the all items list view
                        lstAllItemsListView = UpdateListView(typeof(EntertainmentItem), lstAllItemsListView);

                        //Sets the progress bar to be visible
                        pgbLoadItems.Visible = true;
                        break;
                    }
                case UPDATE_SPECIFIC_ITEM_LISTVIEW:
                    {
                        //Updates the top two item list view and the item specific list view
                        lstItemSpecificListView.Items.Clear();
                        lstItemSpecificListView.Update();
                        lstTopTwoSpecificItem = UpdateListView(displayItemType, lstTopTwoSpecificItem);
                        lstTopTwoSpecificItem.Update();
                        lstItemSpecificListView = UpdateListView(displayItemType, lstItemSpecificListView);
                        lstItemSpecificListView.Update();

                        //Sets the progress bar to be visible
                        pgbLoadItems.Visible = true;
                        break;
                    }
                case UPDATE_ADD_ITEM_LISTVIEW:
                    {
                        //Updates the add item list view
                        lstAddItemListView = UpdateListView(addItemType, lstAddItemListView);

                        //Sets the progress bar to be visible
                        pgbLoadItems.Visible = true;
                        break;
                    }
                case UPDATE_SEARCH_ITEM_LISTVIEW:
                    {
                        //Updates the locate item list view
                        lstSearchItemsListView = UpdateListView(searchItemType, lstSearchItemsListView);
                        //Sets the progress bar to be visible
                        pgbLoadItems.Visible = false;
                        break;
                    }
                case UPDATE_ALL_LIST_VIEWS:

                    //Updates all list views
                    UpdateListViews(UPDATE_ALL_ITEM_LISTVIEW);
                    UpdateListViews(UPDATE_SPECIFIC_ITEM_LISTVIEW);
                    UpdateListViews(UPDATE_ADD_ITEM_LISTVIEW);
                    UpdateListViews(UPDATE_SEARCH_ITEM_LISTVIEW);

                    break;
            }

            ListView UpdateListView(Type itemType, ListView listViewToUpdate)
            {
                //Sets a listView equal to the inputed list view
                ListView listView = listViewToUpdate;

                //Creates a list of entertainment items equal to the inventories items
                List<EntertainmentItem> entertainmentItems = entertainmentInventory.GetEntertainmentItems();

                //Stores whether or not the maximum amount of items has not been reached
                bool maxItems = false;

                //Clears the items on the list view
                listView.Items.Clear();

                //Stores the amount of items added so far
                int numItemsAdded;

                //Checks if the listview is the item specific list view or the add item list view
                if (listViewToUpdate == lstItemSpecificListView || listViewToUpdate == lstAddItemListView)
                {
                    //Gets the number of items in the entertainment inbentory of the correct type
                    for (int itemIndex = 0; itemIndex < entertainmentItems.Count; itemIndex++)
                    {
                        if (entertainmentItems[itemIndex].GetType().Equals(itemType) && !maxItems)
                        {
                            numFilteredItems++;
                        }
                    }

                    //Checks which is greator, the number of items per list view or the number of items in the filtered list and sets
                    //the maximum value of the progress bar to that the smaller value
                    if (numFilteredItems < numItemsPerView)
                    {
                        pgbLoadItems.Maximum = numFilteredItems;
                    }
                    else
                    {
                        pgbLoadItems.Maximum = numItemsPerView;
                    }

                    //For every item in the entertainment inventory
                    for (int itemIndex = 0; itemIndex < entertainmentItems.Count; itemIndex++)
                    {
                        //Checks if the item type matches the filtered type and that the number of items has not exeeded the maximum
                        if (entertainmentItems[itemIndex].GetType().Equals(itemType) && !maxItems)
                        {
                            //Gets the item information and splits it by the comma
                            string[] itemInfo = entertainmentItems[itemIndex].GetItemData().Split(',');

                            //Creates a list view item and sets the first list view value to be the item index
                            ListViewItem entertainmentItem = new ListViewItem(Convert.ToString(listView.Items.Count + 1));

                            //Adds the data to the listview
                            for (int i = 0; i < itemInfo.ToList().Count; i++)
                            {
                                //Adds the correct format for money
                                if (i == 1)
                                {
                                    if (Convert.ToDouble(itemInfo[i]) == (int)Convert.ToDouble(itemInfo[i]))
                                    {
                                        itemInfo[i] = Convert.ToDouble(itemInfo[i]) + ".00";
                                    }
                                    else if (Convert.ToDouble(itemInfo[i]) == Math.Round(Convert.ToDouble(itemInfo[i]), 1))
                                    {
                                        itemInfo[i] = Convert.ToDouble(itemInfo[i]) + "0";
                                    }
                                    entertainmentItem.SubItems.Add("$" + itemInfo[i]);
                                }
                                //Adds the suffix minutes for duration if the item is a movie
                                else if (i == 6 && entertainmentItems[itemIndex] is Movie)
                                {
                                    entertainmentItem.SubItems.Add(itemInfo[i] + " Minutes");
                                }
                                //Adds the rest of the item info
                                else
                                {
                                    entertainmentItem.SubItems.Add(itemInfo[i]);
                                }
                            }
                            //Checks if the number of items on the list view has exceeded its max value
                            if (listView.Items.Count < numItemsPerView && pgbLoadItems.Value != pgbLoadItems.Maximum)
                            {
                                //Adds the item to the list view and increments the progress bar by one
                                listView.Items.Add(entertainmentItem);
                                pgbLoadItems.Value += 1;
                            }
                            else
                            {
                                //Sets maxItems which represents if the list view is full to true
                                maxItems = true;
                            }
                        }
                    }
                }
                else if (listViewToUpdate == lstAllItemsListView)
                {
                    //Checks which is greator, the number of items per list view or the number of items in the filtered list and sets
                    //the maximum value of the progress bar to that the smaller value
                    if (entertainmentInventory.GetEntertainmentItems().Count < numItemsPerView)
                    {
                        pgbLoadItems.Maximum = entertainmentInventory.GetEntertainmentItems().Count;
                    }
                    else
                    {
                        pgbLoadItems.Maximum = numItemsPerView;
                    }
                    
                    //For every item in the inventory
                    for (int itemIndex = 0; itemIndex < entertainmentItems.Count; itemIndex++)
                    {
                        //If the maximum amount of items has not been reached
                        if (!maxItems)
                        {
                            //Gets the item information and splits it by the comma
                            string[] itemInfo = entertainmentItems[itemIndex].GetListData().Split(',');

                            //Creates a list view item and sets the first list view value to be the item index
                            ListViewItem entertainmentItem = new ListViewItem(Convert.ToString(itemIndex + 1));

                            //Adds the item type
                            entertainmentItem.SubItems.Add(entertainmentItems[itemIndex].GetItemType());

                            //Adds the data to the listview
                            for (int i = 0; i < itemInfo.ToList().Count; i++)
                            {
                                //Adds the correct format for money
                                if (i == 1)
                                {
                                    if (Convert.ToDouble(itemInfo[i]) == (int)Convert.ToDouble(itemInfo[i]))
                                    {
                                        itemInfo[i] = Convert.ToDouble(itemInfo[i]) + ".00";
                                    }
                                    else if (Convert.ToDouble(itemInfo[i]) == Math.Round(Convert.ToDouble(itemInfo[i]), 1))
                                    {
                                        itemInfo[i] = Convert.ToDouble(itemInfo[i]) + "0";
                                    }
                                    entertainmentItem.SubItems.Add("$" + itemInfo[i]);
                                }
                                else
                                {
                                    //Adds the rest of the item info
                                    entertainmentItem.SubItems.Add(itemInfo[i]);
                                }
                            }

                            //Adds the item to the list view and increments the progress bar by one
                            listView.Items.Add(entertainmentItem);
                            pgbLoadItems.Value += 1;

                            //Checks if the number of items in the list view has reached its maximum
                            if (listView.Items.Count == numItemsPerView)
                            {
                                //Sets the boolean representing whether or not the maximum amount of items has been reached to true
                                maxItems = true;
                            }
                        }
                    }
                }
                else if (listViewToUpdate == lstTopTwoSpecificItem)
                {
                    //Sets the number of items added to 0
                    numItemsAdded = 0;

                    //For  every item int he entertainment item inventory
                    for (int itemIndex = 0; itemIndex < entertainmentItems.Count; itemIndex++)
                    {
                        //Checks if the item is of the user inputed type and that less than 2 items have been added
                        if (entertainmentItems[itemIndex].GetType().Equals(itemType) && numItemsAdded < 2)
                        {
                            //Stores the item info and splits it by the comma
                            string[] itemInfo = entertainmentItems[itemIndex].GetItemData().Split(',');

                            //Creates a list view item and sets the first list view value to be the item index
                            ListViewItem entertainmentItem = new ListViewItem(Convert.ToString(listView.Items.Count + 1));

                            //Adds the data to the listview
                            for (int i = 0; i < itemInfo.ToList().Count; i++)
                            {
                                //Adds the correct format for money
                                if (i == 1)
                                {
                                    if (Convert.ToDouble(itemInfo[i]) == (int)Convert.ToDouble(itemInfo[i]))
                                    {
                                        itemInfo[i] = Convert.ToDouble(itemInfo[i]) + ".00";
                                    }
                                    else if (Convert.ToDouble(itemInfo[i]) == Math.Round(Convert.ToDouble(itemInfo[i]), 1))
                                    {
                                        itemInfo[i] = Convert.ToDouble(itemInfo[i]) + "0";
                                    }
                                    entertainmentItem.SubItems.Add("$" + itemInfo[i]);
                                }
                                //Adds the suffix minutes for duration if the item is a movie
                                else if (i == 6 && entertainmentItems[itemIndex] is Movie)
                                {
                                    entertainmentItem.SubItems.Add(itemInfo[i] + " Minutes");
                                }
                                //Adds the rest of the item info to the listView
                                else
                                {
                                    entertainmentItem.SubItems.Add(itemInfo[i]);
                                }
                            }

                            //Addes the item to the list view and increments the number of items added by one
                            listView.Items.Add(entertainmentItem);
                            numItemsAdded += 1;
                        }
                    }
                }
                else if (listViewToUpdate == lstSearchItemsListView)
                {
                    //For ebery item in the entertainment inventory
                    for (int itemIndex = 0; itemIndex < entertainmentInventory.GetEntertainmentItems().Count; itemIndex++)
                    {
                        //Checks if the item is of the certain type and that the number of items on the list view has not exceeded its maximum value
                        if (entertainmentInventory.GetEntertainmentItems()[itemIndex].GetType().Equals(searchItemType) && lstSearchItemsListView.Items.Count < numItemsPerView)
                        {
                            //Checks if the search bar text matches the data of the item
                            if ((entertainmentInventory.GetEntertainmentItems()[itemIndex].GetTitle().ToLower().Contains(txtSearchItemTitle.Text.ToLower()) || txtSearchItemTitle.Text.Length == 0)
                                && (entertainmentInventory.GetEntertainmentItems()[itemIndex].GetPlatform().ToLower().Contains(txtSearchItemPlatform.Text.ToLower()) || txtSearchItemPlatform.Text.Length == 0))
                            {
                                //Splits the item info by the comma and stores it
                                string[] itemInfo = entertainmentInventory.GetEntertainmentItems()[itemIndex].GetItemData().Split(',');

                                //Creates a list view item and sets the first list view value to be the item index
                                ListViewItem entertainmentItem = new ListViewItem(Convert.ToString(lstSearchItemsListView.Items.Count + 1));

                                //Adds the data to the listview
                                for (int i = 0; i < itemInfo.ToList().Count; i++)
                                {
                                    //Adds the correct format for money
                                    if (i == 1)
                                    {
                                        if (Convert.ToDouble(itemInfo[i]) == (int)Convert.ToDouble(itemInfo[i]))
                                        {
                                            itemInfo[i] = Convert.ToDouble(itemInfo[i]) + ".00";
                                        }
                                        else if (Convert.ToDouble(itemInfo[i]) == Math.Round(Convert.ToDouble(itemInfo[i]), 1))
                                        {
                                            itemInfo[i] = Convert.ToDouble(itemInfo[i]) + "0";
                                        }
                                        entertainmentItem.SubItems.Add("$" + itemInfo[i]);
                                    }
                                    //Adds the suffix minutes for duration if the item is a movie
                                    else if (i == 6 && entertainmentInventory.GetEntertainmentItems()[itemIndex] is Movie)
                                    {
                                        entertainmentItem.SubItems.Add(itemInfo[i] + " Minutes");
                                    }
                                    //Adds the rest of the item info to the listView
                                    else
                                    {
                                        entertainmentItem.SubItems.Add(itemInfo[i]);
                                    }
                                }

                                //Addes the item to the list view
                                listViewToUpdate.Items.Add(entertainmentItem);
                            }
                        }
                    }
                }
                //Retuns the new listview
                return listView;
            }
        }

        //Pre: Item type, item title, item cost, item genre, item platform, item release year, item info one, item info two
        //Post: An entertainment item
        //Description: Creates an entertainment item with the properties as inputed
        private EntertainmentItem CreateItem(Type itemType, string itemTitle, string itemCost, string itemGenre,
            string itemPlatform, string itemReleaseYear, string itemInfoOne, string itemInfoTwo)
        {
            //Sets the parameters to be lower case
            itemTitle.ToLower();
            itemCost.ToLower();
            itemGenre.ToLower();
            itemPlatform.ToLower();
            itemReleaseYear.ToLower();
            itemInfoOne.ToLower();
            itemInfoTwo.ToLower();

            //Checks if the item is a book and if so returns a new book
            if (itemType == typeof(Book))
            {
                return new Book(itemTitle, Convert.ToDouble(itemCost), itemGenre, itemPlatform,
                                           Convert.ToInt32(itemReleaseYear), itemInfoOne, itemInfoTwo);
            }
            //Checks if the item is a movie and if so returns a new movie
            else if (itemType == typeof(Movie))
            {
                return new Movie(itemTitle, Convert.ToDouble(itemCost), itemGenre, itemPlatform,
                                           Convert.ToInt32(itemReleaseYear), itemInfoOne, Convert.ToInt32(itemInfoTwo.Split(' ')[0]));
            }
            //Checks if the item is a video game and if so returns a video game
            else if (itemType == typeof(VideoGame))
            {
                return new VideoGame(itemTitle, Convert.ToDouble(itemCost), itemGenre, itemPlatform,
                                           Convert.ToInt32(itemReleaseYear), itemInfoOne, Convert.ToDouble(itemInfoTwo));
            }
            
            //Returns an empty entertainment item. This line will never be called
            return new EntertainmentItem();
        }

        //Pre: N/A
        //Post: AS boolean representing if all text boxes on the add item tab have text
        //Description: Checks if all the add item text boxes have text
        private bool ScanAddInputs()
        {
            //Returns a boolean that stores if all the text boxes have text
            return txtAddItemTitle.Text.Length > 0 && txtAddItemCost.Text.Length > 0 && txtAddItemGenre.Text.Length > 0
                && txtAddItemPlatform.Text.Length > 0 && txtAddItemReleaseYear.Text.Length > 0 && txtAddItemInfoOne.Text.Length > 0
                && txtAddItemInfoTwo.Text.Length > 0;
        }
    }
}