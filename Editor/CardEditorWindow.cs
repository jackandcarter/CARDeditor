// CardEditorWindow.cs

using UnityEditor;
using UnityEngine;

public class CardEditorWindow : EditorWindow
{
    private CardData cardData;
    private bool isTemplateCreated = false;

    private void OnEnable()
    {
        CreateTemplateFolder();
    }

    [MenuItem("Tools/CARDeditor")]
    public static void ShowWindow()
    {
        GetWindow<CardEditorWindow>("CARDeditor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Card Editor", EditorStyles.boldLabel);

        cardData = (CardData)EditorGUILayout.ObjectField(
            new GUIContent("Card Data", "Select or create a Card Data asset."),
            cardData, typeof(CardData), false);

        if (cardData == null)
        {
            EditorGUILayout.HelpBox("Create or select a Card Data asset.", MessageType.Warning);

            // Add a button to create a new card
            if (GUILayout.Button(new GUIContent("Create New Card", "Creates a new Card Data asset.")))
            {
                CreateNewCard();
                isTemplateCreated = false; // Reset the flag when creating a new card
            }
        }
        else
        {
            DrawCardFields();

            // Only show the "Save" button if the template hasn't been created
            if (!isTemplateCreated)
            {
                DrawSaveButton();
            }

            // Show button to create a new card
            if (GUILayout.Button(new GUIContent("Create New Card", "Creates a new Card Data asset."), GUILayout.ExpandWidth(false)))
            {
                CreateNewCard();
                isTemplateCreated = false; // Reset the flag when creating a new card
            }
        }
    }

    private void DrawCardFields()
    {
        cardData.name = EditorGUILayout.TextField(
            new GUIContent("Name", "The name of the card."),
            cardData.name);

        cardData.cardType = (CardType)EditorGUILayout.EnumPopup(
            new GUIContent("Card Type", "The type of the card."),
            cardData.cardType);

        if (cardData.cardType == CardType.Placed)
        {
            // Show sub-options for Placed card type
            cardData.cardActionType = (CardActionType)EditorGUILayout.EnumPopup(
                new GUIContent("Card Action Type", "The action type of the placed card."),
                cardData.cardActionType);

            // Show options based on the card action type
            switch (cardData.cardActionType)
            {
                case CardActionType.RegenHealthPerTurn:
                    DrawRegenHealthPerTurnFields();
                    break;
                case CardActionType.RegenManaPerTurn:
                    DrawRegenManaPerTurnFields();
                    break;
                case CardActionType.AttackPerTurn:
                    DrawAttackPerTurnFields();
                    break;
            }

            cardData.manaPointsRequired = EditorGUILayout.IntField(
                new GUIContent("Mana Points Required", "The mana points required to use the card."),
                cardData.manaPointsRequired);
        }
        else if (cardData.cardType == CardType.InstantCast)
        {
            // Show sub-options for Instant Cast card type
            cardData.instantCastType = (InstantCastType)EditorGUILayout.EnumPopup(
                new GUIContent("Instant Cast Type", "The type of the instant cast."),
                cardData.instantCastType);

            // Show options based on the instant cast type
            switch (cardData.instantCastType)
            {
                case InstantCastType.RegeneratePlayerHealth:
                    DrawRegeneratePlayerHealthFields();
                    break;
                case InstantCastType.AttackAllOpposingCards:
                    DrawAttackAllOpposingCardsFields();
                    break;
                case InstantCastType.AttackOpposingPlayer:
                    DrawAttackOpposingPlayerFields();
                    break;
            }

            cardData.manaPointsRequired = EditorGUILayout.IntField(
                new GUIContent("Mana Points Required", "The mana points required to use the card."),
                cardData.manaPointsRequired);
        }

        EditorGUILayout.Space();
    }

    private void DrawAttackPerTurnFields()
    {
        cardData.attackPoints = EditorGUILayout.IntField(
            new GUIContent("Attack Points", "The attack points of the card."),
            cardData.attackPoints);
        cardData.healthPoints = EditorGUILayout.IntField(
            new GUIContent("Health Points", "The health points of the placed card."),
            cardData.healthPoints);
        cardData.attackOpponentHealth = EditorGUILayout.Toggle(
            new GUIContent("Attack Opponent Health", "If checked, the card will also attack the opponent's health points."),
            cardData.attackOpponentHealth);
        cardData.attackAllOpponentCards = EditorGUILayout.Toggle(
            new GUIContent("Attack All Opponent Cards", "If checked, the card will attack all opponent cards."),
            cardData.attackAllOpponentCards);

        // Show element-related fields only when AttackPerTurn is selected
        cardData.elementType = (ElementType)EditorGUILayout.EnumPopup(
            new GUIContent("Element Type", "The elemental type of the card."),
            cardData.elementType);

        if (cardData.elementType != ElementType.None)
        {
            cardData.opposingElementType = (ElementType)EditorGUILayout.EnumPopup(
                new GUIContent("Opposing Element Type", "The opposing elemental type of the card."),
                cardData.opposingElementType);

            cardData.damageIncreasePercentage = EditorGUILayout.IntField(
                new GUIContent("Damage Increase Percentage", "The percentage increase in damage against the opposing element."),
                cardData.damageIncreasePercentage);
        }
    }

