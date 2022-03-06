﻿using Dungeon12.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon12.Entities.Abilities.Priest
{
    /// <summary>
    /// Круг исцеления - аое хил, преобразуется в аое урон, аое щиты, аое ослепление/оглушение, аое бафы, = свою защиту если танк, ауры
    /// </summary>
    public class PriestHolyNova : Ability
    {
        public override Archetype Class => Archetype.Priest;

        public override void Bind()
        {
            Name = Global.Strings.PriestHolyNova;
            Description = Global.Strings.PriestHolyNovaDescT1;
            Area = new AbilityArea(all: true);
            Element = Element.HolyMagic;
            Cooldown = 5;
            UseRange = AbilRange.Friendly;
        }

        public override string[] GetTextParams()
        {
            return new string[] {
                $"{Global.Strings.Heal}: {Value}",
                $"{Global.Strings.Type}: {Element.Display()}",
                " ",
                Global.Strings.NotAffectSummoned
            };
        }
    }
}