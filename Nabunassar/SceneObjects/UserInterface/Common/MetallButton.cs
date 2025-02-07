﻿using Dungeon;
using Dungeon.Control;
using Dungeon.Drawing;
using Dungeon.SceneObjects;
using System;

namespace Nabunassar.SceneObjects.UserInterface.Common
{
    internal class MetallButton : EmptySceneControl
    {
        readonly TextObject Label;

        public MetallButton(string text)
        {
            this.Width = 267;
            this.Height = 77;

            Label = this.AddTextCenter(text.AsDrawText().Gabriela().InSize(24));
        }

        private bool _disabled;
        public bool Disabled
        {
            get => _disabled;
            set
            {
                _disabled = value;
                if (value)
                    Label.Text.ForegroundColor = DrawColor.Gray;
                else
                    Label.Text.ForegroundColor = DrawColor.White;
            }
        }

        public void SetText(string txt)
        {
            Label.Text.SetText(txt);
        }

        public Action OnClick { get; set; }

        public override void Click(PointerArgs args)
        {
            OnClick?.Invoke();
        }

        public override string Image { get; set; } = "UI/Common/mbutton.png".AsmImg();

        public override void Focus()
        {
            if (Disabled)
                return;

            AudioPlayer.Sound("focus.wav".AsmSoundRes());
            Image = "UI/Common/mbutton_f.png".AsmImg();
            //this.textControl.Strings.Paint(ActiveColor, true);
        }

        public override void Unfocus()
        {
            if (Disabled)
                return;

            Image = "UI/Common/mbutton.png".AsmImg();
            //this.textControl.Strings.Paint(InactiveColor, true);
        }
    }
}