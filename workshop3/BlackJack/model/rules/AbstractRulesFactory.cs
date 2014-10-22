using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack.model.rules
{
    public class AbstractRulesFactory<T> where T : new()
    {
        public T CreateObject()
        {
            return new T();
        }
    }
}
