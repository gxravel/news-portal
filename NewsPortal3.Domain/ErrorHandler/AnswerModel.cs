using System;
using System.Collections.Generic;
using System.Text;

namespace NewsPortal3.Domain.ErrorHandler
{
#nullable enable
    public class AnswerModel
    {
        public object? Data { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }

        public AnswerModel()
        {

        }
        public AnswerModel(object data)
        {
            Data = data;
        }
        public AnswerModel(ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }
    }
#nullable disable
}
