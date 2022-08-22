
using System;

[Serializable]
public class CounterStuff
{
    private Item item;
    public int CountStuff { get; set; }
    
    
    public CounterStuff() { }

    public CounterStuff(Item item, int amount)
    {
        this.item = item;
        CountStuff = amount;
    }

    public void AddStuff(int amount)
    {
        CountStuff += amount;
    }

    public string GetNameItem()
    {
        return item.Name;
    }

    public Item GetItem()
    {
        return item;
    }
}
