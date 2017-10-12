//using Assessment.Repository.Interfaces;
using Assessment.Repository.Interfaces;
using System;
using System.Linq;

namespace Assessment.Repository.Entities
{
    public class Address: IAddress
    {
        public int StreetNumber { get { return GetStreetNumber(); } }
        public string StreetName { get { return GetStreetName(); } }
        public string FullAddress {  get; set;}

        private int GetStreetNumber()
        {
            int strNumber = 0;
            string[] tokens = this.FullAddress.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                if (int.TryParse(tokens[i], out strNumber))
                {
                    break;
                }
            }

            return strNumber;
        }


        private string GetStreetName()
        {
            string strName = string.Empty;
            int num, numIndex = int.MinValue;
            bool foundNumber = false;
            string[] tokens = this.FullAddress.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                if (int.TryParse(tokens[i], out num))
                {                   
                    numIndex = i;
                    foundNumber = true;
                }
            }
            if (foundNumber)
            {
                strName = string.Join(" ", tokens.Where((s, i) => i != numIndex).ToArray());
            }
            else
            {
                strName = this.FullAddress;
            }
            return strName;
        }
    }
}