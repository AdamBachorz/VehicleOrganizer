using VehicleOrganizer.Infrastructure.Abstractions.Validators;
using VehicleOrganizer.Infrastructure.Entities;
using VehicleOrganizer.Infrastructure.Validators;
using VehicleOrganizer.Infrastructure.Validators.Criteria;

namespace VehicleOrganizer.Infrastructure.Tests.Validators
{
    public class VehicleValidatorTests : BaseTests
    {
        private IValidator<Vehicle, VehicleValidationCriteria> _validator;

        [SetUp]
        public void Setup()
        {
            base.Setup();
            _validator = new VehicleValidator(null);
        }

        [Test]
        public void ShouldNotGetAnyValidationResults_Validate()
        {
            var input = new Vehicle
            {
                Name = _fixture.Create<string>(),
            };

            var criteria = new VehicleValidationCriteria
            {
                User = _fixture.Create<User>(),
            };
            var validationResult = _validator.Validate(input, criteria);

            Assert.That(validationResult, Is.Null.Or.Empty);
        }

        [Test]
        public void ShouldGetValidationResults_Validate()
        {
            var input = new Vehicle();

            var criteria = new VehicleValidationCriteria
            {
                User = _fixture.Create<User>(),
            };
            var validationResult = _validator.Validate(input, criteria).ToList();
            const int ExpectedValidationMessagesCount = 1;

            Assert.Multiple(() =>
            {
                Assert.That(validationResult, Is.Not.Null.Or.Empty);
                Assert.That(validationResult, Has.Count.EqualTo(ExpectedValidationMessagesCount));
            });
        }
    }
}
