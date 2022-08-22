using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instanse;

    [SerializeField] private Canvas _canvas;
    //[SerializeField] private List<Item> inventory;
    
    //InventoryWithCount
    [SerializeField] private List<CounterStuff> inventory;
    
    public Transform ItemContent;
    public ItemSlotInUI InventoryItem;
    private void Awake()
    {
        if (instanse == null)
        {
            instanse = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        _canvas.enabled = false;
    }

    public void AddItem(Item item)
    {
        bool t = false;
        if (inventory.Count > 0)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].GetNameItem().ToUpper() == item.Name.ToUpper() && item.Type == Item.AddingObject.Adding)
                {
                    inventory[i].CountStuff += item.Amount;
                    t = true;
                    break;
                }
            }

            if (!t)
            {
                inventory.Add(new CounterStuff(item, item.Amount));
            }
            
        }
        else
        {
            inventory.Add(new CounterStuff(item, item.Amount));
        }

        RefreshList();
    }

    public void RemoveItem(Item item)
    {
        bool t = false;
        foreach (var e in inventory)
        {
            if (item.Name == e.GetNameItem())
            {
                if (e.CountStuff >= 2)
                {
                    e.CountStuff--;
                    t = true;
                }
            }
        }
        if(!t)
            Remove(item);
        
        RefreshList();
    }

    public void Remove(Item item)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
           if (inventory[i].GetItem().Name == item.Name)
           {
               inventory.Remove(inventory[i]);
           } 
        }
            
        
    }
    public void ShowInventory()
    {
        _canvas.enabled = !_canvas.isActiveAndEnabled;
    }

    public void RefreshList()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }
        
        foreach (var item in inventory)
        {
            ItemSlotInUI obj = Instantiate(InventoryItem, ItemContent);
            var itemIcon = obj._image;
            itemIcon.sprite = item.GetItem().Icon;
            obj._text.text = item.CountStuff.ToString();
            obj._item = item.GetItem();

        }
    }
}
