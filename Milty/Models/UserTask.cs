using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Milty.Models
{
    public class UserTask
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Name { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Автор")]
        public string User { get; set; }
        [Display(Name = "Тег")]
        public string Tag { get; set; }
        [Display(Name = "Раздел")]
        public string Repository { get; set; }
    }
}