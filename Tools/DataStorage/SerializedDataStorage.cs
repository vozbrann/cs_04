using System;
using System.Collections.Generic;
using System.IO;
using cs_Vozbrannyi_04.Enums;
using cs_Vozbrannyi_04.Models;
using cs_Vozbrannyi_04.Tools.Managers;

namespace cs_Vozbrannyi_04.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private readonly List<Person> _users;

        internal SerializedDataStorage()
        {
            try
            {
                _users = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _users = new List<Person>();
                Random random = new Random();

                string[] names = Enum.GetNames(typeof(Names));
                string[] surnames = Enum.GetNames(typeof(Surnames));

                for (int i = 0; i < 50; ++i)
                {
                    int currName = random.Next(0, 10);
                    int currSurname = random.Next(0, 10);
                    DateTime birthday = new DateTime(random.Next(1885, 2019), random.Next(1, 12), random.Next(1, 28));
                    string email = surnames[currSurname] + "@test.com";

                    Person tmp = new Person(names[currName], surnames[currSurname], email, birthday);

                    _users.Add(tmp);
                }

                SaveChanges();
            }
        }

        public bool PersonExists(Person person)
        {
            return _users.Contains(person);
        }

        public void AddPerson(Person person)
        {
            _users.Add(person);
            SaveChanges();
        }

        public void RemovePerson(Person person)
        {
            _users.Remove(person);
            SaveChanges();
        }

        public void ApplyChanges()
        {
            SaveChanges();
        }

        public List<Person> PersonsList
        {
            get { return _users; }
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(_users, FileFolderHelper.StorageFilePath);
        }
    }
}