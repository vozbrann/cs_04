using System;

namespace cs_Vozbrannyi_04.Tools.Exceptions
{
    class PersonNotBornException : ArgumentException
    {
        public PersonNotBornException(string message)
            : base(message)
        { }
    }
}
