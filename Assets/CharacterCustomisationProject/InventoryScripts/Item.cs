using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public string description;
    public string category;
    public bool isEquippable;
    public Sprite itemImage;
    public bool isStackable;
    public int maxNumberPerStack;
}
