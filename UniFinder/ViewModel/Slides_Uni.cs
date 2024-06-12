using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniFinder.ViewModel
{
    public class Slides_Uni
    {
        public IEnumerable<UniversityTbl> university { get; set; }

        public IEnumerable<Campaigntbl> campaign { get; set; }
    }
}