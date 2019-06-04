﻿namespace Rogue.Drawing.SceneObjects.Map
{
    using Rogue.Entites.Alive;
    using Rogue.Entites.Animations;
    using Rogue.Map;
    using Rogue.Types;
    using Rogue.View.Interfaces;
    using System;
    using System.Collections.Generic;

    public class MoveableSceneObject : AnimatedSceneObject
    {
        protected Moveable moveable;
        protected GameMap location;

        protected MapObject mapObj;

        public MoveableSceneObject(GameMap location,MapObject mapObj,Moveable moveable,Rectangle defaultFramePosition, Action<List<ISceneObject>> showEffects) : base(mapObj.Name,defaultFramePosition, showEffects)
        {
            this.mapObj = mapObj;
            this.moveable = moveable;
            this.location = location;
        }

        private int moveDistance = 0;
        private readonly HashSet<int> moves = new HashSet<int>();

        protected override void DrawLoop()
        {
            if (moveDistance > 0)
            {
                moveDistance--;
                
                foreach (var move in moves)
                {
                    Move(DirectionMap[move]);
                }
            }
            else if (moveDistance < 0)
            {
                moveDistance++;
            }
        }
        protected override void AnimationLoop()
        {
            if (moveDistance != 0)
                return;

            var next = moveable.WalkChance.Random();
            if (next > moveable.WalkChance.Mid())
            {
                moves.Clear();

                var direction = RandomRogue.Next(0, 4);

                if (DirectionMap[direction].dir == lastClosedDirection)
                    return;

                moves.Add(direction);

                var diagonally = RandomRogue.Next(0, 4);
                if (diagonally != direction && NotPair(direction, diagonally))
                {
                    moves.Add(diagonally);
                }

                moveDistance = moveable.WalkDistance.Random();
            }
        }

        private void Move((Direction dir, Vector vect, Func<Moveable, AnimationMap> anim) data)
        {
            switch (data.dir)
            {
                case Direction.Up:
                case Direction.Down:
                    switch (data.vect)
                    {
                        case Vector.Plus:
                            mapObj.Location.Y += mapObj.MovementSpeed;
                            break;
                        case Vector.Minus:
                            mapObj.Location.Y -= mapObj.MovementSpeed;
                            break;
                        default:
                            break;
                    }
                    break;
                case Direction.Left:
                case Direction.Right:
                    switch (data.vect)
                    {
                        case Vector.Plus:
                            mapObj.Location.X += mapObj.MovementSpeed;
                            break;
                        case Vector.Minus:
                            mapObj.Location.X -= mapObj.MovementSpeed;
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            SetAnimation(data.anim(moveable));

            if (!CheckMoveAvailable(data.dir))
            {
                moveDistance = -moveable.WaitTime;
                SetAnimation(moveable.Idle);
            }
            else if (this.aliveTooltip != null)
            {
                this.aliveTooltip.Left = this.Position.X;
                this.aliveTooltip.Top = this.Position.Y - 0.8;
            }
        }

        private Direction lastClosedDirection;

        private bool CheckMoveAvailable(Direction direction)
        {
            var inMoveRegion = (moveable?.MoveRegion.IntersectsWith(this.mapObj) ?? false);

            if (inMoveRegion && this.location.Move(this.mapObj, direction))
            {
                this.Left = this.mapObj.Location.X;
                this.Top = this.mapObj.Location.Y;
                return true;
            }
            else
            {
                lastClosedDirection = direction;
                this.mapObj.Location.X = this.Left;
                this.mapObj.Location.Y = this.Top;
                return false;
            }
        }

        private readonly Dictionary<int, (Direction dir, Vector vect, Func<Moveable, AnimationMap> anim)> DirectionMap = new Dictionary<int, (Direction, Vector, Func<Moveable, AnimationMap>)>
        {
            { 0,(Direction.Up, Vector.Minus, m=>m.MoveUp) },
            { 1,(Direction.Down, Vector.Plus, m=>m.MoveDown) },
            { 2,(Direction.Left, Vector.Minus, m=>m.MoveLeft) },
            { 3,(Direction.Right, Vector.Plus, m=>m.MoveRight) },
        };


        private bool NotPair(int common, int additional)
        {
            if (common + additional == 1)
                return false;

            if (common + additional == 5)
                return false;

            return true;
        }

        private enum Vector
        {
            Plus,
            Minus
        }
    }
}
