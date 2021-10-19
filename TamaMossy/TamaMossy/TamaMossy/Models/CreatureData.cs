using System;
using System.Collections.Generic;
using System.Text;

namespace TamaMossy.Models
{
    public class CreatureData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PlayerName { get; set; }
        public float Hunger { get; set; }
        public float Thirst { get; set; }
        public float Loneliness { get; set; }
        public float Boredom { get; set; }
        public float Stimulated { get; set; }
        public float Tired { get; set; }
    }
}
