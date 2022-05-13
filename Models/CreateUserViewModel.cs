using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Test.Models
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "Введите имя")]
        [MaxLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]

        public string Name { get; set; }

        [Range(1,100,ErrorMessage = "Недопустимый возраст")]
        public int Age { get; set; }
    }
}
