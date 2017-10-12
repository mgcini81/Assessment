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
