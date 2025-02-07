﻿using Nabunassar.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nabunassar.Entities.Abilities.Mage
{
    internal class MageArrowAttack : Ability
    {
        public override Archetype Archetype => Archetype.Mage;

        public override void Bind()
        {
            Area = new AbilityArea();
            Element = Element.Magic;
            UseRange = AbilRange.Any;
        }

        public override string[] GetTextParams()
        {
            return new string[] {
                $"{Global.Strings["Damage"]}: {Value}",
                $"{Global.Strings["Type"]}: {Element.Display()}",
                $"{Global.Strings["Range"]}: {UseRange.Display()}"
            };
        }
    }
}
