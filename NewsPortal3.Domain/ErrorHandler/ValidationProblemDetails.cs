using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal3.Domain.ErrorHandler
{
    public class ValidationProblemDetails : ProblemDetails
    {
        public ICollection<ValidationError> ValidationErrors { get; set; }
    }
}
