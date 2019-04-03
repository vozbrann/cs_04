using System;

namespace cs_Vozbrannyi_04.Tools.Exceptions
{
    class InvalidEmailException : ArgumentException
    {
        public InvalidEmailException(string message)
            : base(message)
        { }
    }
}
