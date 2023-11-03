using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiLabs.Dal.Entities
{
    [Table(nameof(LoggingInfo))]
    public partial class LoggingInfo : object
    {
        [Key]
        public int Id { get; set; } = default!;

        [MaxLength(100, ErrorMessage = "Неверное значение названия конечной точки")]
        public string MethodName { get; set; } = default!;

        [MaxLength(100, ErrorMessage = "Неверное значение пользовательской информации")]
        public string UserInfo { get; set; } = default!;
        public DateTime DateTime { get; set; } = default!;
    }
}
