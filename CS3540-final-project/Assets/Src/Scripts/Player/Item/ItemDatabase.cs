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
        .WithDescription("Spell (20s cooldown) : Rage target ally unit, increase its attack damage and speed by 50% but losing health over 5 seconds.")
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
    public static readonly Item WINDGRACE_SPELL = Register(new TauntSkill("windgrace_spell", new Item.ItemProperty()
        .WithDisplayName("Windgrace")
        .WithDescription("Spell (10s cooldown) : Largely increase target ally unit's alert range and movement speed.")
        .WithMaxStack(1)
        .WithMaxCoolDown(10)));

    // spawners
    public static readonly Item ORC_WARRIOR_SPAWNER = Register(new SpawnerItem("orc_warrior_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Warrior")
        .WithDescription("Consumable : Spawns a Orc Warrior, a melee unit with fast attack speed. Can only use during preparation."),
        "Prefabs/Character/OrcWarrior"));
    public static readonly Item ORC_MAGE_SPAWNER = Register(new SpawnerItem("orc_mage_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Mage")
        .WithDescription("Consumable : Spawns Orc Mage, a ranged unit that can attack behind melee units. Can only use during preparation."),
        "Prefabs/Character/OrcMage"));
    public static readonly Item DRAKE_SPAWNER = Register(new SpawnerItem("drake_spawner", new Item.ItemProperty()
        .WithDisplayName("Orc Warrior")
        .WithDescription("Consumable : Spawns a Drake, a ranged flying unit with low health but high attack. Can only use during preparation."),
        "Prefabs/Character/Drake"));

    // rituals
    public static readonly Item WOLF_RITUAL = Register(new Item("wolf_ritual", new Item.ItemProperty()
        .WithDisplayName("Wolf Ritual")
        .WithDescription("Passive : Ally units' attack damage increases when there is no other ally units nearby.")));
    public static readonly Item HEALTH_RITUAL = Register(new Item("max_health_ritual", new Item.ItemProperty()
        .WithDisplayName("Health Ritual")
        .WithDescription("Passive : Ally units's max health is increased by 20%.")));
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

