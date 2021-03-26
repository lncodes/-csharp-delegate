using System;
using System.Collections.Generic;

namespace Lncodes.Example.Delegate
{
    public sealed class InventoryController
    {
        public readonly int MaxCapacity = 3;
        private readonly List<string> _itemCollection = new List<string>();

        //Declare Delegate Using Delegate Keyword
        public delegate void DeleteItemCallback(int itemIndex, string itemName);
        private readonly DeleteItemCallback _deleteItemCallback;

        //Constructor
        public InventoryController(DeleteItemCallback deleteItem) =>
            _deleteItemCallback = deleteItem;

        /// <summary>
        /// Method for adding item to inventory
        /// </summary>
        /// <param name="item">Item Want To Add To Inventory</param>
        /// <param name="addItemCallback">Delegate Call When Success Adding Item</param>
        public void AddingItem(string item, Action<string> addItemCallback)
        {
            if (_itemCollection.Count < MaxCapacity)
            {
                _itemCollection.Add(item);
                addItemCallback(item);
            }
            else Console.WriteLine("Your inventory has reached max capacity");
        }

        /// <summary>
        /// Method for adding item to inventory
        /// </summary>
        /// <param name="getRandomItem">Delegate Return Item</param>
        /// <param name="addItemCallback">Delegate Call When Success Adding Item</param>
        public void AddingItem(Func<string> getRandomItem, Action<string> addItemCallback)
        {
            if (_itemCollection.Count < MaxCapacity)
            {
                var item = getRandomItem();
                _itemCollection.Add(item);
                addItemCallback(item);
            }
            else Console.WriteLine("Your inventory has reached max capacity");
        }

        /// <summary>
        /// Method for adding item to inventory
        /// </summary>
        /// <param name="getRandomItem">Delegate Return Item</param>
        /// <param name="addItemCallback">Delegate Call When Success Adding Item</param>
        /// <param name="canAddingItems">Delegate Check If Can Adding New Item</param>
        public void AddingItem(Func<string> getRandomItem, Action<string> addItemCallback, Predicate<int> canAddingItems)
        {
            if (canAddingItems(_itemCollection.Count))
            {
                var item = getRandomItem();
                _itemCollection.Add(item);
                addItemCallback(item);
            }
            else Console.WriteLine("Your inventory has reached max capacity");
        }

        /// <summary>
        /// Method for delete item from inventory
        /// </summary>
        /// <param name="itemIndex">Item index that want to delete</param>
        public void DeleteItem(int itemIndex)
        {
            var itemName = _itemCollection[itemIndex];
            _itemCollection.RemoveAt(itemIndex);
            _deleteItemCallback(itemIndex, itemName);
        }

        /// <summary>
        /// Method to show all item in inventory
        /// </summary>
        public void ShowAllItem()
        {
            Console.WriteLine();
            Console.WriteLine("All items in inventory");
            _itemCollection.ForEach(Console.WriteLine);
        }
    }
}