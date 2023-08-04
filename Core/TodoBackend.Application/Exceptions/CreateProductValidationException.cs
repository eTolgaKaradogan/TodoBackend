using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoBackend.Application.Exceptions
{
    public class CreateProductValidationException : Exception
    {
        public CreateProductValidationException() : base("Product validation failed!")
        {

        }

        public CreateProductValidationException(string? message) : base (message)
        {

        }

        public CreateProductValidationException(string? message, Exception? innerException) : base(message, innerException)
        {

        }
    }
}
