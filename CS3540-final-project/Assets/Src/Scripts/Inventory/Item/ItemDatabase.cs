public class ItemDatabase
{
    public static readonly Item HEALTH_POTION = new HealingItem("health_potion",
        new Item.ItemProperty().withDisplayName("Health Potion").withDescription("Heal unit by 10."), 10);
    public static readonly Item ORC_WARRIOR_SPAWNER = new SpawnerItem("orc_warrior_spawner",
        new Item.ItemProperty().withDisplayName("Orc Warrior").withDescription("Spawns a Orc Warrior to help you fight."), "Prefabs/Character/OrcWarrior");
}

