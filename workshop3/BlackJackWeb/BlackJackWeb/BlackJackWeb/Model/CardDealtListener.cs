﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model
{
    public interface CardDealtListener
    {
        void CardDealt(Card a_card, Player a_player);
    }
}
