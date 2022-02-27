using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonItemMenuController : MenuButtonController
{
    /*
    [SerializeField] GameObject menuItemPrefab;
 
    public override void OnEnable()
    {
        base.OnEnable();
        Dictionary<Item, int> inventory = new Dictionary<Item, int>();
 
        foreach (Item item in Inventory.instance.items)
        {
            if (inventory.ContainsKey(item))
            {
                inventory[item] += 1;
            }
            else
            {
                inventory.Add(item, 1);
            }
        }
     
 
        maxIndex = inventory.Count - 1;
 
        int index = 0;
        foreach (KeyValuePair<Item, int> entry in inventory)
        {
            if(menuItemPrefab != null)
            {
                GameObject gameObject = Instantiate(menuItemPrefab, Vector3.zero, Quaternion.identity, this.transform) as GameObject;
                gameObject.GetComponent<MenuButton>().menuButtonController = this;
                gameObject.GetComponent<MenuButton>().thisIndex = index;
                gameObject.GetComponent<MenuButton>().animator = gameObject.GetComponent<Animator>();
                gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = entry.Key.name; //GET CHILD IS RESPECTIVE TO THE PARENT EMPTY OBJECT STARTING AT 0
                gameObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = entry.Value.ToString();
                index++;
            }
        }
    }
    */
}
