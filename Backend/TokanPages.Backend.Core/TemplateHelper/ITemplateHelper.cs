﻿using System.Collections.Generic;
using TokanPages.Backend.Core.TemplateHelper.Model;

namespace TokanPages.Backend.Core.TemplateHelper
{

    public interface ITemplateHelper
    {
        string MakeBody(string ATemplate, List<Item> AValueTag);
    }

}
