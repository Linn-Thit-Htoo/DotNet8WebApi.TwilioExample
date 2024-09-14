using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNet8WebApi.TwilioExample.Entities
{
    [Table("Tbl_User")]
    public class Tbl_User
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool DeleteFlag { get; set; }
    }
}
