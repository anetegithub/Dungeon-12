﻿using System;

namespace Dungeon.Utils
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public sealed class GroupAttribute : ValueAttribute
    {
        public GroupAttribute(string name) : base(name) { }
    }
}