    private void DrawRegenHealthPerTurnFields()
    {
        cardData.healthRegenPerTurn = EditorGUILayout.IntField(
            new GUIContent("Health Regen Per Turn", "The health points to regen each turn."),
            cardData.healthRegenPerTurn);
        cardData.healAllCards = EditorGUILayout.Toggle(
            new GUIContent("Heal All Cards", "If checked, the card will heal all cards for the player."),
            cardData.healAllCards);
        cardData.healthPoints = EditorGUILayout.IntField(
            new GUIContent("Health Points", "The health points of the placed card."),
            cardData.healthPoints);
    }

    private void DrawRegenManaPerTurnFields()
    {
        cardData.manaRegenPerTurn = EditorGUILayout.IntField(
            new GUIContent("Mana Regen Per Turn", "The mana points to regen each turn."),
            cardData.manaRegenPerTurn);
        cardData.healthPoints = EditorGUILayout.IntField(
            new GUIContent("Health Points", "The health points of the placed card."),
            cardData.healthPoints);
    }

    private void DrawRegeneratePlayerHealthFields()
    {
        cardData.instantCastHealthRegen = EditorGUILayout.IntField(
            new GUIContent("Instant Health Regen", "The amount of health instantly restored to the player."),
            cardData.instantCastHealthRegen);
    }

    private void DrawAttackAllOpposingCardsFields()
    {
        cardData.instantCastAttackPoints = EditorGUILayout.IntField(
            new GUIContent("Attack Points", "The attack points of the card."),
            cardData.instantCastAttackPoints);
        cardData.attackOpponentHealth = EditorGUILayout.Toggle(
            new GUIContent("Attack Opponent Health", "If checked, the card will also attack the opponent's health points."),
            cardData.attackOpponentHealth);
    }

    private void DrawAttackOpposingPlayerFields()
    {
        cardData.instantCastAttackPoints = EditorGUILayout.IntField(
            new GUIContent("Attack Points", "The attack points used against the opposing player's health points."),
            cardData.instantCastAttackPoints);
    }

    private void DrawSaveButton()
    {
        if (GUILayout.Button(new GUIContent("Save Card Template", "Saves the current card data as a template.")))
        {
            SaveCardTemplate();
            isTemplateCreated = true; // Set the flag once the template is created
        }
    }

    private void SaveCardTemplate()
    {
        string templateFolder = "Assets/CARDtemplates";
        string templatePath = $"{templateFolder}/{cardData.name}_Template.asset";

        if (!AssetDatabase.IsValidFolder(templateFolder))
        {
            CreateTemplateFolder();
        }

        // Check if the asset already exists
        CardData existingTemplate = AssetDatabase.LoadAssetAtPath<CardData>(templatePath);

        if (existingTemplate != null)
        {
            bool userConfirmed = EditorUtility.DisplayDialog(
                "Overwrite Template?",
                $"A template with the name '{cardData.name}' already exists. Do you want to overwrite it?",
                "Yes",
                "No");

            if (!userConfirmed)
            {
                // User chose not to overwrite
                return;
            }

            // Copy the data from the existing template to the current cardData
            EditorUtility.CopySerialized(existingTemplate, cardData);
        }

        // Overwrite existing asset or create a new one
        AssetDatabase.CreateAsset(cardData, templatePath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = cardData;

        // Bring the editor window to the front after saving
        ShowWindow();
    }

    private void CreateTemplateFolder()
    {
        if (!AssetDatabase.IsValidFolder("Assets/CARDtemplates"))
        {
            AssetDatabase.CreateFolder("Assets", "CARDtemplates");
        }
    }

    private void CreateNewCard()
    {
        cardData = CreateInstance<CardData>();
        cardData.name = "NewCard";
        Selection.activeObject = cardData;
    }
}
