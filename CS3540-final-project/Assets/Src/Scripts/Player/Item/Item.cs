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

    virtual public bool OnUse(Transform user, RaycastHit targetHit, int index)
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

    public override bool Equals(object obj)
    {
        return obj is Item && GetRegistryName() == ((Item)obj).GetRegistryName() && GetID() == ((Item)obj).GetID();
    }

    public class ItemProperty
    {
        private string displayName;

        private string description;
        private int maxStack;
        private float maxCooldown;

        public ItemProperty()
        {
            displayName = "NAME";
            description = "DESCRIPTION";
            maxStack = 99;
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

        public ItemProperty WithMaxStack(int value)
        {
            maxStack = value;
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

        public int GetMaxStack()
        {
            return maxStack;
        }

        public float GetMaxCooldown()
        {
            return maxCooldown;
        }
    }

    public override int GetHashCode()
    {
        return registryName.GetHashCode() + id.GetHashCode();
    }
    public static bool operator ==(Item a, Item b)
    {
        return a.GetRegistryName() == b.GetRegistryName() && a.GetID() == b.GetID();
    }

    public static bool operator !=(Item a, Item b)
    {
        return a.GetRegistryName() != b.GetRegistryName() || a.GetID() != b.GetID();
    }
}
