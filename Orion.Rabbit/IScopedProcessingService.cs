using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Rabbit
{
    public interface IScopedProcessingService<T> where T : class
    {
        Task HandleMessageAsync(T message);
    }

}
