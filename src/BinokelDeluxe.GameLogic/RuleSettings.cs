﻿using System;

// DOCUMENTED

namespace BinokelDeluxe.GameLogic
{
    public enum GameType
    {
        /// <summary>
        /// A game where three players play against each other.
        /// </summary>
        ThreePlayerGame,
        /// <summary>
        /// A game where four players play in teams of two against each other.
        /// </summary>
        FourPlayerCrossBinokelGame
    }

    public enum CountingType
    {
        /// <summary>
        /// Ten points for aces, tens and kings, zero points for the remainder.
        /// </summary>
        TenPointsForAceToKing,
        /// <summary>
        /// Eleven points for aces, ten for tens, four for kings, 3 for obers, 2 for unters.
        /// </summary>
        DecreasingPointsForAceToUnter
    }

    /// <summary>
    /// This class contains any setting which affects the game logic.
    /// </summary>
    public class RuleSettings : Common.IConfigurable, IEquatable<RuleSettings>
    {
        /// <summary>
        /// Defines whether three or four player binokel is being played.
        /// </summary>
        public GameType GameType { set; get; } = GameType.ThreePlayerGame;

        /// <summary>
        /// True if the cards which display a seven shall be included.
        /// </summary>
        public bool SevensAreIncluded { set; get; } = true;

        /// <summary>
        /// Defines the score of each card.
        /// </summary>
        public CountingType CountingType { set; get; } = CountingType.DecreasingPointsForAceToUnter;

        /// <summary>
        /// True if the score for each player/team shall be rounded towards the next multiple of ten.
        /// </summary>
        public bool ScoresWillBeRounded { set; get; } = true;

        /// <summary>
        /// True if, when one player goes out, all others will receive 10 points per player in addition to their meld.
        /// </summary>
        public bool ExtraPointsForOthersWhenGoingOut { set; get; } = true;

        /// <summary>
        /// True if the seven of trumps can be melded for ten points.
        /// </summary>
        public bool ExtraPointsForSevenOfTrumps { set; get; } = true;

        /// <summary>
        /// True if the ten points are awarded for winning the last trick, false if ten points are awarded for winning the first trick.
        /// </summary>
        public bool ExtraPointsForLastTrickInsteadOfFirst { set; get; } = true;

        /// <summary>
        /// True if the seven of trumps may be discarded despite being melded.
        /// </summary>
        public bool SevenOfTrumpsCanBeMeldedAndDiscarded { set; get; } = true;

        /// <summary>
        /// True if a Bettel (attempt to lose all tricks, and gaining 1000 points in case of success) is allowed.
        /// </summary>
        public bool BettelsAreAllowed { set; get; } = false;

        /// <summary>
        /// True if when player B plays a trump because he cannot follow suit, player C is forced to play a higher trump if he has one and also cannot follow suit.
        /// If false, player C still needs to play a trump but may play a lower trump than player B.
        /// </summary>
        public bool SecondTrumpMustAlsoWin { set; get; } = true;

        /// <summary>
        /// Checks whether this and other are equal.
        /// Two rule settings objects are equal if all their properties are equal.
        /// </summary>
        /// <param name="other">The other object.</param>
        /// <returns>True if this and other are equal.</returns>
        public bool Equals(RuleSettings other)
        {
            if (other == null)
            {
                return false;
            }
            return
                Enum.Equals(GameType, other.GameType) &&
                SevensAreIncluded == other.SevensAreIncluded &&
                Enum.Equals(CountingType, other.CountingType) &&
                ScoresWillBeRounded == other.ScoresWillBeRounded &&
                ExtraPointsForLastTrickInsteadOfFirst == other.ExtraPointsForLastTrickInsteadOfFirst &&
                ExtraPointsForOthersWhenGoingOut == other.ExtraPointsForOthersWhenGoingOut &&
                ExtraPointsForSevenOfTrumps == other.ExtraPointsForSevenOfTrumps &&
                SevenOfTrumpsCanBeMeldedAndDiscarded == other.SevenOfTrumpsCanBeMeldedAndDiscarded &&
                BettelsAreAllowed == other.BettelsAreAllowed &&
                SecondTrumpMustAlsoWin == other.SecondTrumpMustAlsoWin;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as RuleSettings);
        }

        public override int GetHashCode()
        {
            unchecked // overflow is fine, just wrap
            {
                var hash = 17;
                hash = hash * 29 + GameType.GetHashCode();
                hash = hash * 29 + SevensAreIncluded.GetHashCode();
                hash = hash * 29 + CountingType.GetHashCode();
                hash = hash * 29 + ScoresWillBeRounded.GetHashCode();
                hash = hash * 29 + ExtraPointsForLastTrickInsteadOfFirst.GetHashCode();
                hash = hash * 29 + ExtraPointsForOthersWhenGoingOut.GetHashCode();
                hash = hash * 29 + ExtraPointsForSevenOfTrumps.GetHashCode();
                hash = hash * 29 + SevenOfTrumpsCanBeMeldedAndDiscarded.GetHashCode();
                hash = hash * 29 + BettelsAreAllowed.GetHashCode();
                hash = hash * 29 + SecondTrumpMustAlsoWin.GetHashCode();
                return hash;
            }
        }
    }
}
