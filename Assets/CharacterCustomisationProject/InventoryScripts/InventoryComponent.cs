using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryComponent : MonoBehaviour
{
    [System.Serializable]
    public class ItemStack
    {
        public Item item;
        public int quantity;
        public bool isStackable;
        public int maxStack;

        public ItemStack(Item _item, int _q, bool _isStackable, int _maxStack)
        {
            item = _item;
            quantity = _q;
            isStackable = _isStackable;
            maxStack = _maxStack;

        }

        public ItemStack(Item _item)
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
    bool displayingAllItems = false;
    bool displayingEquipmentsOnly = false;
    bool displayingOtherItemsOnly = false;

    [SerializeField] List<Listener> allListeners = new List<Listener>();
    public List<Item> allItemList = new List<Item>();
    Dictionary<string, Item> itemDictionary = new Dictionary<string, Item>();
    
    public List<ItemStack> inventory = new List<ItemStack>();

    [SerializeField] Sprite defaultEmptyImage;

    [SerializeField] GameObject headParent;
    [SerializeField] Button headEquipmentButton;

    [SerializeField] GameObject rightHandParent;
    [SerializeField] Button rightHandButton;

    [SerializeField] GameObject leftHandParent;
    [SerializeField] Button leftHandButton;

    [SerializeField] GameObject backPackParent;
    [SerializeField] Button backPackButton;

    [SerializeField] GameObject itemDropPrefab;
    [SerializeField] GameObject itemDisplayPrefab;

    [SerializeField] GameObject scrollViewParent;
    [SerializeField] TextMeshProUGUI descriptionText;

    private void Start()
    {
        foreach(Item i in allItemList)
        {
            itemDictionary.Add(i.name, i);
        }
    }
    public void AddItem(string itemName)
    {
        Item itemData = itemDictionary[itemName];
        CheckListener(itemData);
        List<ItemStack> itemStackInInventory = IsItemInInventory(itemName);
        Debug.Log("stack Count" + itemStackInInventory.Count);
        if (itemStackInInventory.Count != 0)
        {
            for (int i = 0; i < itemStackInInventory.Count; i++)
            {
                if (i != itemStackInInventory.Count - 1)
                {
                    ItemStack itemStack = itemStackInInventory[i];
                    if (itemStack.quantity < itemStack.maxStack)
                    {
                        itemStack.quantity++;
                        break;
                    }
                }
                else
                {
                    ItemStack itemStack = itemStackInInventory[i];
                    if (itemStack.quantity < itemStack.maxStack)
                    {
                        itemStack.quantity++;
                        break;
                    }
                    else
                    {
                        inventory.Add(new ItemStack(itemData, 1, itemData.isStackable, itemData.maxNumberPerStack));
                    }
                }
            }

        }
        else
        {
            inventory.Add(new ItemStack(itemData, 1, itemData.isStackable, itemData.maxNumberPerStack));
        }
        Debug.Log("InventoryCount: " + inventory.Count);
       

        EmptyDisplay();
        if (displayingAllItems) DisplayAllItem();
        else if (displayingEquipmentsOnly) DisplayEquipmentsOnly();
        else if (displayingOtherItemsOnly) DisplayOtherItemsOnly();
    }
    public void PickUpItem(GameObject _g)
    {
        // Name is going to be in the form of "Helmet_Drop"
        string itemName = _g.name.Substring(0, _g.name.IndexOf('_'));
        AddItem(itemName);
        Destroy(_g);
       
    }

    public void RemoveItem(ItemStack _itemStack)
    {
        _itemStack.quantity--;
        if (_itemStack.quantity <= 0)
        {
            int atIndex = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == _itemStack)
                {
                    atIndex = i;
                }
            }
            inventory.RemoveAt(atIndex);
        }
    }
    public void DropItem(ItemStack _itemStack)
    {
        RemoveItem(_itemStack);
        TestInstantiateItem(_itemStack.item);
        EmptyDisplay();
        if (displayingAllItems) DisplayAllItem();
        else if (displayingEquipmentsOnly) DisplayEquipmentsOnly();
        else if (displayingOtherItemsOnly) DisplayOtherItemsOnly();
    }

    public void EquipItem(ItemStack i)
    {
        Equipment e = (Equipment)i.item;
        switch(i.item.category)
        {
            case "HeadGear":
                if(headParent.transform.childCount == 0)
                {
                    Instantiate(e.prefab, headParent.transform);
                    foreach(Image img in headEquipmentButton.gameObject.GetComponentsInChildren<Image>())
                    {
                        if(img.name.Contains("ItemImage"))
                        {
                            img.sprite = i.item.itemImage;
                            img.color = Color.white;
                        }
                    }
                    foreach(TextMeshProUGUI txt in headEquipmentButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        if (txt.name.Contains("ItemName"))
                        {
                            txt.text = i.item.name;
                        }
                    }
                    headEquipmentButton.onClick.AddListener(delegate { UnequipItem("HeadGear", i.item); });
                    RemoveItem(i);
                }
                break;
            case "BackGear":
                
                if(backPackParent.transform.childCount == 0)
                {
                    Instantiate(e.prefab, backPackParent.transform);
                    foreach (Image img in  backPackButton.gameObject.GetComponentsInChildren<Image>())
                    {
                        if (img.name.Contains("ItemImage"))
                        {
                            img.sprite = i.item.itemImage;
                            img.color = Color.white;
                        }
                    }
                    foreach (TextMeshProUGUI txt in backPackButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        if (txt.name.Contains("ItemName"))
                        {
                            txt.text = i.item.name;
                        }
                    }
                    backPackButton.onClick.AddListener(delegate { UnequipItem("BackGear", i.item); });
                    RemoveItem(i);
                }
                
                break;
            case "HandGear":
                if (rightHandParent.transform.childCount != 0 && leftHandParent.transform.childCount != 0)
                {
                    break;
                }
                else if(rightHandParent.transform.childCount < 1)
                {
                    Instantiate(e.prefab, rightHandParent.transform);
                    foreach (Image img in rightHandButton.gameObject.GetComponentsInChildren<Image>())
                    {
                        if (img.name.Contains("ItemImage"))
                        {
                            img.sprite = i.item.itemImage;
                            img.color = Color.white;
                        }
                    }
                    foreach (TextMeshProUGUI txt in rightHandButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        if (txt.name.Contains("ItemName"))
                        {
                            txt.text = i.item.name;
                        }
                    }
                    leftHandButton.onClick.AddListener(delegate { UnequipItem("LeftHandGear", i.item); });
                    RemoveItem(i);
                }
                else if(leftHandParent.transform.childCount < 1)
                {
                    GameObject tmp = Instantiate(e.prefab, leftHandParent.transform);
                    //tmp.transform.localRotation = new Quaternion(-15.719f, -162.341f, -270.797f, tmp.transform.localRotation.w);
                    //tmp.transform.position = new Vector3(-0.499f, -0.324f, -0.017f);
                    foreach (Image img in leftHandButton.gameObject.GetComponentsInChildren<Image>())
                    {
                        if (img.name.Contains("ItemImage"))
                        {
                            img.sprite = i.item.itemImage;
                            img.color = Color.white;
                        }
                    }
                    foreach (TextMeshProUGUI txt in leftHandButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                    {
                        if (txt.name.Contains("ItemName"))
                        {
                            txt.text = i.item.name;
                        }
                    }
                    rightHandButton.onClick.AddListener(delegate { UnequipItem("RightHandGear", i.item); });
                    RemoveItem(i);
                }
                break;

        }
        EmptyDisplay();
        if (displayingAllItems) DisplayAllItem();
        else if (displayingEquipmentsOnly) DisplayEquipmentsOnly();
        else if (displayingOtherItemsOnly) DisplayOtherItemsOnly();
    } 
    
    public void UnequipItem(string _itemPos, Item _item)
    {
        switch(_itemPos)
        {
            case "HeadGear":
                Destroy(headParent.transform.GetChild(0).gameObject);
                foreach (Image img in headEquipmentButton.gameObject.GetComponentsInChildren<Image>())
                {
                    if (img.name.Contains("ItemImage"))
                    {
                        img.sprite = defaultEmptyImage;
                        img.color = Color.red;
                    }
                }
                foreach (TextMeshProUGUI txt in headEquipmentButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (txt.name.Contains("ItemName"))
                    {
                        txt.text = "Empty";
                    }
                }
                headEquipmentButton.onClick.RemoveAllListeners();
                break;
            case "BackGear":
                Destroy(backPackParent.transform.GetChild(0).gameObject);
                foreach (Image img in backPackButton.gameObject.GetComponentsInChildren<Image>())
                {
                    if (img.name.Contains("ItemImage"))
                    {
                        img.sprite = defaultEmptyImage;
                        img.color = Color.red;
                    }
                }
                foreach (TextMeshProUGUI txt in backPackButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (txt.name.Contains("ItemName"))
                    {
                        txt.text = "Empty";
                    }
                }
                backPackButton.onClick.RemoveAllListeners();
                break;
            case "LeftHandGear":
                Destroy(leftHandParent.transform.GetChild(0).gameObject);

                foreach (Image img in leftHandButton.gameObject.GetComponentsInChildren<Image>())
                {
                    if (img.name.Contains("ItemImage"))
                    {
                        img.sprite = defaultEmptyImage;
                        img.color = Color.red;
                    }
                }
                foreach (TextMeshProUGUI txt in leftHandButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (txt.name.Contains("ItemName"))
                    {
                        txt.text = "Empty";
                    }
                }
                leftHandButton.onClick.RemoveAllListeners();
                break;
            case "RightHandGear":
                Destroy(rightHandParent.transform.GetChild(0).gameObject);
                foreach (Image img in rightHandButton.gameObject.GetComponentsInChildren<Image>())
                {
                    if (img.name.Contains("ItemImage"))
                    {
                        img.sprite = defaultEmptyImage;
                        img.color = Color.red;  
                    }
                }
                foreach (TextMeshProUGUI txt in rightHandButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    if (txt.name.Contains("ItemName"))
                    {
                        txt.text = "Empty";
                    }
                }
                rightHandButton.onClick.RemoveAllListeners();
                break;
        }
        Item itemData = itemDictionary[_item.name];
        inventory.Add(new ItemStack(itemData, 1, itemData.isStackable, itemData.maxNumberPerStack));

        EmptyDisplay();
        if (displayingAllItems) DisplayAllItem();
        else if (displayingEquipmentsOnly) DisplayEquipmentsOnly();
        else if (displayingOtherItemsOnly) DisplayOtherItemsOnly();
    }

    public void TestInstantiateItem(Item _item)
    {
        GameObject itemDrop = Instantiate(itemDropPrefab, new Vector3(this.transform.position.x + 1, this.transform.position.y + 1, this.transform.position.z), new Quaternion());
        itemDrop.GetComponentInChildren<SpriteRenderer>().sprite = _item.itemImage;
        itemDrop.name = _item.name + "_Drop";
        itemDrop.layer = 6;
    }

    public void DisplayAllItem()
    {
        displayingAllItems = true;
        displayingEquipmentsOnly = false;
        displayingOtherItemsOnly = false;

        EmptyDisplay();
        foreach(ItemStack i in inventory)
        {
            GameObject itemDisplay = Instantiate(itemDisplayPrefab, scrollViewParent.transform);
            Image[] allImageObject = itemDisplay.GetComponentsInChildren<Image>();

            itemDisplay.GetComponent<Button>().onClick.AddListener(delegate { DisplayDescription(i.item); });

            // Sets the image of the item display to the item sprite
            foreach (Image image in allImageObject)
            {
                if(image.name.Contains("Image"))
                {
                    image.sprite = i.item.itemImage;
                }
            }

            TextMeshProUGUI[] allTextObject = itemDisplay.GetComponentsInChildren<TextMeshProUGUI>();
            foreach(TextMeshProUGUI text in allTextObject)
            {
                if(text.name.Contains("ItemName"))
                {
                    text.text = i.item.name;
                }
                if(text.name.Contains("ItemQuantity"))
                {
                    text.text = i.quantity.ToString();
                }
            }

            // TODO: Delegate On Click to both equip and drop buttons here
            Button[] allButtonObject = itemDisplay.GetComponentsInChildren<Button>();
            foreach(Button b in allButtonObject)
            {
                if(b.name.Contains("DropButton"))
                {
                    b.onClick.AddListener(delegate { DropItem(i); });
                }
                if(b.name.Contains("EquipButton"))
                {
                    if (!i.item.isEquippable)
                    {
                        b.gameObject.SetActive(false);
                    }
                    else
                    {
                        b.onClick.AddListener(delegate { EquipItem(i); });
                    }
                }
            }
            DisplayEquippedItems();
        }
    }

    public void DisplayEquipmentsOnly()
    {
        displayingAllItems = false;
        displayingEquipmentsOnly = true;
        displayingOtherItemsOnly = false;
        EmptyDisplay();
        foreach (ItemStack i in inventory)
        {
            if(i.item.isEquippable)
            {

                GameObject itemDisplay = Instantiate(itemDisplayPrefab, scrollViewParent.transform);
                Image[] allImageObject = itemDisplay.GetComponentsInChildren<Image>();
                itemDisplay.GetComponent<Button>().onClick.AddListener(delegate { DisplayDescription(i.item); });

                // Sets the image of the item display to the item sprite
                foreach (Image image in allImageObject)
                {
                    if (image.name.Contains("Image"))
                    {
                        image.sprite = i.item.itemImage;
                    }
                }

                TextMeshProUGUI[] allTextObject = itemDisplay.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (TextMeshProUGUI text in allTextObject)
                {
                    if (text.name.Contains("ItemName"))
                    {
                        text.text = i.item.name;
                    }
                    if (text.name.Contains("ItemQuantity"))
                    {
                        text.text = i.quantity.ToString();
                    }
                }

                // TODO: Delegate On Click to both equip and drop buttons here
                Button[] allButtonObject = itemDisplay.GetComponentsInChildren<Button>();
                foreach (Button b in allButtonObject)
                {
                    if (b.name.Contains("DropButton"))
                    {
                        b.onClick.AddListener(delegate { DropItem(i); });
                    }
                    if (b.name.Contains("EquipButton"))
                    {
                        if (!i.item.isEquippable)
                        {
                            b.gameObject.SetActive(false);
                        }
                        else
                        {
                            b.onClick.AddListener(delegate { EquipItem(i); });
                        }
                    }
                }
                DisplayEquippedItems();
            }
        }
    }

    public void DisplayOtherItemsOnly()
    {
        displayingAllItems = false;
        displayingEquipmentsOnly = false;
        displayingOtherItemsOnly = true;
        EmptyDisplay();
        foreach (ItemStack i in inventory)
        {
            if (!i.item.isEquippable)
            {

                GameObject itemDisplay = Instantiate(itemDisplayPrefab, scrollViewParent.transform);
                Image[] allImageObject = itemDisplay.GetComponentsInChildren<Image>();
                itemDisplay.GetComponent<Button>().onClick.AddListener(delegate { DisplayDescription(i.item); });

                // Sets the image of the item display to the item sprite
                foreach (Image image in allImageObject)
                {
                    if (image.name.Contains("Image"))
                    {
                        image.sprite = i.item.itemImage;
                    }
                }

                TextMeshProUGUI[] allTextObject = itemDisplay.GetComponentsInChildren<TextMeshProUGUI>();
                foreach (TextMeshProUGUI text in allTextObject)
                {
                    if (text.name.Contains("ItemName"))
                    {
                        text.text = i.item.name;
                    }
                    if (text.name.Contains("ItemQuantity"))
                    {
                        text.text = i.quantity.ToString();
                    }
                }

                // TODO: Delegate On Click to both equip and drop buttons here
                Button[] allButtonObject = itemDisplay.GetComponentsInChildren<Button>();
                foreach (Button b in allButtonObject)
                {
                    if (b.name.Contains("DropButton"))
                    {
                        b.onClick.AddListener(delegate { DropItem(i); });
                    }
                    if (b.name.Contains("EquipButton"))
                    {
                        if (!i.item.isEquippable)
                        {
                            b.gameObject.SetActive(false);
                        }
                        else
                        {
                            b.onClick.AddListener(delegate { EquipItem(i); });
                        }
                    }
                }
                DisplayEquippedItems();
            }
        }
    }

    private List<ItemStack> IsItemInInventory(string itemName) 
    {
        List<ItemStack> tmp = new List<ItemStack>();
        foreach(ItemStack i in inventory)
        {
            if(i.item.name == itemName)
            {
                tmp.Add(i);
            }
        }

        return tmp;
    }
    public void EmptyDisplay()
    {
        Transform[] allDisplay = scrollViewParent.GetComponentsInChildren<Transform>();
        foreach(Transform t in allDisplay)
        {
            if(!t.name.Contains("Content"))
            {
                Destroy(t.gameObject);
            }
        }
    }

    public void DisplayEquippedItems()
    {
        if (headParent.transform.childCount == 0)
        {
            foreach (Image img in headEquipmentButton.gameObject.GetComponentsInChildren<Image>())
            {
                if (img.name.Contains("ItemImage"))
                {
                    img.sprite = defaultEmptyImage;
                }
            }
            foreach (TextMeshProUGUI txt in headEquipmentButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (txt.name.Contains("ItemName"))
                {
                    txt.text = "Empty";
                }
            }
        }
        if (backPackParent.transform.childCount == 0)
        {
            foreach (Image img in backPackButton.gameObject.GetComponentsInChildren<Image>())
            {
                if (img.name.Contains("ItemImage"))
                {
                    img.sprite = defaultEmptyImage;
                }
            }
            foreach (TextMeshProUGUI txt in backPackButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (txt.name.Contains("ItemName"))
                {
                    txt.text = "Empty";
                }
            }
        }
        if(leftHandParent.transform.childCount == 0)
        {
            foreach (Image img in leftHandButton.gameObject.GetComponentsInChildren<Image>())
            {
                if (img.name.Contains("ItemImage"))
                {
                    img.sprite = defaultEmptyImage;
                }
            }
            foreach (TextMeshProUGUI txt in leftHandButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (txt.name.Contains("ItemName"))
                {
                    txt.text = "Empty";
                }
            }
        }
        if(rightHandParent.transform.childCount == 0)
        {
            foreach (Image img in rightHandButton.gameObject.GetComponentsInChildren<Image>())
            {
                if (img.name.Contains("ItemImage"))
                {
                    img.sprite = defaultEmptyImage;
                }
            }
            foreach (TextMeshProUGUI txt in rightHandButton.gameObject.GetComponentsInChildren<TextMeshProUGUI>())
            {
                if (txt.name.Contains("ItemName"))
                {
                    txt.text = "Empty";
                }
            }
        }
    }

    public void DisplayDescription(Item i)
    {
        descriptionText.text = i.description;
    }

    public void AddListener (Listener _l)
    {
        allListeners.Add(_l);
    }

    public void CheckListener(Item _i)
    {
        foreach(Listener l in allListeners)
        {
            l.CheckListener(_i);
        }
    }
}
