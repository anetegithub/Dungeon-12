﻿namespace Dungeon12.Drawing.SceneObjects.Dialogs.Shop
{
    using Dungeon;
    using Dungeon.Control.Keys;
    using Dungeon.Drawing.SceneObjects.Map;
    using Dungeon.Drawing.SceneObjects.UI;
    using Dungeon.Map;
    using Dungeon.Merchants;
    using Dungeon.View.Interfaces;
    using Dungeon12.Drawing.SceneObjects.Inventories;
    using Dungeon12.Drawing.SceneObjects.Main.CharacterInfo;
    using System;

    public class ShopWindow : DraggableControl<ShopWindow>
    {
        public override int Layer => 50;

        public override bool AbsolutePosition => true;

        protected override Key[] OverrideKeyHandles => new Key[] { Key.Escape };

        public ShopWindow(string title, PlayerSceneObject playerSceneObject, Merchant shop, Action<ISceneObject> destroyBinding, Action<ISceneObjectControl> controlBinding, GameMap gameMap)
            : base(default)
        {
            Global.Freezer.World = this;

            this.Top = 2;
            this.Left = 0;

            var charInfo = new CharacterInfoWindow(gameMap, playerSceneObject, this.ShowEffects, false,false)
            {
                Left = 16 + 5,
                DisableDrag = true
            };
            charInfo.Top = 0;

            var shopWindow = new ShopWindowContent(title, shop, playerSceneObject, charInfo.Inventory)
            {
                Left = 5,
            };

            shopWindow.BindCharacterInventory(charInfo.Inventory);

            this.Width = 28;
            this.Height = 17;

            this.AddChild(shopWindow);
            this.AddChild(charInfo);
            this.AddChild(new InventoryDropItemMask(playerSceneObject, charInfo.Inventory, gameMap)
            {
                Top = -2
            });
        }

        public override void KeyDown(Key key, KeyModifiers modifier, bool hold) => Close();

        private void Close()
        {
            this.Destroy?.Invoke();
            Global.Freezer.World = null;
            Global.Interacting = false;
        }
    }
}