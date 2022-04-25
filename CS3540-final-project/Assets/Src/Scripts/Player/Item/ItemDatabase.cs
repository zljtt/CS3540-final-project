using System.Collections;
using System.Collections.Generic;
public class ItemDatabase
{
    public static List<Item> ITEMS = new List<Item>();
    public static readonly Item EMPTY = Register(new Item("empty", new Item.ItemProperty()));
    // consumble items
    public static readonly Item HEALTH_POTION = Register(new HealingItem("health_potion", new Item.ItemProperty()
        .WithDisplayName("Healing Potion")
        .WithDescription("Consumable (3s cooldown) : Heal target ally unit by 20% max health over 4 seconds.")
        .WithMaxCoolDown(3),
        20));

    // skills
    public static readonly Item MASS_HEAL_ABILITY = Register(new AOEHealingAbility("mass_heal", new Item.ItemProperty()
        .WithDisplayName("Mass Heal")
        .WithDescription("Skill (10s cooldown) : Heal all ally units in 5 radius by 10% max health over 2 seconds.")
        .WithMaxStack(1)
        .WithMaxCoolDown(10),
        10, 5));
    public static readonly Item RAGE_SPELL = Register(new SingleAllyTargetItem("rage_spell", new Item.ItemProperty()
        .WithDisplayName("Rage")
        .WithDescription("Spell (20s cooldown) : Rage target ally unit, increase its attack damage and speed by 25% but losing health over 5 seconds.")
        .WithMaxStack(1)
        .WithMaxCoolDown(20),
        EffectType.RAGE, 5, false));
    public static readonly Item ENCOURAGE_SPELL = Register(new SingleAllyTargetItem("encourage_spell", new Item.ItemProperty()
        .WithDisplayName("Encourage")
        .WithDescription("Spell (30s cooldown) : Encourage target ally unit, increase its attack damage and max health by 20% for 20 seconds.")
        .WithMaxStack(1)
        .WithMaxCoolDown(30),
        EffectType.ENCOURAGE, 20, false));

    public static readonly Item TAUNT_SPELL = Register(new TauntSkill("taunt_spell", new Item.ItemProperty()
        .WithDisplayName("Taunt")
        .WithDescription("Spell (25s cooldown) : Target ally unit taunts enemies, forcing all nearby enemy units to attack it.")
        .WithMaxStack(1)
        .WithMaxCoolDown(25)));

    // spawners
    public static readonly Item ORC_WARRIOR_SPAWNER = Register(new SpawnerItem("orc_warrior_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Warrior")
        .WithDescription("Consumable (only during preparation) : Spawns a Orc Warrior, a melee unit with fast attack speed."),
        "Prefabs/Character/OrcWarrior"));
    public static readonly Item ORC_MAGE_SPAWNER = Register(new SpawnerItem("orc_mage_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Mage")
        .WithDescription("Consumable (only during preparation) : Spawns Orc Mage, a ranged unit that can attack behind melee units."),
        "Prefabs/Character/OrcMage"));
    public static readonly Item DRAKE_SPAWNER = Register(new SpawnerItem("drake_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Warrior")
        .WithDescription("Consumable (only during preparation) : Spawns a Drake, a ranged flying unit with low health but high attack."),
        "Prefabs/Character/Drake"));


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

