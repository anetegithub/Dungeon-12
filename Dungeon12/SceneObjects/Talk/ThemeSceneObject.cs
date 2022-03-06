﻿using Dungeon;
using Dungeon.Drawing.SceneObjects;
using Dungeon.SceneObjects;
using Dungeon12.Entities.Talks;

namespace Dungeon12.SceneObjects.Talk
{
    public class ThemeSceneObject : SceneControl<Dialogue>
    {
        public ThemeSceneObject(Dialogue component) : base(component)
        {
            this.Width = 400;
            this.Height = 920;
            this.Image = @"Talk/themes.png".AsmImg();

            this.Blur = false;

            var ava = this.AddChild(new ImageObject($"Npc/{component.Avatar}".AsmImg())
            {
                Width = 277,
                Height = 436,
                Left = 65,
                Top = 126
            });

            var position = component.Name.Split('*')[0];
            var name = component.Name.Split('*')[1];

            var pos = this.AddTextCenter(position.AsDrawText().Gabriela().InSize(24), vertical: false);
            pos.Top = 30;
            var nme = this.AddTextCenter(name.AsDrawText().Gabriela().InSize(24), vertical: false);
            nme.Top = 75;

            var goaltop = 127;

            foreach (var goal in component.Goals)
            {
                this.AddChild(new GoalSceneObject(component.Id, goal)
                {
                    Top = goaltop,
                    Left = ava.Left + ava.Width - 50
                });

                goaltop += 48;
            }

            var top = ava.Top + ava.Height + 30;

            foreach (var subj in component.Subjects)
            {
                if (!Global.Game.State.Dialogues.Contains(subj.SubjectId))
                {
                    var sbj = this.AddChildCenter(new SubjectSceneObject(subj));
                    sbj.Top = top;
                    top += 52;
                }
            }
        }
    }
}