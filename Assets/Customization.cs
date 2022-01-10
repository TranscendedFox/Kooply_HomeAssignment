using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

[System.Serializable]
public class ItemSlot
{   
    public Item itemName;
    public int currentItem;
    public GameObject[] models;
}
public enum Item {head, body, weapon}
public class Customization : MonoBehaviour
{
    [Header("Cosmetics")]
    public ItemSlot[] items;
    private Animator animator;    
    [SerializeField] private RuntimeAnimatorController[] animations;
    private int selectedItemSlot;

    [Header("UI")]
    [SerializeField] private Button[] buttons;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color deselectedColor;

    [Header("Effect")]
    [SerializeField] private Transform head;

    private void Start()
    {
        selectedItemSlot = 0;
        animator = GetComponent<Animator>();
        head.GetComponent<MultiParentConstraint>().weight = 0;
        SelectSlotButton(0);
    }
    public void ChangeItemButton(int direction)
    {
        ItemSlot item = items[selectedItemSlot];

        item.models[item.currentItem].SetActive(false);

        //Effect
        if (item.models[item.currentItem].name == "TheHead")
        {
            head.GetComponent<MultiParentConstraint>().weight = 0;
        }

        if (direction == 0)
        {            
            if (item.currentItem + 1 > item.models.Length - 1)
            {
                item.currentItem = 0;
            }
            else
            {
                item.currentItem++;
            }            
        }
        else
        {
            if (item.currentItem - 1 < 0)
            {
                item.currentItem = item.models.Length - 1;
            }
            else
            {
                item.currentItem--;
            }
        }

        item.models[item.currentItem].SetActive(true);

        //Effect
        if (item.models[item.currentItem].name == "TheHead")
        {
            head.GetComponent<MultiParentConstraint>().weight = 1;
        }

        //Change animation relative to weapon
        if (item.itemName == Item.weapon)
        {
            string a = item.models[item.currentItem].name;
            foreach (var anim in animations)
            {
                if (anim.name.Remove(anim.name.Length - 8) == a.Remove(a.Length - 7))
                {
                    animator.runtimeAnimatorController = anim;
                }
            }
            if (items[0].models[items[0].currentItem].name == "TheHead")
            {
                head.GetComponent<MultiParentConstraint>().weight = 1;
            }
        }
    }

    public void SelectSlotButton(int item)
    {
        selectedItemSlot = item;

        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == item)
            {
                buttons[i].GetComponent<Image>().color = selectedColor;
            }
            else
            {
                buttons[i].GetComponent<Image>().color = deselectedColor;
            }                
        }        
    }
}
