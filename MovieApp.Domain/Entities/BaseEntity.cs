using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Domain.Entities
{
    public abstract class BaseEntity<T>
    {
        public T Id { get; set; }
        public virtual DateTime Created { get; set; } = DateTime.UtcNow;
        public virtual int? CreatorUserId { get; set; }

        public BaseEntity(T id = default!)
        {
            Id = id;
        }
    }
}
