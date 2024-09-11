using System;
using System.Security.Cryptography;

namespace Lncodes.Example.Delegate;

internal static class Program
{
    /// <summary>
    /// Main entry point for the program.
    /// </summary>
    private static void Main()
    {
        //Initialize inventory controller with a delete item callback
        var deleteItemCallback = new InventoryController.DeleteItemCallback(
            (itemIndex, itemName) => Console.WriteLine($"4. Successfully deleted item '{itemName}' at index {itemIndex} in the inventory.")
        );

        var inventoryController = new InventoryController(deleteItemCallback);
        AddItemsToInventory(inventoryController);
        inventoryController.DeleteItem(1);
    }

    /// <summary>
    /// Adds items to the inventory using various methods.
    /// </summary>
    /// <param name="inventoryController">The inventory controller instance.</param>
    private static void AddItemsToInventory(InventoryController inventoryController)
    {
        // Add a specific item
        inventoryController.AddItem("Potion",
            (item) => Console.WriteLine($"1. Successfully added '{item}' item to the inventory."));

        // Add a random item
        inventoryController.AddItem(GetRandomItem,
            (item) => Console.WriteLine($"2. Successfully added random item '{item}\' to the inventory."));

        // Add a random item with a capacity check
        const int maxItemInInventory = 2;
        inventoryController.AddItem(GetRandomItem,
            (item) => Console.WriteLine($"3. Successfully added random item '{item}' to the inventory."),
            (amountOfItemInInventory) => amountOfItemInInventory < maxItemInInventory);
    }

    /// <summary>
    /// Get a random item to be added to the inventory.
    /// </summary>
    /// <returns>A string representing a random item.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the random value is out of expected range.</exception>
    private static string GetRandomItem()
    {
        var itemId = RandomNumberGenerator.GetInt32(3);
        return itemId switch
        {
            0 => "Weapon",
            1 => "Potion",
            2 => "Armor",
            _ => throw new InvalidOperationException($"Unexpected Item ID: {itemId}")
        };
    }
}