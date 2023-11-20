// CardData.cs

using UnityEngine;

[CreateAssetMenu(fileName = "NewCardData", menuName = "Cards/Card Data")]
public class CardData : ScriptableObject
{
    new public string name; // 'new' keyword to hide the warning
    public ElementType elementType;

    // Common fields
    public int manaPoints;
    public int healthPoints;
    public int damageIncreasePercentage;

    // Placed Card fields
    public CardType cardType;
    public CardActionType cardActionType;
    public int manaPointsRequired;

    // Instant Cast Card fields
    public InstantCastType instantCastType;
    public int instantCastHealthRegen;
    public int instantCastAttackPoints;

    // Additional fields based on card action type
    public int healthRegenPerTurn;
    public bool healAllCards;
    public int manaRegenPerTurn;
    public int attackPoints;
    public bool attackOpponentHealth;
    public bool attackAllOpponentCards;

    // Additional fields for Placed Card Type
    public bool regenHealthPerTurn;
    public bool regenManaPerTurn;
    public bool attackOpposingPlayer; // Updated name to match CardEditorWindow
    public int directDamage;

    // Additional fields for Element Type
    public ElementType opposingElementType;

  
}
