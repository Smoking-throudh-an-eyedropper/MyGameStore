using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameStore.Entities
{
    internal class Announcement
    {
        public int idAnnouncement { get; set; }
        public string downloadAnnouncement { get; set; }
        public string nameGame { get; set; }
        public decimal priceAnnouncement { get; set; }
        public decimal discountAnnouncement { get; set; }
    }
}
