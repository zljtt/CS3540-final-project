using System.Collections;
using System.Collections.Generic;
public class ItemDatabase
{
    public static List<Item> ITEMS = new List<Item>();
    public static readonly Item HEALTH_POTION = Register(new HealingItem("health_potion", new Item.ItemProperty()
        .withDisplayName("Health Potion")
        .withDescription("Heal unit by 10."),
        10));
    public static readonly Item ORC_WARRIOR_SPAWNER = Register(new SpawnerItem("orc_warrior_spawner", new Item.ItemProperty()
        .withDisplayName("Orc Warrior")
        .withDescription("Spawns a Orc Warrior to help you fight."),
        "Prefabs/Character/OrcWarrior"));

    public static Item Register(Item item)
    {
        item.SetID(ITEMS.Count);
        ITEMS.Add(item);
        return item;
    }
}

