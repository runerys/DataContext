using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataContext.Core
{
    public interface IServiceLocator
    {
        T Resolve<T>();
        void RebindToConstant<T>(T instance) where T : class;
    }
}
