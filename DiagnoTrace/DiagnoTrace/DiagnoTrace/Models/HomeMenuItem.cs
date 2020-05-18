using System;
using System.Collections.Generic;
using System.Text;

namespace DiagnoTrace.Models
{
    public enum MenuItemType
    {        
        Home,
        Hotspots,
        Questions,
        Settings,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }
}
