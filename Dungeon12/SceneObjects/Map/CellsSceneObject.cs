﻿using Dungeon;
using Dungeon.Control;
using Dungeon.Drawing.SceneObjects;
using Dungeon.SceneObjects;
using Dungeon12.Entities.Map;
using Dungeon12.Functions.ObjectFunctions;
using System.Collections.Generic;

namespace Dungeon12.SceneObjects.Map
{
    public class CellsSceneObject : SceneControl<Location>
    {
        public override bool AbsolutePosition => true;

        private class CloseBtn : EmptySceneControl
        {
            ExploreSceneObject exploreSceneObject;

            public CloseBtn(ExploreSceneObject exploreSceneObject)
            {
                this.Image = "Backgrounds/cross.png".AsmImg();
                this.exploreSceneObject = exploreSceneObject;
                this.Width = 48;
                this.Height = 48;
            }

            public override void Click(PointerArgs args)
            {
                Global.Helps.StepClick();
                exploreSceneObject.Destroy();
                Global.Freezer.Unfreeze();
            }

            public override void Focus()
            {
                this.Image = "Backgrounds/cross_bump.png".AsmImg();
            }
            public override void Unfocus()
            {
                this.Image = "Backgrounds/cross.png".AsmImg();
            }
        }

        public static CellsSceneObject Active;

        public void Close()
        {
            closebtn.Click(null);
        }

        private CloseBtn closebtn;

        public CellsSceneObject(ExploreSceneObject explore, Location location) : base(location, true)
        {
            Global.Freezer.Freeze(this);
            Image = "Backgrounds/location.png".AsmImg();
            Active = this;

            closebtn = this.AddChild(new CloseBtn(explore)
            {
                Left = Width - 50,
            });

            this.AddChild(new CenterCell(location, explore)
            {
                Left = 168,
                Top = 156,
                Width = 210,
                Height = 210
            });

            //this.AddChild(Background = new ImageObject(location.Background.AsmRes())
            //{
            //    Left = 168,
            //    Top = 156,
            //});

            //this.AddChild(Object = new ImageObject(location.Object.AsmRes())
            //{
            //    Left = 168,
            //    Top = 156
            //});


            this.AddChild(new PolygonSceneObject(location.Polygon.P0, location.BackgroundImage, 0));
            this.AddChild(new PolygonSceneObject(location.Polygon.P1, location.BackgroundImage, 1));
            this.AddChild(new PolygonSceneObject(location.Polygon.P2, location.BackgroundImage, 2));
            this.AddChild(new PolygonSceneObject(location.Polygon.P3, location.BackgroundImage, 3));
            this.AddChild(new PolygonSceneObject(location.Polygon.P4, location.BackgroundImage, 4));
            this.AddChild(new PolygonSceneObject(location.Polygon.P5, location.BackgroundImage, 5));



#warning DEVELOP
            Global.Game.Party.Hero1.Fraction = Entities.Enums.Fraction.MageGuild;
            Global.Game.Location.Polygon.P4.Load(new Entities.Map.Polygon
            {
                Name = "Должность",
                Icon = "specscroll.png",
                Function = nameof(SelectSpecFunction)
            });
        }

        private List<PolygonSceneObject> Polygons = new List<PolygonSceneObject>();

        private class CenterCell : EmptySceneControl
        {
            private ExploreSceneObject exploreSceneObject;

            public CenterCell(Location location, ExploreSceneObject exploreSceneObject)
            {
                this.exploreSceneObject = exploreSceneObject;
                this.AddChild(new ImageObject(location.BackgroundImage.AsmRes())
                {
                    Width = 210,
                    Height = 210
                });

                var icon = location.Polygon?.Icon;
                if (icon == null)
                    icon = location.BackgroundImage.AsmRes();
                else
                    icon = $"Icons/{location.Polygon?.Icon}".AsmImg();

                this.AddChild(new ImageObject(icon)
                {
                    Width = 210,
                    Height = 210
                });

                if (location.Polygon.ObjectImage != null)
                {
                    var Object = new ImageObject(() => $"SpecChips/{location.Polygon.ObjectImage}".AsmImg())
                    {
                        Width = 210,
                        Height = 210,
                        Top=-15
                    };
                    this.AddChild(Object);
                }
            }

            public override void Focus()
            {
                this.exploreSceneObject.ExploreTitle.StartBlink();
            }

            public override void Unfocus()
            {
                this.exploreSceneObject.ExploreTitle.StopBlink();
            }
        }

        public override double Width => 545;

        public override double Height => 525;
    }
}