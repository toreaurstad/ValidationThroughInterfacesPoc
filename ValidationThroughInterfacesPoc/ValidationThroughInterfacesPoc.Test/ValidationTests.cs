using FluentAssertions;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;

namespace ValidationThroughInterfacesPoc.Test
{

    [TestFixture]
    public class ValidationTests
    {

        [Test]
        public void ValidateReturnsExpectedForUndefinedCar()
        {
            Car car = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            Assert.Throws<ArgumentNullException>(() => { ValidatorSupportingInterfaces.Validate(car); });
        }

        [Test]
        public void ValidateReturnsExpectedForCarNotSpecifyingOneRequiredValidatingPropertyDefinedOnInterface()
        {
            Car car = new Car { Make = "Audi", WheelCount = 4 };
            // ReSharper disable once ExpressionIsAlwaysNull
            ValidatorSupportingInterfaces.Validate(car).Should()
                .NotBe(ValidationResult.Success, "Expected the object not be valid");
        }

        [Test]
        public void ValidateReturnsExpectedForCarNotSpecifyingTwoRequiredValidatingPropertyDefinedOnInterface()
        {
            Car car = new Car { WheelCount = 4, Model = "A" };
            // ReSharper disable once ExpressionIsAlwaysNull
            ValidatorSupportingInterfaces.Validate(car).Should()
                .NotBe(ValidationResult.Success, "Expected the object not be valid");
            ValidationResult vd = ValidatorSupportingInterfaces.Validate(car);
            Console.WriteLine(vd);
        }

        [Test]
        public void ValidateReturnsExpectedForCarFulfillingAllValidatingPropertyDefinedOnInterface()
        {
            Car car = new Car { Make = "Audi", Model = "A4", WheelCount = 4 };
            // ReSharper disable once ExpressionIsAlwaysNull
            ValidatorSupportingInterfaces.Validate(car).Should()
                .Be(ValidationResult.Success, "Expected the object be valid");
        }



    }
}
