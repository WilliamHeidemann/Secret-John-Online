using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace _2_Game
{
    public class ElectionSystem : MonoBehaviour
    {
        [SerializeField] private GameStateManager gameStateManager;

        [SerializeField] private List<Image> playerNames;
        [SerializeField] private Image confirmButton;
        [SerializeField] private TextMeshProUGUI confirmButtonText;

        private State state;

        private int presidentId;
        private int chancellorId;

        private const float ReducedAlphaValue = 0.2f;

        private enum State
        {
            None,
            SelectPresident,
            SelectChancellor,
            Confirm
        }

        public void SelectPlayer(int index)
        {
            var playerBox = playerNames[index];

            if (state is State.SelectPresident)
            {
                presidentId = index;
                playerBox.SetAlpha(ReducedAlphaValue);
                state = State.SelectChancellor;
                confirmButtonText.text = "Select Chancellor";
            }
            else if (state is State.SelectChancellor)
            {
                if (index == presidentId)
                {
                    playerBox.SetAlpha(1f);
                    state = State.SelectPresident;
                    confirmButtonText.text = "Select President";
                    return;
                }

                chancellorId = index;
                playerBox.SetAlpha(ReducedAlphaValue);
                state = State.Confirm;
                confirmButtonText.text = "Confirm";
                confirmButton.SetAlpha(1f);
            }
            else if (state is State.Confirm)
            {
                if (index == chancellorId)
                {
                    playerBox.SetAlpha(1f);
                    state = State.SelectChancellor;
                    confirmButtonText.text = "Select Chancellor";
                    confirmButton.SetAlpha(ReducedAlphaValue);
                }
                else ResetState();
            }
        }

        public void GovernmentButtonClick()
        {
            if (state is State.None) StartSelectPresident();
            else if (state is State.SelectPresident or State.SelectChancellor) ResetState();
            else if (state is State.Confirm) Confirm();
        }

        private void Confirm()
        {
            gameStateManager.ElectGovernmentRpc((ulong)presidentId, (ulong)chancellorId);
            ResetState();
        }

        private void StartSelectPresident()
        {
            state = State.SelectPresident;
            confirmButton.SetAlpha(ReducedAlphaValue);
        }

        public void ResetState()
        {
            state = State.None;
            confirmButton.SetAlpha(1f);
            confirmButtonText.text = "Select President";
            playerNames.ForEach(image => image.SetAlpha(1f));
        }
    }
}