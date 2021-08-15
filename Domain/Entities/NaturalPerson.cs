using System;
using System.Text.RegularExpressions;
using Domain.Results;
using Domain.VOs;

namespace Domain.Entities
{
    public class NaturalPerson : Person
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        public override object Clone()
        {
            NaturalPerson clone = new NaturalPerson()
            {
                Id = this.Id,
                Address = this.Address,
                AddressComplement = this.AddressComplement,
                AddressNumber = this.AddressNumber,
                BirthDate = this.BirthDate,
                Document = this.Document,
                Gender = this.Gender,
                Name = this.Name
            };

            return clone;
        }

        public override Result<bool> IsValid()
        {
            var result = new Result<bool>();

            if (string.IsNullOrEmpty(this.Address.ZipCode))
                result.AddError("Zip Code was not informed.");

            Regex regex = new Regex("[^\\d-]");

            if (this.Address.ZipCode != null &&
                    regex.IsMatch(this.Address.ZipCode))
                result.AddError("Zip code is invalid (ex.: 11111-111)");

            if (string.IsNullOrEmpty(this.Name))
                result.AddError("Name was not informed.");

            if (this.BirthDate.Equals(DateTime.MinValue))
                result.AddError("Birthdate was not informed.");

            if (this.BirthDate.Date.Subtract(DateTime.Now.Date).Days > 0)
                result.AddError("Birthdate cannot be great than today.");

            if (string.IsNullOrEmpty(this.Document))
                result.AddError("CPF was not informed.");

            regex = new Regex("[^\\d]");
            string doc = regex.Replace(this.Document, "");

            if (doc.Length != 11)
                result.AddError("Invalid CPF (length diferent than 11).");
            else
            {
                string tempDoc = doc.Substring(0, 9);
                int sum = 0;
                int[] multiPlexOne = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiPlexTwo = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

                for (int i = 0; i < 9; i++)
                    sum += int.Parse(tempDoc[i].ToString()) * multiPlexOne[i];

                int remainder = sum % 11;

                if (remainder < 2)
                    remainder = 0;
                else
                    remainder = 11 - remainder;

                string digit = remainder.ToString();
                tempDoc = tempDoc + digit;
                sum = 0;

                for (int i = 0; i < 10; i++)
                    sum += int.Parse(tempDoc[i].ToString()) * multiPlexTwo[i];

                remainder = sum % 11;

                if (remainder < 2)
                    remainder = 0;
                else
                    remainder = 11 - remainder;

                digit += remainder;

                if (!this.Document.EndsWith(digit))
                    result.AddError("Invalid check CPF digits.");
            }

            return result;
        }
    }
}