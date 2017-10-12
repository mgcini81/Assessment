using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Assessment.Repository.Entities;
using Assessment.Repository.Interfaces;
using Ninject;
using System.Reflection;

namespace Assessment.Repository.Data.Tests
{
    [TestClass()]
    public class CSVRepositoryTests
    {

        /// <summary>
        /// Tests if the data is parsed and loaded properly from the csv file
        /// </summary>
        [TestMethod()]
        public void GetAllTest()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            ICSVRepository<IPerson, IFrequency, IAddress> _repo = _kernel.Get<ICSVRepository<IPerson, IFrequency, IAddress>>();

            var data = _repo.GetAll();

            Assert.IsTrue(data.Count() > 0);
        }


        /// <summary>
        /// Tests whether the Assessment.Repository.Data.CSVRepository. GetNameFrequency() works as designed. In this case, we expect the first IPerson object to be Clive and the frequency count to be 2
        /// </summary>
        [TestMethod()]
        public void GetNameFrequencyTest()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            ICSVRepository<IPerson, IFrequency, IAddress> _repo = _kernel.Get<ICSVRepository<IPerson, IFrequency, IAddress>>();

            var frequencies = _repo.GetNameFrequency();

            Assert.IsTrue((frequencies.Count() > 0) && frequencies.FirstOrDefault().Value == "Clive" && frequencies.FirstOrDefault().Count == 2);
        }


        /// <summary>
        /// Tests whether the Assessment.Repository.Data.CSVRepository.GetSortedAddresses() works as designed. In this case, we expect the first top address to be '65 Ambling Way'
        /// </summary>
        [TestMethod()]
        public void GetSortedAddressesTest()
        {
            IKernel _kernel = new StandardKernel();
            _kernel.Load(Assembly.GetExecutingAssembly());
            ICSVRepository<IPerson, IFrequency, IAddress> _repo = _kernel.Get<ICSVRepository<IPerson, IFrequency, IAddress>>();

            var sortedAddresses = _repo.GetSortedAddresses();

            Assert.IsTrue((sortedAddresses.Count() > 0) && sortedAddresses.FirstOrDefault().FullAddress == "65 Ambling Way");
        }
       
    }
    public class NinjectBindings : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IPerson>().To<Person>();
            Bind<IAddress>().To<Address>();
            Bind<IFrequency>().To<Frequency>();
            Bind(typeof(IFileDataProvider<>)).To(typeof(CSVPersonDataProvider));
            Bind(typeof(ICSVRepository<,,>)).To(typeof(CSVRepository));
            Bind<IOutputWritter>().To<OutputWritter>();
        }
    }
}