﻿namespace Dungeon.Classes
{
    using Dungeon.Drawing;
    using Dungeon.View.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class ClassStatAttribute : Attribute
    {
        public string Title { get; }

        public IDrawColor Color { get; }

        public int Group { get; }

        public string Description { get; }

        public ClassStatAttribute(string title, string description="")
        {
            Title = title;
            Description = description;
        }

        public ClassStatAttribute(string title, IDrawColor color, string description = "") : this(title,description) => Color = color;

        public ClassStatAttribute(string title, ConsoleColor color, string description = "") : this(title, new DrawColor(color),description) { }

        public ClassStatAttribute(string title, int group, string description = "") : this(title,description) => Group = group;

        public ClassStatAttribute(string title, IDrawColor color, int group, string description = "") : this(title, color,description) => Group = group;

        public ClassStatAttribute(string title, ConsoleColor color, int group, string description = "") : this(title, new DrawColor(color), group,description) { }
    }

    public class ClassStat : BaseStatEquip
    {
        public string Description { get; set; }

        public ClassStat(string title, IEnumerable<string> properties, StatValues values, IDrawColor color)
        {
            //Image = System.Reflection.Assembly.GetCallingAssembly().GetName().Name + "name.png".ImgRes();

            this.StatName = title;
            this.StatProperties = properties.ToList();
            this.StatValues = values;
            this.Color = color;
        }

        public ClassStat(string title, IEnumerable<string> properties, StatValues values, ConsoleColor color)
        {
            //Image = System.Reflection.Assembly.GetCallingAssembly().GetName().Name + "name.png".ImgRes();

            this.StatName = title;
            this.StatProperties = properties.ToList();
            this.StatValues = values;
            this.Color = new DrawColor(color);
        }

        public ClassStat(string title, string property, StatValues values, ConsoleColor color)
        {
            //Image = System.Reflection.Assembly.GetCallingAssembly().GetName().Name + "name.png".ImgRes();

            this.StatName = title;
            this.StatProperties = property.InList();
            this.StatValues = values;
            this.Color = new DrawColor(color);
        }

        public int Group { get; set; }
    }
}