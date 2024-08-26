using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public interface IQuestionCategory<Category>
{
    Category Value();
    void SetValue(Category category);

}
