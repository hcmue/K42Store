using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace K42Store.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Mã khách hàng")]
        [Key]
        [Required(ErrorMessage = "*")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
        public string MaKh { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
