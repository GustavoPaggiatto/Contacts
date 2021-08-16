using System;
using System.Linq;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Test
{
    public class PersonServiceTest
    {
        readonly ServiceCollection _services;

        public PersonServiceTest()
        {
            this._services = new ServiceCollection();

            this._services.AddLogging();

            CrossCutting.Register.Set(this._services);
        }

        [Fact]
        [Trait("Person (Natural)", "Insert Natural Person without ZipCode")]
        public void InsertNaturalPersonWithoutZipCode()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new NaturalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, null),
                AddressNumber = 0,
                Document = "430.478.798-50",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo H. Cechini Paggiatto"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Zip Code was not informed.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Natural)", "Insert Natural Person with ZipCode invalid (alpha numeric chars)")]
        public void InsertNaturalPersonWithZipCodeInvalidAlphaNumericChars()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new NaturalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "Gustavo03208-010"),
                AddressNumber = 0,
                Document = "430.478.798-50",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo H. Cechini Paggiatto"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Zip code is invalid (ex.: 11111-111)", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Natural)", "Insert Natural Person without name")]
        public void InsertNaturalPersonWithoutName()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new NaturalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "430.478.798-50",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = null
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Name was not informed.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Natural)", "Insert Natural Person with birthdate in future")]
        public void InsertNaturalPersonWithBirthdateInFuture()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new NaturalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "430.478.798-50",
                BirthDate = DateTime.Now.AddDays(1),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo Henrique Cechini Paggiatto"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Birthdate cannot be great than today.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Natural)", "Insert Natural Person without CPF")]
        public void InsertNaturalPersonWithoutCpf()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new NaturalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo Henrique Cechini Paggiatto"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(2, result.Errors.Count);
            Assert.Equal("CPF was not informed.", result.Errors.First());
            Assert.Equal("Invalid CPF (length diferent than 11).", result.Errors.Last());
        }

        [Fact]
        [Trait("Person (Natural)", "Insert Natural Person with invalid CPF check digits")]
        public void InsertNaturalPersonWithInvalidCpfCheckDigits()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new NaturalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "430.478.798-49",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo Henrique Cechini Paggiatto"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Invalid check CPF digits.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Natural)", "Insert Valid Natural Person")]
        public void InsertNaturalPersonValid()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new NaturalPerson()
            {
                Address = new Domain.VOs.Address("Brazil", "SP", "São Paulo", "Rua Justiniano (V.A.)", "03208-010"),
                AddressNumber = 293,
                Document = "430.478.798-50",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo Henrique Cechini Paggiatto"
            });

            int personsLength = personService.Get().Content.Count();

            Assert.Equal(false, result.HasError);
            Assert.Null(result.Errors);
            Assert.Equal(1, personsLength);
        }

        [Fact]
        [Trait("Person (Natural)", "Update Natural Person without Id")]
        public void UpdateNaturalPersonWithoutId()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Update(new NaturalPerson()
            {
                Address = new Domain.VOs.Address("Brazil", "SP", "São Paulo", "Rua Justiniano (V.A.)", "03208-010"),
                AddressNumber = 293,
                Document = "430.478.798-50",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo Henrique Cechini Paggiatto",
                Id = 0
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Register not found.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Natural)", "Delete Natural Person without Id")]
        public void DeleteNaturalPersonWithoutId()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Delete(new NaturalPerson()
            {
                Address = new Domain.VOs.Address("Brazil", "SP", "São Paulo", "Rua Justiniano (V.A.)", "03208-010"),
                AddressNumber = 293,
                Document = "430.478.798-50",
                BirthDate = new DateTime(1994, 7, 31),
                Gender = Domain.VOs.Gender.Male,
                Name = "Gustavo Henrique Cechini Paggiatto",
                Id = -1
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Register not found.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Legal)", "Insert Legal Person without ZipCode")]
        public void InsertLegalPersonWithoutZipCode()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new LegalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, null),
                AddressNumber = 0,
                Document = "47.012.193/0001-64",
                CompanyName = "CGode",
                TradeName = "Gus"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Zip Code was not informed.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Legal)", "Insert Legal Person with invalid ZipCode")]
        public void InsertLegalPersonWithInvalidZipCode()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new LegalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "Gus03208tavo010"),
                AddressNumber = 0,
                Document = "47.012.193/0001-64",
                CompanyName = "CGode",
                TradeName = "Gus"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Zip code is invalid (ex.: 11111-111)", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Legal)", "Insert Legal Person without CNPJ")]
        public void InsertLegalPersonWithoutCnpj()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new LegalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "",
                CompanyName = "CGode",
                TradeName = "Gus"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(2, result.Errors.Count);
            Assert.Equal("CNPJ was not informed.", result.Errors.First());
            Assert.Equal("CNPJ length is invalid.", result.Errors.Last());
        }

        [Fact]
        [Trait("Person (Legal)", "Insert Legal Person without Company Name")]
        public void InsertLegalPersonWithoutCompanyName()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new LegalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "47.012.193/0001-64",
                CompanyName = null,
                TradeName = "Gus"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Company name was not informed.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Legal)", "Insert Legal Person without Trade Name")]
        public void InsertLegalPersonWithoutTradeName()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new LegalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "47.012.193/0001-64",
                CompanyName = "GCode",
                TradeName = ""
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Trade name was not informed.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Legal)", "Insert Legal Person with invalid CNPJ check digits")]
        public void InsertLegalPersonWithInvalidCnpjCheckDigits()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new LegalPerson()
            {
                Address = new Domain.VOs.Address(null, null, null, null, "03208-010"),
                AddressNumber = 0,
                Document = "47.012.193/0001-70",
                CompanyName = "GCode",
                TradeName = "Gus"
            });

            Assert.Equal(true, result.HasError);
            Assert.Equal(1, result.Errors.Count);
            Assert.Equal("Invalid CNPJ check digits.", result.Errors.First());
        }

        [Fact]
        [Trait("Person (Legal)", "Insert Legal Person valid")]
        public void InsertLegalPersonValid()
        {
            var personService = this._services.BuildServiceProvider().GetService<IPersonService>();

            var result = personService.Insert(new LegalPerson()
            {
                Address = new Domain.VOs.Address("Brazil", "SP", "São Paulo", "Rua Justiniano", "03208-010"),
                AddressNumber = 293,
                Document = "47.012.193/0001-64",
                CompanyName = "GCode",
                TradeName = "Gus"
            });

            int personsLength = personService.Get().Content.Count();

            Assert.False(result.HasError);
            Assert.Null(result.Errors);
            Assert.Equal(1, personsLength);
        }
    }
}
