using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace EasyGlobalization.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Key { get; set; }     // ключ
        public string Value { get; set; }   // значение
        public int CulutureId { get; set; }
        [ForeignKey("CulutureId")]
        public Culture Culture { get; set; }
    }
}
