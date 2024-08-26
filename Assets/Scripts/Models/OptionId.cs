using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class OptionId : IId<int>
{
    private readonly int id;

    public OptionId(int id)
    {
        this.id = id;
    }

    public int Value()
    {
        return id;
    }

    public static OptionId Generate()
    {   
        int id = 0;
        string guid = Guid.NewGuid().ToString();
        for(int i = 0; i < guid.Length; i++)
        {
            id += guid[i];
        }
        return new OptionId(id);
    }
}