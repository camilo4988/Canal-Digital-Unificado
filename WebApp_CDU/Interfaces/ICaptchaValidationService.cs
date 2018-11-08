using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp_CDU.Interfaces
{
    public interface ICaptchaValidationService
    {
        bool Validate(string response);
    }
}