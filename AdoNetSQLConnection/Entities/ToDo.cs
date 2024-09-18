using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetSQLConnection.Entities
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
