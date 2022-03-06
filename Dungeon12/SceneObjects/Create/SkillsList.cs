﻿using Dungeon;
using Dungeon.Control;
using Dungeon.SceneObjects;
using Dungeon.Types;
using Dungeon.View.Interfaces;
using Dungeon12.ECS.Components;
using Dungeon12.Entities;
using Dungeon12.Entities.Enums;
using Dungeon12.SceneObjects.Base;
using System.Linq;

namespace Dungeon12.SceneObjects.Create
{
    public class SkillsList : SceneControl<Hero>
    {
        public SkillsList(Hero component) : base(component)
        {
            this.Height = 212;
            this.Width = 242;

            var title = this.AddTextCenter(Global.Strings.Skills.AsDrawText().Gabriela().InColor(Global.CommonColor).InSize(25), vertical: false);
            title.Top = 5;

            var skillgroups = typeof(Skill).All<Skill>()
                .Select(x => new
                {
                    val = x,
                    g = x.ToValue<string>()
                })
                .GroupBy(x => x.g, x => x.val);

            foreach (var group in skillgroups)
            {
                var top = 65;
                foreach (var skill in group)
                {
                    this.AddChild(new SkillListItem(Component, skill)
                    {
                        Top = top
                    });

                    top += 40;
                }
            }
        }

        public class SkillListItem : SceneControl<Hero>, ITooltiped, IMouseHint
        {
            Skill _skill;

            public SkillListItem(Hero component, Skill skill) : base(component)
            {
                _skill = skill;
                this.Width = 242;
                this.Height = 35;

                this.AddTextCenter(skill.Display().AsDrawText().Gabriela().InColor(Global.CommonColor).InSize(20));
            }

            public override bool Visible => Component.Class == _skill.Class();

            public IDrawText TooltipText => $"{Global.Strings.LeftMouseButton} - {Global.Strings.Info}".AsDrawText().Gabriela();

            public bool ShowTooltip => true;

            public GraphicsTooltip CreateMouseHint() =>
                new GraphicsTooltip(_skill.Display(), 
                    Global.Strings.ByProperty($"{_skill}Desc"), 
                    GraphicsTooltipSize.Auto);

            public void RefreshTooltip() { }
        }
    }
}