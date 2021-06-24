using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryComponent : MonoBehaviour
{
    class ItemStack
    {
        public Item item;
        public int quantity;
        public bool isStackable;
        public int maxStack;

        ItemStack(Item _item, int _q, bool _isStackable, int _maxStack)
        {
            item = _item;
            quantity = _q;
            isStackable = _isStackable;
            maxStack = _maxStack;

        }

        ItemStack(Item _item)
        {
            item = _item;
            quantity = 1;
            isStackable = false;
            maxStack = 1;
        }

        public void UseItem()
        {
            quantity -= 1;
        }

    }
    List<Item> inventory;
    [SerializeField] GameObject headParent;
    [SerializeField] Image headEquipmentImage;

    [SerializeField] GameObject rightHandParent;
    [SerializeField] Image rightHandImage;

    [SerializeField] GameObject leftHandParent;
    [SerializeField] Image leftHandImage;

    [SerializeField] GameObject backPackParent;
    [SerializeField] GameObject backPackImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
