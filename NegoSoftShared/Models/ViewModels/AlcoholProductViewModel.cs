using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegoSoftShared.Models.ViewModels
{
    public class AlcoholProductViewModel
    {
        public Guid ProTypeId { get; set; }
        public float ApAlcoholVolume { get; set; }
        public int ApYear { get; set; }
    }
}
