﻿using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Abstractions.Validators.Criteria;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Repositories;
using VehicleOrganizer.Infrastructure.Validators;

namespace VehicleOrganizer.Infrastructure.Tests.Validators
{
    public class VehicleValidatorTests : BaseDataBaseTests
    {
        private IValidator<Vehicle, VehicleValidationCriteria> _validator;

        [SetUp]
        public void Setup()
        {
            base.Setup();
            _validator = new VehicleValidator(new VehicleRepository(_db));
        }

        [Test]
        public void ShouldNotGetAnyValidationResults_Validate()
        {
            var input = new Vehicle
            {
                Name = _fixture.Create<string>(),
            };

            var validationResult = _validator.Validate(input);

            Assert.That(validationResult, Is.Null.Or.Empty);
        }

        [Test]
        public void ShouldGetValidationResults_Validate()
        {
            var input = new Vehicle();

            var validationResult = _validator.Validate(input).ToList();
            const int ExpectedValidationMessagesCount = 1;

            Assert.Multiple(() =>
            {
                Assert.That(validationResult, Is.Not.Null.Or.Empty);
                Assert.That(validationResult, Has.Count.EqualTo(ExpectedValidationMessagesCount));
            });
        }
    }
}
