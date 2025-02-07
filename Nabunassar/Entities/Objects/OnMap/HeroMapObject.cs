﻿using Dungeon.SceneObjects.Grouping;

namespace Nabunassar.Entities.Objects.OnMap
{
    internal class HeroMapObject : MapObject
    {
        private readonly Hero _hero;
        
        public HeroMapObject(Hero hero)
        {
            _hero=hero;
            //_hero.OnSelect(() => Selected.True());
            GameObject=hero;
        }

        public ObjectGroupProperty Selected { get; set; } = new();

        public override bool IsSelected { get => Selected; set => Selected.Set(value); }

        public override void Select()
        {
            
        }
    }
}
