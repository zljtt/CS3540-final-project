using System.Collections;
using System.Collections.Generic;

public class StoreEntries
{

    public static List<StoreEntry> ENTRIES = new List<StoreEntry>();
    public static readonly StoreEntry DRAKE = Register(new StoreEntry(new ItemStack(ItemDatabase.DRAKE_SPAWNER, 1), 10));
    public static readonly StoreEntry WARRIOR = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_WARRIOR_SPAWNER, 1), 5));
    public static readonly StoreEntry MAGE = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_MAGE_SPAWNER, 1), 10));
    public static readonly StoreEntry HEALTH_POTION_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 1), 4));
    public static readonly StoreEntry HEALTH_POTION_3 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 3), 10));
    public static readonly StoreEntry HEALTH_POTION_10 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 10), 25));

    public static readonly StoreEntry ABILITY_MASS_HEAL = Register(new StoreEntry(new ItemStack(ItemDatabase.MASS_HEAL_ABILITY, 1), 40));

    public static StoreEntry Register(StoreEntry entry)
    {
        ENTRIES.Add(entry);
        return entry;
    }


}