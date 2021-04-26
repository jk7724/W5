using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class SetsVM
    {
        public int SetId { get; set; }
       
        public IEnumerable<SelectListItem> SetsList { get; set; }
    }
}
