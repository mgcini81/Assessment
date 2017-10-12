using Assessment.Repository.Entities;
using Assessment.Repository.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Assessment.Repository.Data
{
    public class CSVPersonDataProvider : IFileDataProvider<IPerson>
    {
        public IQueryable<IPerson> Data { get; set; }
        private readonly string _fileName = "data.csv";      
        private static readonly ILog _logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Loads the data from the csv file. The data is stored in the Data property
        /// </summary>
        public void Load()
        {
            try
            {
                this.Data = (from l in File.ReadAllLines(_fileName).Skip(1)
                             let c = l.Split(',')
                             select new Person
                             {
                                 FirstName = c[0],
                                 LastName = c[1],
                                 Address = new Address { FullAddress = c[2] },
                                 PhoneNumber = c[3]
                             }).AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                _logger.Error(ex.StackTrace);
                this.Data =  new List<Person>().AsQueryable();
            }
        }
    }
}