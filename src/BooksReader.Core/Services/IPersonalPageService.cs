﻿using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;

namespace BooksReader.Core.Services
{
    public interface IPersonalPageService : ICRUDOperatonService<PersonalPage>, IValidator<PersonalPage>
    {

    }
}
