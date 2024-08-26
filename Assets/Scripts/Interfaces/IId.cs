using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IId<T>
{
    public T Value();
}
