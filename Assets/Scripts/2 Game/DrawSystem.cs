using System;
using System.Collections.Generic;
using System.Linq;
using _2_Game;
using UnityEngine;
using UnityEngine.UI;
using Utility;

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

    private bool isCard1Selected;
    private bool isCard2Selected;
    private bool isCard3Selected;

    private bool isPresident;

    private List<Alignment> selected;
    private List<Alignment> discarded;
    
    public void SetPolicies(Alignment first, Alignment second)
    {
        card1 = first;
        card2 = second;
        image1.sprite = GetSprite(first);
        image2.sprite = GetSprite(second);
        image3.gameObject.SetActive(false);
        isPresident = false;
        
        selected = new List<Alignment>();
        discarded = new List<Alignment> { card1, card2 };
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
        
        selected = new List<Alignment>();
        discarded = new List<Alignment> { card1, card2, card3 };
    }

    private Sprite GetSprite(Alignment alignment) => alignment == Alignment.Liberal ? liberalSprite : fascistSprite;

    private Alignment GetAlignment(int index) => index switch
    {
        0 => card1,
        1 => card2,
        2 => card3,
        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
    };

    private bool IsSelected(int index) => index switch
    {
        0 => isCard1Selected,
        1 => isCard2Selected,
        2 => isCard3Selected,
        _ => throw new ArgumentOutOfRangeException(nameof(index), index, null)
    };

    public void ToggleSelect(int index)
    {
        switch (index)
        {
            case 0:
                isCard1Selected = !isCard1Selected;
                image1.SetAlpha(isCard1Selected ? 1f : 0.5f);
                break;
            case 1:
                isCard2Selected = !isCard2Selected;
                image2.SetAlpha(isCard2Selected ? 1f : 0.5f);
                break;
            case 2:
                isCard3Selected = !isCard3Selected;
                image3.SetAlpha(isCard3Selected ? 1f : 0.5f);
                break;
        }

        var alignment = GetAlignment(index);
        if (IsSelected(index))
        {
            selected.Remove(alignment);
            discarded.Add(alignment);
        }
        else
        {
            selected.Add(alignment);
            discarded.Remove(alignment);
        }
        
        if (isPresident) confirmButton.SetActive(selected.Count == 2);
        else confirmButton.SetActive(selected.Count == 1);
    }

    public void Confirm()
    {
        if (isPresident) ConfirmPresidentPick();
        else ConfirmChancellorPick();
    }

    private void ConfirmPresidentPick()
    {
        var toDiscard = discarded.First();
        gameStateManager.DiscardPolicyRpc(toDiscard);

        var selectedArray = selected.ToArray();
        var (first, second) = (selectedArray[0], selectedArray[1]);
        gameStateManager.ForwardPoliciesRpc(first, second);
    }

    private void ConfirmChancellorPick()
    {
        var toDiscard = discarded.First();
        gameStateManager.DiscardPolicyRpc(toDiscard);
        
        var toSelect = selected.First();
        gameStateManager.EnactPolicyRpc(toSelect);
    }
}