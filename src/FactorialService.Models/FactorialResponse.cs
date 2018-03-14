using System.Collections.Generic;
 
namespace FactorialService.Models
{
    public class FactorialResponse
    {
        public int Factorial { get; set; }
        public bool Success { get; set; }
        public IEnumerable<dynamic> Errors { get; set; }
    }
}