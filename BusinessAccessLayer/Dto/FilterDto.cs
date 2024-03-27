using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer.Dto
{
    public class FilterDto
    {
        public string? Type { get; set; }
        public string? Breed { get; set; }
        public string? Sex { get; set; }
        //Pagination
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
