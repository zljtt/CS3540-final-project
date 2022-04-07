public class ItemDatabase
{
    public static readonly Item HEALTH_POTION = new HealingItem("health_potion",
        new Item.ItemProperty().withDisplayName("Health Potion").withDescription("Heal unit"), 10);
    public static readonly Item FARMER_SPAWNER = new SpawnerItem("farmer_spawner",
        new Item.ItemProperty().withDisplayName("Farmer Spawner").withDescription("Spawns a farmer"), "Prefabs/Character/Farmer");
}

