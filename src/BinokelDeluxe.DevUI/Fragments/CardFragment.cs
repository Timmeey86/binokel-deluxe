﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BinokelDeluxe.DevUI.Fragments
{
    /// <summary>
    /// This UI fragment is responsible for drawing and detecting clicks on cards.
    /// </summary>
    internal class CardFragment
    {
        private readonly Dictionary<Common.Card, DevCard> _cardGraphics = new Dictionary<Common.Card, DevCard>();

        private readonly Func<Texture2D> _getBackTexture;
        private readonly Func<Texture2D> _getFrontTexture;
        private readonly Func<SpriteFont> _getFont;

        private int _dealerNumber = -1;
        private IEnumerable<IEnumerable<Common.Card>> _cardsPerPlayer;

        // For less calculation
        private IList<Vector2> _playerPositions = null;
        private int _amountOfPlayers = 0;
        private int _amountOfCardsPerPlayer = 0;

        public CardFragment(Func<Texture2D> getCardBackTexture, Func<Texture2D> getCardFrontTexture, Func<SpriteFont> getFont)
        {
            _getBackTexture = getCardBackTexture;
            _getFrontTexture = getCardFrontTexture;
            _getFont = getFont;
        }

        /// <summary>
        /// Displays the given list of cards on the fragment.
        /// </summary>
        /// <param name="cardsPerPlayer">The cards for each player.</param>
        public void SetCards(IEnumerable<IEnumerable<Common.Card>> cardsPerPlayer)
        {
            _amountOfPlayers = cardsPerPlayer.Count();
            _amountOfCardsPerPlayer = cardsPerPlayer.First().Count();
            _cardsPerPlayer = cardsPerPlayer;

            CalculatePlayerPositions();
            CalculateCardGraphics(cardsPerPlayer);
        }

        /// <summary>
        /// Sets the number of the dealer.
        /// </summary>
        /// <param name="dealerNumber">The position of the dealer, where 0 is the user.</param>
        public void SetDealer(int dealerNumber)
        {
            _dealerNumber = dealerNumber;
            // TODO: Add dealer button
        }
        
        public void Update(GameTime gameTime, InputHandler inputHandler)
        {
            _cardGraphics.Values.ToList().ForEach(card => card.Update(gameTime, inputHandler));

            if (inputHandler.ReleasedPoint.HasValue)
            {
                // Try detecting clicks on cards
                foreach (var playerCards in _cardsPerPlayer)
                {
                    foreach (var card in playerCards.Reverse())
                    {
                        var cardGraphics = _cardGraphics[card];
                        if (cardGraphics.IsInDrawingArea(inputHandler.ReleasedPoint.Value))
                        {
                            cardGraphics.IsCovered = !cardGraphics.IsCovered;
                            Console.WriteLine(String.Format(
                                "Clicked {0} of {1} from deck {2}",
                                card.Type.ToString(),
                                card.Suit.ToString(),
                                card.DeckNumber.ToString()
                                ));
                            break; // Detecting one card is sufficient
                        }
                    }
                }
                inputHandler.Reset();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _cardGraphics.Values.ToList().ForEach(card => card.Draw(spriteBatch));
        }

        private void CalculateCardGraphics(IEnumerable<IEnumerable<Common.Card>> cardsPerPlayer)
        {
            var backTexture = _getBackTexture();
            var frontTexture = _getFrontTexture();
            var font = _getFont();
            var playerPosition = 0;
            var cardNumber = 0;
            foreach (var playerCards in cardsPerPlayer)
            {
                cardNumber = 0;
                foreach (var card in playerCards)
                {
                    _cardGraphics.Add(
                        card,
                        new DevCard(backTexture, frontTexture, font)
                        {
                            Card = card,
                            Position = _playerPositions[playerPosition],
                            // 10 degrees rotation per card, with the middle card having a rotation of zero.
                            Angle = (cardNumber - _amountOfCardsPerPlayer / 2 - 1) * 10f
                        });
                    Debug.Assert(_cardGraphics.ContainsKey(card));
                    cardNumber++;
                }
                playerPosition++;
            }
        }

        private void CalculatePlayerPositions()
        {
            if (_amountOfPlayers == 3)
            {
                _playerPositions = new List<Vector2>()
                {
                    new Vector2(400, 480),
                    new Vector2(650, 280),
                    new Vector2(150, 280)
                };
            }
            else
            {
                _playerPositions = new List<Vector2>()
                {
                    new Vector2(400, 480),
                    new Vector2(650, 280),
                    new Vector2(400, 200),
                    new Vector2(150, 280)
                };
            }
        }
    }
}