using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistence.ORMEntities
{
    //I could add navigation properties here, because it's a demo and we're only working with 3 records.
    //However, my experience is that in production scenarios navigation properties make things sping out
    //of control (I've literally faced situation where a single SELECT query would pull up half the DB).
    [Table("Customers")]
    public class Customer
    {
        [Key]
        [Column("Id")]
        public long? CustomerId { get; set; }
        [Column("Name")]
        public string Name { get; set; }

    }
}