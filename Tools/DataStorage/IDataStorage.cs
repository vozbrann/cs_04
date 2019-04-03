using System.Collections.Generic;
using cs_Vozbrannyi_04.Models;

namespace cs_Vozbrannyi_04.Tools.DataStorage
{
    internal interface IDataStorage
    {
        bool PersonExists(Person person);

        void AddPerson(Person person);

        void RemovePerson(Person person);

        void ApplyChanges();

        List<Person> PersonsList { get; }
    }
}