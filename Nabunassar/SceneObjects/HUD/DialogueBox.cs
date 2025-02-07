﻿using Nabunassar.SceneObjects.Base;
using Nabunassar.SceneObjects.UserInterface.Common;

namespace Nabunassar.SceneObjects.HUD
{
    internal class DialogueBox : EmptySceneControl, IAutoFreeze
    {
        public DialogueBox(string left, string right, string text, bool autoresize=true)
        {
            this.Width = 525;

            var drawtext = text.Gabriela().InColor(Global.CommonColorLight).InSize(22).WithWordWrap();

            this.Height = this.MeasureText(drawtext, this).Y + 100;

            this.AddBorderMapBack(new BorderConfiguration()
            {
                ImagesPath = "UI/bordermin/bord21.png",
                Size = 16,
                Padding = 2
            });

            this.AddTextCenter(drawtext, vertical: false).Top = 20;

            this.AddChild(new ClassicButton(left,100,30,22)
            {
                OnClick = () => OnLeft?.Invoke(),
                Left = 125,
                Top = 75
            });
            this.AddChild(new ClassicButton(right, 100, 30, 22)
            {
                OnClick = () => OnRight?.Invoke(),
                Left = 300,
                Top = 75
            });
        }

        public Action OnLeft { get; set; }

        public Action OnRight { get; set; }
    }
}
