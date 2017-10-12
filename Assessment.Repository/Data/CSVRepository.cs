using Assessment.Repository.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using Assessment.Repository.Entities;
using log4net;
using System.Reflection;

namespace Assessment.Repository.Data
{
    public class CSVRepository : ICSVRepository<IPerson, IFrequency,IAddress>
    {
        private IFileDataProvider<IPerson> _dataprovider;
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public CSVRepository(IFileDataProvider<IPerson> dtprovider)
        {
            _dataprovider = dtprovider;            
            _dataprovider.Load();
        }        
         /// <summary>
         /// Returns all the IPerson objects in the Data object of the data provider
         /// </summary>
         /// <returns></returns>
        public IEnumerable<IPerson> GetAll()
        {
            try
            {
                return _dataprovider.Data.ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return new List<Person>().AsEnumerable();
            }
        }

        /// <summary>
        /// Returns the single or default IPerson that meets the criteria specified in the predicate
        /// </summary>
        /// <param name="predicate">the filter</param>
        /// <returns></returns>
        public  IPerson SingleOrDefault(Expression<Func<IPerson, bool>> predicate)
        {
            try
            {
                return _dataprovider.Data.SingleOrDefault(predicate);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return new Person();
            }
        }

        /// <summary>
        /// Returns the frequency of the first and last names ordered by frequency descending and then alphabetically ascending
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IFrequency> GetNameFrequency()
        {
            try
            {
                return (from r in _dataprovider.Data
                        group r by r.FirstName into g
                        select new Frequency { Count = g.Count(), Value = g.Key }).OrderByDescending(o => o.Count).ThenBy(o => o.Value).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return new List<Frequency>().AsEnumerable();
            }
        }

        /// <summary>
        /// Returns Addresses sorted by street name
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAddress> GetSortedAddresses()
        {
            try
            {
                return _dataprovider.Data.Select(o => o.Address).OrderBy(o => o.StreetName).ToList();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                return new List<Address>().AsEnumerable();
            }
        }
    }
}
