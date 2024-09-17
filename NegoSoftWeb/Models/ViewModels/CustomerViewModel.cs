using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NegoSoftWeb.Models.ViewModels
{
    public class CustomerViewModel
    {
        public Guid CusId { get; set; }
        public string CusFirstName { get; set; }
        public string CusLastName { get; set; }
        public string CusEmail { get; set; }
        public string CusPhone { get; set; }
    }
}
