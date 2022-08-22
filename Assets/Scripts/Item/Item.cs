using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Create")]
public class Item : ScriptableObject
{
    public string Name;
    public Sprite Icon;
    public AddingObject Type;
    public DiaItem Dia;
    public int Amount ;

    public enum AddingObject
    {
        Adding,
        NOAdding
    }

    public enum DiaItem
    {
        Heal,
        Speed,
        Nostihng
    }
    
}
