using EntityFrameworkNet6.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkNet6.Domain
{
    public class Coach : BaseDomainObject
    {
        public string Name { get; set; }
        public int? TeamId { get; set; }//Nullable Because a coach can be teamless at some point
        public virtual Team Team { get; set; }
    }
}
