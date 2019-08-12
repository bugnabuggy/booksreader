using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly ITranslationService _translationService;
        protected BaseController(ITranslationService translationService)
        {
            _translationService = translationService;
        }
    }
}