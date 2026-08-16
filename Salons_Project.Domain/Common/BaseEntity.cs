using System;
using System.Collections.Generic;
using System.Text;

namespace Salons_Project.Domain.Common;

public class BaseEntity
{ 
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

}
