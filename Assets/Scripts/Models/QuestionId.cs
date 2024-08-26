using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class QuestionId : IId<string>
{
    private readonly string id;

    public QuestionId(string id)
    {
        this.id = id;
    }

    public string Value()
    {
        return id;
    }

    public string GetId()
    {
        return id;
    }

    public static QuestionId Generate()
    {
        return new QuestionId(Guid.NewGuid().ToString());
    }
}