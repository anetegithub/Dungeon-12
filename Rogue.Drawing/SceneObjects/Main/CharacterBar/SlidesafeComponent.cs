﻿using Rogue.Drawing.SceneObjects.Map;
using Rogue.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rogue.Drawing.SceneObjects.Main.CharacterBar
{
    public abstract class SlidesafeComponent : TooltipedSceneObject
    {
        public double SlideOffsetLeft = 0;
        public double SlideOffsetTop = 0;
        public Func<bool> SlideNeed = () => true;

        public SlidesafeComponent(string tooltip, Action<List<ISceneObject>> showEffects) : base(tooltip, showEffects)
        {
        }

        public override double Left
        {
            get => SlideNeed.Invoke() ? base.Left+SlideOffsetLeft : base.Left;
            set => base.Left = value;
        }

        public override double Top
        {
            get => SlideNeed.Invoke() ? base.Top + SlideOffsetTop : base.Top;
            set => base.Top = value;
        }
    }
}