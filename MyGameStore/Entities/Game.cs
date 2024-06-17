using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGameStore.Entities
{
    internal class Game
    {
        internal int idGame { get; set; }
        internal string nameGame { get; set; }
        internal DateTime releaseDateGame { get; set; }
        internal decimal sizeGame { get; set; }
        internal string skinGame { get; set; }
        internal string descriptionGame { get; set; }
        internal string nameCampaign { get; set; }
        internal string nameTage { get; set; }
        internal string nameGenre { get; set; }
        internal string nameLanguage { get; set; }
        internal decimal priceAnnouncement { get; set; }
        internal decimal discountAnnouncement { get; set; }
    }
}
