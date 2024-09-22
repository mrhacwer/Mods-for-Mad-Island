using MelonLoader;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace CraftRequirements //Namespace of your project
{
    public class CraftRequirementsMOD : MelonMod
    {
        [HarmonyPatch(typeof(UIManager), "ItemDescOpen")]
        class PatchItemDesc : HarmonyPatch
        {
            public static void Postfix(int slotID)
            {
                ManagersScript _MS = GameObject.Find("Managers").GetComponent<ManagersScript>();
                bool flag = slotID >= 100 && slotID < 1000;
                bool flag2 = slotID >= 1000;
                CraftInfo.Required[] array;
                if (flag)
                {
                    array = _MS.craftMN.craftData[_MS.inventory.craftPanelID].craftInfo[slotID - 100].required;
                    if (_MS.inventory.itemSlot[slotID].GetComponent<Button>().interactable)
                    {
                        return;
                    }
                }
                else
                {
                    if (!flag2)
                    {
                        return;
                    }
                    array = _MS.shop.shops[_MS.shop.activeShopID].shopItems[slotID - 1000].GetComponent<CraftInfo>().shopTrade;
                }
                CraftRequirements craftRequirements = new CraftRequirements();
                for (int i = 0; i < array.Length; i++)
                {
                    CraftRequirements.Requirement req = new CraftRequirements.Requirement
                    {
                        ItemKey = array[i].itemData.name,
                        Required = ((array[i].count < 1f) ? 1 : ((int)array[i].count))
                    };
                    craftRequirements.AddRequirement(req);
                }
                craftRequirements.CheckRequirements();
                if (!craftRequirements.MissingRequirement)
                {
                    return;
                }
                List<CraftRequirements.Requirement> requirementList = craftRequirements.GetRequirementList();
                for (int j = 0; j < requirementList.Count; j++)
                {
                    CraftRequirements.Requirement requirement = requirementList[j];
                    ItemData itemData = _MS.itemMN.FindItem(requirement.ItemKey);
                    ItemSlot itemSlot = _MS.inventory.needSlot[j];
                    string text = itemSlot.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = itemData.GetItemName();
                    if (requirement.Current < requirement.Required)
                    {
                        itemSlot.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "<color=\"red\">" + text + "</color>";
                    }
                    else
                    {
                        itemSlot.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = text;
                    }
                }
            }
        }
    }
}