using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchOdds.Domain.Common
{
    public interface IHasKey<T>
    {
        T ID { get; set; }
    }
}
