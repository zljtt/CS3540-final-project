using System.Collections;
using System.Collections.Generic;
public class ItemDatabase
{
    public static List<Item> ITEMS = new List<Item>();
    public static readonly Item EMPTY = Register(new Item("empty", new Item.ItemProperty()));
    public static readonly Item HEALTH_POTION = Register(new HealingItem("health_potion", new Item.ItemProperty()
        .WithDisplayName("Health Potion")
        .WithDescription("Heal target ally unit by 20% max health over 4 seconds.")
        .WithMaxCoolDown(3),
        20));
    public static readonly Item ORC_WARRIOR_SPAWNER = Register(new SpawnerItem("orc_warrior_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Warrior")
        .WithDescription("Spawns a Orc Warrior to help you fight."),
        "Prefabs/Character/OrcWarrior"));
    public static readonly Item ORC_MAGE_SPAWNER = Register(new SpawnerItem("orc_mage_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Mage")
        .WithDescription("Spawns a Orc Mage to help you fight."),
        "Prefabs/Character/OrcMage"));
    public static readonly Item DRAKE_SPAWNER = Register(new SpawnerItem("drake_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Warrior")
        .WithDescription("Spawns a Drake to help you fight."),
        "Prefabs/Character/Drake"));
    public static readonly Item MASS_HEAL_ABILITY = Register(new AOEHealingAbility("mass_heal", new Item.ItemProperty()
        .WithDisplayName("Mass Heal")
        .WithDescription("Heal all ally units in 5 radius by 10% max health over 2 seconds.")
        .WithMaxStack(1)
        .WithMaxCoolDown(10),
        10, 5));

    public static Item Register(Item item)
    {
        item.SetID(ITEMS.Count);
        ITEMS.Add(item);
        return item;
    }

    public static Item GetFromRegistryName(string name)
    {
        foreach (Item item in ITEMS)
        {
            if (item.GetRegistryName() == name)
            {
                return item;
            }
        }
        return EMPTY;
    }
}

