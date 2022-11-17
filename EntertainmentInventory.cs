//Author: Daniel Akselrod
//File Name: EntertainmentInventory.cs
//Project Name: AmazonInventoryManager
//Creation Date: March 8, 2020
//Modified Date: May 7, 2020
//Description: The purpose of this class is to store the attributtes and behaviours of an entertainment inventory

using System;
using System.Collections.Generic;

namespace AmazonInventoryManager
{
    class EntertainmentInventory
    {
        //Stores a list of entertianment items
        private List<EntertainmentItem> entertainmentItems;

        //Pre: N/A
        //Post: N/A
        //Description: The default constructor of the entertainment inventory makes a new list of entertainment items
        public EntertainmentInventory()
        {
            entertainmentItems = new List<EntertainmentItem>();
        }

        //Pre: N/A
        //Post: Returns the entertainment inventory
        //Description: Returns the entertainment inventory as a list of entertainment items
        public List<EntertainmentItem> GetEntertainmentItems()
        {
            return entertainmentItems;
        }

        //Pre: N/A
        //Post: The  index of a specific entertainment item
        //Description: Locates an item in the inventory and returns the  index
        public int GetItemIndex(EntertainmentItem entertainmentItem, List<EntertainmentItem> entertainmentItems)
        {
            for (int itemIndex = 0; itemIndex < entertainmentItems.Count; itemIndex++)
            {
                if (entertainmentItem.GetItemData().ToLower() == entertainmentItems[itemIndex].GetItemData().ToLower())
                {
                    return itemIndex;
                }
            }
            return -1;
        }

        //Pre: N/A
        //Post:The inventory size of a specific item type
        //Description: Finds the number of items of a specific item type
        public int GetNumItems(Type itemType)
        {
            int numItemType = 0;
            for (int itemIndex = 0; itemIndex < entertainmentItems.Count; itemIndex ++)
            {
                if (entertainmentItems[itemIndex].GetType() == itemType)
                {
                    numItemType += 1;
                }
            }

            return numItemType;
        }

        //Pre: A list of entertainment items
        //Post: N/A
        //Description: Sets the entertainment inventory
        public void SetEntertainmentItems(List<EntertainmentItem> entertainmentItems)
        {
            this.entertainmentItems = entertainmentItems;
        }

        //Pre: Item Index
        //Post: N/A
        //Description: Removes an entertainment item at a specific indexin the inventory
        public void RemoveItem(int itemIndex)
        {
            entertainmentItems.RemoveAt(itemIndex);
        }

        //Pre: The entertainment item to add
        //Post: N/A
        //Description: Adds an entertainment item to the inventory
        public void AddItem(EntertainmentItem entertainmentItem)
        {
            entertainmentItems.Add(entertainmentItem);
        }

        //Pre: The item index to replace the item, the new item
        //Post: N/A
        //Description: Replaces an entertainment item with another one in the same position
        public void ReplaceItem(int oldItemIndex, EntertainmentItem modifiedItem)
        {
            entertainmentItems[oldItemIndex] = modifiedItem;
        }

        //Pre: The sort type, whether or not the inventory should be reversed
        //Post: N/A
        //Description: Sorts the inventory in one of 6 possible ways
        public void SortInventory(int sortType, bool reverse)
        {
            //Stores the ways that the iunventory can be sorted
            const int SORT_COST = 1;
            const int SORT_AZ = 2;
            const int SORT_RELEASE_YEAR = 3;

            //Stores whether or not the inventory should be reversed
            bool itemsSorted = false;

            switch (sortType)
            {
                case SORT_COST:
                    //Sorts the items by cost from smallest to greatest
                    while (!itemsSorted)
                    {
                        itemsSorted = true;
                        for (int itemIndex = 0; itemIndex < entertainmentItems.Count - 1; itemIndex++)
                        {
                            if (entertainmentItems[itemIndex].GetCost() > entertainmentItems[itemIndex + 1].GetCost())
                            {
                                EntertainmentItem tempItem = entertainmentItems[itemIndex];
                                entertainmentItems[itemIndex] = entertainmentItems[itemIndex + 1];
                                entertainmentItems[itemIndex + 1] = tempItem;
                                itemsSorted = false;
                            }
                        }
                    }

                    break;
                case SORT_AZ:
                    //Sorts the items from A-Z
                    while (!itemsSorted)
                    {
                        itemsSorted = true;
                        for (int itemIndex = 0; itemIndex < entertainmentItems.Count - 1 && itemIndex < 100000; itemIndex++)
                        {
                            if (entertainmentItems[itemIndex].GetTitle().ToLower()[0] > entertainmentItems[itemIndex + 1].GetTitle().ToLower()[0])
                            {
                                EntertainmentItem tempItem = entertainmentItems[itemIndex];
                                entertainmentItems[itemIndex] = entertainmentItems[itemIndex + 1];
                                entertainmentItems[itemIndex + 1] = tempItem;
                                itemsSorted = false;
                            }
                            else if (entertainmentItems[itemIndex].GetTitle().ToLower()[0] == entertainmentItems[itemIndex + 1].GetTitle().ToLower()[0])
                            {
                                if (entertainmentItems[itemIndex].GetTitle().ToLower()[1] > entertainmentItems[itemIndex + 1].GetTitle().ToLower()[1])
                                {
                                    EntertainmentItem tempItem = entertainmentItems[itemIndex];
                                    entertainmentItems[itemIndex] = entertainmentItems[itemIndex + 1];
                                    entertainmentItems[itemIndex + 1] = tempItem;
                                    itemsSorted = false;
                                }
                                else if (entertainmentItems[itemIndex].GetTitle().ToLower()[1] == entertainmentItems[itemIndex + 1].GetTitle().ToLower()[1])
                                {
                                    if (entertainmentItems[itemIndex].GetTitle().ToLower()[2] > entertainmentItems[itemIndex + 1].GetTitle().ToLower()[2])
                                    {
                                        EntertainmentItem tempItem = entertainmentItems[itemIndex];
                                        entertainmentItems[itemIndex] = entertainmentItems[itemIndex + 1];
                                        entertainmentItems[itemIndex + 1] = tempItem;
                                        itemsSorted = false;
                                    }
                                }
                            }
                        }
                    }

                    break;
                case SORT_RELEASE_YEAR:
                    //Sorts the items from oldest to most recent release year
                    while (!itemsSorted)
                    {
                        itemsSorted = true;
                        for (int itemIndex = 0; itemIndex < entertainmentItems.Count - 1 && itemIndex < 100000; itemIndex++)
                        {
                            if (entertainmentItems[itemIndex].GetReleaseYear() > entertainmentItems[itemIndex + 1].GetReleaseYear())
                            {
                                EntertainmentItem tempItem = entertainmentItems[itemIndex];
                                entertainmentItems[itemIndex] = entertainmentItems[itemIndex + 1];
                                entertainmentItems[itemIndex + 1] = tempItem;
                                itemsSorted = false;
                            }
                        }
                    }
                    break;
            }
            
            //Reverses the entertainment inventory
            if (reverse)
            {
                entertainmentItems.Reverse();
            }
        }
    }
}
