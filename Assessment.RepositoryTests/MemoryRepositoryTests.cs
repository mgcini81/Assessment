using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assessment.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assessment.Repository.Entities;
using Assessment.Repository.Interfaces;

namespace Assessment.Repository.Data.Tests
{
    [TestClass()]
    public class MemoryRepositoryTests
    {
        [TestMethod()]
        public void GetAllMemoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetNameFrequencyMemoryTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetSortedAddressesMemoryTest()
        {
            Assert.Fail();
        }
    }
    public class TestDataProvider : IFileDataProvider<IPerson>
    {
        public IQueryable<IPerson> Data { get; set; }

        public void Load()
        {
            List<Person> persons = new List<Person>();

            persons.Add(new Person
            {
                FirstName = "Alert",
                LastName = "Zulu",
                Address = new Address { FullAddress = "32 West Street" },
                PhoneNumber = "0314598759"
            });

            persons.Add(new Person
            {
                FirstName = "Sipho",
                LastName = "Zulu",
                Address = new Address { FullAddress = "12 Marchison Street" },
                PhoneNumber = "0314598259"
            });

            persons.Add(new Person
            {
                FirstName = "Jama",
                LastName = "Xulu",
                Address = new Address { FullAddress = "45 Church Street" },
                PhoneNumber = "0314593259"
            });

            persons.Add(new Person
            {
                FirstName = "Peter",
                LastName = "Xulu",
                Address = new Address { FullAddress = "12 Sixth Road" },
                PhoneNumber = "0319593259"
            });

            persons.Add(new Person
            {
                FirstName = "Xolani",
                LastName = "Zulu",
                Address = new Address { FullAddress = "45 Eighth Road" },
                PhoneNumber = "0314593559"
            });

            persons.Add(new Person
            {
                FirstName = "Thando",
                LastName = "Mpanza",
                Address = new Address { FullAddress = "17 First Road" },
                PhoneNumber = "0314543259"
            });

            persons.Add(new Person
            {
                FirstName = "Jane",
                LastName = "Mills",
                Address = new Address { FullAddress = "34 Second Road" },
                PhoneNumber = "0314543259"
            });

            this.Data = persons.AsQueryable();

        }
    }

    public class NinjectTestDataBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {           
            Bind(typeof(IFileDataProvider<>)).To(typeof(TestDataProvider));
            Bind(typeof(ICSVRepository<,,>)).To(typeof(CSVRepository));
            Bind<IOutputWritter>().To<OutputWritter>();
        }
    }
}