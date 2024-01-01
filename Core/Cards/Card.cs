﻿namespace Core.Cards
{
    public sealed class Card: BaseEntity
    {
        public string CardNumber { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public CardStatus Status { get; set; } = CardStatus.Active;

    }
}
