using System.Collections.Generic;
using UnityEngine;

namespace CraftRequirements
{
    public class CraftRequirements
    {
        private ManagersScript _MS = GameObject.Find("Managers").GetComponent<ManagersScript>();
        private Dictionary<string, CraftRequirements.Requirement> RequirementDict = new Dictionary<string, CraftRequirements.Requirement>();
        private List<CraftRequirements.Requirement> Requirements = new List<CraftRequirements.Requirement>();
        public class Requirement
        {
            public string ItemKey;
            public int Required;
            public int Current;
        }
        public bool MissingRequirement
        {
            get
            {
                foreach (CraftRequirements.Requirement type in Requirements)
                {
                    if (type.Required > type.Current)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        public void AddRequirement(CraftRequirements.Requirement req)
        {
            RequirementDict.Add(req.ItemKey, req);
            Requirements.Add(req);
        }
        public List<CraftRequirements.Requirement> GetRequirementList()
        {
            return this.Requirements;
        }
        private void CheckInventory(ItemSlot[] inventory, int offset, int size)
        {
            for (int i = 0; i < size; i++)
            {
                ItemSlot itemSlot = inventory[i + offset];
                CraftRequirements.Requirement type;
                if (itemSlot.itemKey != null && RequirementDict.TryGetValue(itemSlot.itemKey, out type))
                {
                    type.Current += itemSlot.stack;
                }
            }
        }
        public void CheckRequirements()
        {
            InventoryManager inventory = _MS.inventory;
            if (inventory.craftPanelID == 0)
            {
                CheckInventory(inventory.itemSlot, 0, inventory.baseCount);
                return;
            }
            CheckInventory(inventory.itemSlot, 50, inventory.tmpSubInventory.size);
            int num = 0;
            while (MissingRequirement && num < inventory.refInventory.Count)
            {
                CheckInventory(inventory.refInventory[num].slots, 0, inventory.refInventory[num].slots.Length);
                num++;
            }
        }
    }
}

