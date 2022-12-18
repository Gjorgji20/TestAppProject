using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppProject.Model
{
    public class Client
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string BirthDate { get; set; }
    }
}
