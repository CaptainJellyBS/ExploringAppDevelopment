using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    public class PlaygroundEntry
    {
        public int ID { get; set; }
        public DateTime EnterTime { get; set; }
        public CreatureData Creature { get; set; }
    }
}
