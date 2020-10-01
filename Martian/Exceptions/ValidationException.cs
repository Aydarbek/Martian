using System;
using System.Collections.Generic;
using System.Text;

namespace Martian.Exceptions
{
    class ValidationException : Exception
    {
        internal ValidationException(string message) : base(message)
        {

        }
    }
}
