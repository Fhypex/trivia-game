using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IQuestionOption<Id, Option>
{
    Id GetId();
    Option GetValue();
}