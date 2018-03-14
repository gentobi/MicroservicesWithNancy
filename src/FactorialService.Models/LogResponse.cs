using System.Collections.Generic;

namespace FactorialService.Models
{
     public class LogResponse
    {
        public bool Success { get; set; }
        public IEnumerable<dynamic> Errors { get; set; }
    }
}