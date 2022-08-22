using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotInUI : MonoBehaviour, IPointerClickHandler
{
   [SerializeField] public Image _image;
   [SerializeField] public TextMeshProUGUI _text;
   
   public Item _item;

   public void OnPointerClick(PointerEventData eventData)
   {
      ItemController.HealPlayer(_item);
      Inventory.instanse.RemoveItem(_item);
   }
}
