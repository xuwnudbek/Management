using System.Collections.Generic;
using System.Data.SqlTypes;
using Management.Domain.Models;

namespace Management.Infrastructure.Data;

public class DbContext
{
    public List<Student> Students { get; set; }

    public DbContext()
    {
        this.Students = new List<Student>();
    }
}
