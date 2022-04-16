using System.Collections;
using System.Collections.Generic;
public class ItemDatabase
{
    public static List<Item> ITEMS = new List<Item>();
    public static readonly Item HEALTH_POTION = Register(new HealingItem("health_potion", new Item.ItemProperty()
        .WithDisplayName("Health Potion")
        .WithDescription("Heal unit by 10.")
        .WithMaxCoolDown(3),
        10));
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
    .WithDescription("Heals allied units in 5 radius by 10.")
    .WithMaxCoolDown(10),
    10, 5));

    public static Item Register(Item item)
    {
        item.SetID(ITEMS.Count);
        ITEMS.Add(item);
        return item;
    }
}

