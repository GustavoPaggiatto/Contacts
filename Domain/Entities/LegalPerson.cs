using System.Text.RegularExpressions;
using Domain.Results;

namespace Domain.Entities
{
    public class LegalPerson : Person
    {
        public string CompanyName { get; set; }
        public string TradeName { get; set; }

        public override object Clone()
        {
            LegalPerson clone = new LegalPerson()
            {
                Id = this.Id,
                Address = this.Address,
                AddressComplement = this.AddressComplement,
                AddressNumber = this.AddressNumber,
                CompanyName = this.CompanyName,
                Document = this.Document,
                TradeName = this.TradeName
            };

            return clone;
        }

        public override Result<bool> IsValid()
        {
            Result<bool> result = new Result<bool>();

            if (string.IsNullOrEmpty(this.Address.ZipCode))
                result.AddError("Zip Code was not informed.");

            Regex regex = new Regex("[^\\d-]");

            if (regex.IsMatch(this.Address.ZipCode))
                result.AddError("Zip code is invalid (ex.: 11111-111)");

            if (string.IsNullOrEmpty(this.Document))
                result.AddError("CNPJ was not informed.");

            if (string.IsNullOrEmpty(this.CompanyName))
                result.AddError("Company name was not informed.");

            if (string.IsNullOrEmpty(this.TradeName))
                result.AddError("Trade name was not informed.");

            regex = new Regex("[^\\d]");
            string doc = regex.Replace(this.Document, "");

            if (doc.Length != 14)
                result.AddError("CNPJ length is invalid.");
            else
            {
                int sum = 0;
                string tempDoc = doc.Substring(0, 12);
                int[] multiPlexOne = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiPlexTwo = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

                for (int i = 0; i < 12; i++)
                    sum += int.Parse(tempDoc[i].ToString()) * multiPlexOne[i];

                int remainder = sum % 11;

                if (remainder < 2)
                    remainder = 0;
                else
                    remainder = 11 - remainder;

                string digit = remainder.ToString();
                tempDoc = tempDoc + digit;
                sum = 0;

                for (int i = 0; i < 13; i++)
                    sum += int.Parse(tempDoc[i].ToString()) * multiPlexTwo[i];

                remainder = sum % 11;

                if (remainder < 2)
                    remainder = 0;
                else
                    remainder = 11 - remainder;

                digit += remainder;

                if (!this.Document.EndsWith(digit))
                    result.AddError("Invalid CNPJ check digits.");
            }

            return result;
        }
    }
}