using System;
using System.Security.Cryptography;

namespace Lncodes.Example.Delegate
{
    public class Program
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected Program() { }

        /// <summary>
        /// Main Program
        /// </summary>
        private static void Main()
        {
            //Create delete item delegate
            var deleteItemCallback = new InventoryController.DeleteItemCallback(
                (itemIndex, itemName) => Console.WriteLine($"Sucess delete items {itemName} at index {itemIndex}")
            );

            var inventoryController = new InventoryController(deleteItemCallback);
            AddItemToInventory(inventoryController);
            DeleteItemFromInveotry(inventoryController);
            ShowAllItem(inventoryController);
        }

        /// <summary>
        /// Method to adding item to inventory
        /// </summary>
        /// <param name="inventoryController"></param>
        private static void AddItemToInventory(InventoryController inventoryController)
        {
            inventoryController.AddingItem("Potion",
                (item) => Console.WriteLine($"Sucess Adding Item : {item}"));

            inventoryController.AddingItem(() => GetRandomItem(GetRandomItemId()),
                (item) => Console.WriteLine($"Sucess Adding Item : {item}"));

            inventoryController.AddingItem(() => GetRandomItem(GetRandomItemId()),
                (item) => Console.WriteLine($"Sucess Adding Item : {item}"),
                (ammountOfItem) => ammountOfItem < inventoryController.MaxCapacity);
        }

        /// <summary>
        /// Method for delete item in inventory
        /// </summary>
        /// <param name="inventoryController"></param>
        private static void DeleteItemFromInveotry(InventoryController inventoryController)
        {
            int deletedItemIndex = 1;
            inventoryController.DeleteItem(deletedItemIndex);
        }

        /// <summary>
        /// Method to show all item in inventory
        /// </summary>
        /// <param name="inventoryController"></param>
        private static void ShowAllItem(InventoryController inventoryController) =>
            inventoryController.ShowAllItem();

        /// <summary>
        /// Method for random item
        /// </summary>
        /// <returns>A String Represent Random Item For Add To Inventory</returns>
        /// <exception cref="Exception">Thrown when random value > 3</exception>
        private static string GetRandomItem(int itemId)
        {
            switch (itemId)
            {
                case 0:
                    return "Weapon";
                case 1:
                    return "Potion";
                case 2:
                    return "Armor";
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemId));
            }
        }

        /// <summary>
        /// Method for get random item id
        /// </summary>
        /// <returns cref=int></returns>
        private static int GetRandomItemId()
        {
            var ammoutOfItemTypes = 3;
            return RandomNumberGenerator.GetInt32(0, ammoutOfItemTypes);
        }
    }
}