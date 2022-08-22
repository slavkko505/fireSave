using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Item item;

    public void PickUp()
    {
        Inventory.instanse.AddItem(item);
        Destroy(gameObject);
    }
    
    public static void HealPlayer(Item item)
    {
        switch (item.Dia)
        {
            default:
                case Item.DiaItem.Heal:
                    Debug.Log("Heal");
                break;
            case Item.DiaItem.Speed:
                Debug.Log("speed");
                break;
            case Item.DiaItem.Nostihng:
                Debug.Log("nothing");
                break;
        }
    }
    
}
