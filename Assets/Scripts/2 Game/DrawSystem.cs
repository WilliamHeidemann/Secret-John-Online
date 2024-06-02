using System;
using System.Collections.Generic;
using _2_Game;
using UnityEngine;
using UnityEngine.UI;

public class DrawSystem : MonoBehaviour
{
    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private Sprite liberalSprite;
    [SerializeField] private Sprite fascistSprite;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;
    [SerializeField] private GameObject confirmButton;
    
    private Alignment card1;
    private Alignment card2;
    private Alignment card3;

    private int selectedCards;
    private bool isCard1Selected;
    private bool isCard2Selected;
    private bool isCard3Selected;

    private bool isPresident;
    
    public void SetPolicies(Alignment first, Alignment second)
    {
        card1 = first;
        card2 = second;
        image1.sprite = GetSprite(first);
        image2.sprite = GetSprite(second);
        image3.gameObject.SetActive(false);
        isPresident = false;
    }

    public void SetPolicies(Alignment first, Alignment second, Alignment third)
    {
        card1 = first;
        card2 = second;
        card3 = third;
        image1.sprite = GetSprite(first);
        image2.sprite = GetSprite(second);
        image3.sprite = GetSprite(third);
        image3.gameObject.SetActive(true);
        isPresident = true;
    }

    private Sprite GetSprite(Alignment alignment) => alignment == Alignment.Liberal ? liberalSprite : fascistSprite;

    public void ToggleSelect(int index)
    {
        switch (index)
        {
            case 0:
                isCard1Selected = !isCard1Selected;
                selectedCards += isCard1Selected ? 1 : -1;
                var image1Color = image1.color;
                image1Color.a = isCard1Selected ? 1f : 0.5f;
                image1.color = image1Color;
                break;
            case 1:
                isCard2Selected = !isCard2Selected;
                selectedCards += isCard2Selected ? 1 : -1;
                var image2Color = image2.color;
                image2Color.a = isCard2Selected ? 1f : 0.5f;
                image2.color = image2Color;
                break;
            case 2:
                isCard3Selected = !isCard3Selected;
                selectedCards += isCard3Selected ? 1 : -1;
                var image3Color = image3.color;
                image3Color.a = isCard3Selected ? 1f : 0.5f;
                image3.color = image3Color;
                break;
        }

        if (isPresident) confirmButton.SetActive(selectedCards == 2);
        else confirmButton.SetActive(selectedCards == 1);
    }

    public void Confirm()
    {
        if (isPresident) ConfirmPresidentPick();
        else ConfirmChancellorPick();
    }

    private void ConfirmPresidentPick()
    {
        var toDiscard = card1;
        if (!isCard1Selected) toDiscard = card1;
        else if (!isCard2Selected) toDiscard = card2;
        else if (!isCard3Selected) toDiscard = card3;
        gameStateManager.DiscardPolicyRpc(toDiscard);
        
        // Different solution:
        // Have two collections, one for policies to discard, one for policies to forward/enact. 
        // Toggling a card moves it from one collection to the other. 
        // Otherwise finding the policy to discard / forward is very cumbersome. 
    }

    private void ConfirmChancellorPick()
    {
        
    }
}