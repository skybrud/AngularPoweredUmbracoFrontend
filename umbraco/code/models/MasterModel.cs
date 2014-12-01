using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code.models
{
    public class MasterModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AngularTemplateUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
