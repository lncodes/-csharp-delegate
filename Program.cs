using System;

namespace Lncodes.Example.Delegate
{
    public class Program
    {
        static void Main()
        {
            //Create delete item delegate
            var deleteItemCallback = new InventoryController.DeleteItemCallback(
                (itemIndex, itemName) => Console.WriteLine($"Sucess delete items {itemName} at index {itemIndex}")
            );

            //Create InventoryController instance
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
            //Calling Inventory Controller Method
            inventoryController.AddingItem("Potion",
                (item) => Console.WriteLine($"Sucess Adding Item : {item}"));

            inventoryController.AddingItem(GetRandomItem,
                (item) => Console.WriteLine($"Sucess Adding Item : {item}"));

            inventoryController.AddingItem(GetRandomItem,
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
        private static string GetRandomItem()
        {
            switch (new Random().Next(3))
            {
                case 0:
                    return "Weapon";
                case 1:
                    return "Potion";
                case 2:
                    return "Armor";
                default:
                    throw new Exception("Error random item");
            }
        }
    }
}