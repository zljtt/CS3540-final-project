using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string registryName;
    private int id;
    private ItemProperty property;

    public Item(string registryName, ItemProperty property)
    {
        this.registryName = registryName;
        this.property = property;
    }

    virtual public bool OnUse(Transform user, RaycastHit targetHit, ItemStack stack, Inventory inventory)
    {
        return false;
    }

    public string GetRegistryName()
    {
        return registryName;
    }

    public ItemProperty GetProperties()
    {
        return property;
    }

    public Sprite GetSprite()
    {
        return Resources.Load<Sprite>("Sprites/Items/" + registryName);
    }

    public int GetID()
    {
        return id;
    }

    public void SetID(int i)
    {
        id = i;
    }



    public class ItemProperty
    {
        private string displayName;

        private string description;
        private float maxCooldown;

        public ItemProperty()
        {
            displayName = "NAME";
            description = "DESCRIPTION";
            maxCooldown = 0;
        }
        public ItemProperty WithDisplayName(string value)
        {
            displayName = value;
            return this;
        }

        public ItemProperty WithDescription(string value)
        {
            description = value;
            return this;
        }

        public ItemProperty WithMaxCoolDown(float value)
        {
            maxCooldown = value;
            return this;
        }

        public string GetName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }
        public float GetMaxCoolDown()
        {
            return maxCooldown;
        }
    }
}
