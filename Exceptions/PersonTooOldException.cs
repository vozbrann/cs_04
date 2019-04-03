using System;

namespace cs_Vozbrannyi_04.Tools.Exceptions
{
    class PersonTooOldException : ArgumentException
    {
        public PersonTooOldException(string message)
            : base(message)
        { }
    }
}
