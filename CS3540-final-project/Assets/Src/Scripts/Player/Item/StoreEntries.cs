using System.Collections;
using System.Collections.Generic;

public class StoreEntries
{

    public static List<StoreEntry> ENTRIES = new List<StoreEntry>();
    public static readonly StoreEntry DRAKE = Register(new StoreEntry(new ItemStack(ItemDatabase.DRAKE_SPAWNER, 1), 12));
    public static readonly StoreEntry WARRIOR_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_WARRIOR_SPAWNER, 1), 7));
    public static readonly StoreEntry WARRIOR_3 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_WARRIOR_SPAWNER, 3), 20));
    public static readonly StoreEntry MAGE_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_MAGE_SPAWNER, 1), 8));
    public static readonly StoreEntry MAGE_2 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_MAGE_SPAWNER, 2), 15));
    public static readonly StoreEntry HEALTH_POTION_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 1), 2));
    public static readonly StoreEntry HEALTH_POTION_3 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 4), 7));
    public static readonly StoreEntry HEALTH_POTION_10 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 10), 16));

    public static readonly StoreEntry MASS_HEALING_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.MASS_HEAL_ABILITY, 1), 40));
    public static readonly StoreEntry TAUNT_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.TAUNT_SPELL, 1), 50));
    public static readonly StoreEntry ENCOURAGE_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.ENCOURAGE_SPELL, 1), 45));
    public static readonly StoreEntry RAGE_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.RAGE_SPELL, 1), 30));

    public static StoreEntry Register(StoreEntry entry)
    {
        ENTRIES.Add(entry);
        return entry;
    }


}