using System;
using System.Collections.Generic;

namespace Lncodes.Example.Delegate;

/// <summary>
/// Initializes a new instance of the <see cref="InventoryController"/> class.
/// </summary>
/// <param name="deleteItemCallback">Callback to be invoked when an item is successfully deleted.</param>
public sealed class InventoryController(InventoryController.DeleteItemCallback deleteItemCallback)
{
    private readonly List<string> _itemCollection = [];

    // Delegate declaration
    public delegate void DeleteItemCallback(int itemIndex, string itemName);
    private readonly DeleteItemCallback _successDeleteCallback = deleteItemCallback;

    /// <summary>
    /// Adds an item to the inventory.
    /// </summary>
    /// <param name="item">The item to be added.</param>
    /// <param name="successAddCallback">Callback invoked when the item is successfully added.</param>
    public void AddItem(string item, Action<string> successAddCallback)
    {
        _itemCollection.Add(item);
        successAddCallback(item);
    }

    /// <summary>
    /// Adds an item to the inventory using a function to get the item.
    /// </summary>
    /// <param name="getRandomItem">Function that provides the item to be added.</param>
    /// <param name="successAddCallback">Callback invoked when the item is successfully added.</param>
    public void AddItem(Func<string> getRandomItem, Action<string> successAddCallback)
    {
        var item = getRandomItem();
        _itemCollection.Add(item);
        successAddCallback(item);
    }

    /// <summary>
    /// Adds an item to the inventory if a condition is met.
    /// </summary>
    /// <param name="getRandomItem">Function that provides the item to be added.</param>
    /// <param name="successAddCallback">Callback invoked when the item is successfully added.</param>
    /// <param name="canAddingItems">Predicate to check if more items can be added.</param>
    public void AddItem(Func<string> getRandomItem, Action<string> successAddCallback, Predicate<int> canAddingItems)
    {
        if (canAddingItems(_itemCollection.Count))
        {
            var item = getRandomItem();
            _itemCollection.Add(item);
            successAddCallback(item);
        }
        else Console.WriteLine("3. Failed to add item to inventory as it has reached its maximum capacity.");
    }

    /// <summary>
    /// Deletes an item from the inventory.
    /// </summary>
    /// <param name="itemIndex">Index of the item to be deleted.</param>
    public void DeleteItem(int itemIndex)
    {
        var itemName = _itemCollection[itemIndex];
        _itemCollection.RemoveAt(itemIndex);
        _successDeleteCallback(itemIndex, itemName);
    }
}