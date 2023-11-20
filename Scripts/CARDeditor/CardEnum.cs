using UnityEngine;

public enum CardType
{
    Placed,
    InstantCast
}

public enum CardActionType
{
    RegenHealthPerTurn,
    RegenManaPerTurn,
    AttackPerTurn
}

public enum InstantCastType
{
    RegeneratePlayerHealth,
    AttackAllOpposingCards,
    AttackOpposingPlayer
}

public enum ElementType
{
    None,
    Fire,
    Water,
    Ice,
    Air,
    Lightning,
    Earth,
    Dark,
    Light
}

public enum AttackType
{
    None,
    Normal,
    Direct,
    AllOpponent
}

public enum RegenerationType
{
    None,
    Health,
    Mana
}
