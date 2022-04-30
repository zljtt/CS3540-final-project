using System.Collections;
using System.Collections.Generic;

public class StoreEntries
{

    public static List<StoreEntry> ENTRIES = new List<StoreEntry>();
    public static readonly StoreEntry DRAKE_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.DRAKE_SPAWNER, 1), 12));
    public static readonly StoreEntry DRAKE_6 = Register(new StoreEntry(new ItemStack(ItemDatabase.DRAKE_SPAWNER, 6), 70));
    public static readonly StoreEntry WARRIOR_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_WARRIOR_SPAWNER, 1), 8));
    public static readonly StoreEntry WARRIOR_3 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_WARRIOR_SPAWNER, 3), 23));
    public static readonly StoreEntry WARRIOR_5 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_WARRIOR_SPAWNER, 10), 72));
    public static readonly StoreEntry MAGE_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_MAGE_SPAWNER, 1), 8));
    public static readonly StoreEntry MAGE_2 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_MAGE_SPAWNER, 2), 15));
    public static readonly StoreEntry MAGE_3 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_MAGE_SPAWNER, 3), 23));
    public static readonly StoreEntry ARCHER_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_ARCHER_SPAWNER, 1), 10));
    public static readonly StoreEntry ARCHER_3 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_ARCHER_SPAWNER, 3), 28));

    public static readonly StoreEntry FIGHTER_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_FIGHTER_SPAWNER, 1), 15));
    public static readonly StoreEntry FIGHTER_2 = Register(new StoreEntry(new ItemStack(ItemDatabase.ORC_FIGHTER_SPAWNER, 2), 30));

    public static readonly StoreEntry HEALTH_POTION_1 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 1), 2));
    public static readonly StoreEntry HEALTH_POTION_3 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 4), 7));
    public static readonly StoreEntry HEALTH_POTION_10 = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_POTION, 10), 16));

    public static readonly StoreEntry MASS_HEALING_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.MASS_HEAL_ABILITY, 1), 40));
    public static readonly StoreEntry TAUNT_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.TAUNT_SPELL, 1), 50));
    public static readonly StoreEntry ENCOURAGE_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.ENCOURAGE_SPELL, 1), 45));
    public static readonly StoreEntry RAGE_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.RAGE_SPELL, 1), 30));
    public static readonly StoreEntry WINDGRACE_SPELL = Register(new StoreEntry(new ItemStack(ItemDatabase.WINDGRACE_SPELL, 1), 15));

    public static readonly StoreEntry HEALTH_RITUAL = Register(new StoreEntry(new ItemStack(ItemDatabase.HEALTH_RITUAL, 1), 50));
    public static readonly StoreEntry WOLF_RITUAL = Register(new StoreEntry(new ItemStack(ItemDatabase.WOLF_RITUAL, 1), 45));


    public static StoreEntry Register(StoreEntry entry)
    {
        ENTRIES.Add(entry);
        return entry;
    }


}