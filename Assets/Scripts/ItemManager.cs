using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//Enum which has the various item types
public enum ItemType {
    Weapon,
    Armor,
    Misc
}
public enum OrderByType {
    Alphabet,
    Level,
    Weight,
    Cost

}
//Each Item is actually just a Structure
[System.Serializable]
public struct Item {
    [SerializeField]
    private string _name;
    public string Name {
        get {
            return _name;
        }
    }
    [SerializeField]
    private int _weight;
    public int Weight {
        get {
            return _weight;
        }
    }
    [SerializeField]
    private ItemType _type;
    public ItemType Type {
        get {
            return _type;
        }
    }
    [SerializeField]
    private int _level;//Level of the object (higher level corresponds to higher dmg/defense, etc.)
    public int Level {
        get {
            return _level;
        }
    }
    [SerializeField]
    private int _cost;
    public int Cost {
        get {
            return _cost;
        }
    }
    [SerializeField]
    private Sprite _image;
    public Sprite Image {
        get {
            return _image;
        }
    }
}

//Class used for each Item in a character's inventory
public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private List<Item> possibleItems;
    [SerializeField]
    private StatManager myStatManager;
    
    void Update()
    {
        
    }
    //This function creates several items for the currentCharacter
    public void CreateItems(int numItems) {
        for(int i = 0; i < numItems; i++) {
            Item tempItem = possibleItems[Random.Range(0, possibleItems.Count)];
            myStatManager.currentCharacter.Inventory.Add(tempItem);
        }
        myStatManager.RefreshStatPage();
    }

    //This function sorts the items owned by the current player
        //Because of my use of multiple Structs, rather than classes, I ended up being unable to find a way to use delegates/events in this system
    public void SortItems(ref List<Item> Items, OrderByType order) {
        if(order == OrderByType.Alphabet) {
            Items = Items.OrderBy(item => item.Name).ToList<Item>();
        }
        else if(order == OrderByType.Level) {
            Items = Items.OrderBy(item => item.Level).ToList<Item>();
        }
        else if(order == OrderByType.Weight) {
            Items = Items.OrderBy(item => item.Weight).ToList<Item>();
        }
        else if(order == OrderByType.Cost) {
            Items = Items.OrderBy(item => item.Cost).ToList<Item>();
        }
    }
}
