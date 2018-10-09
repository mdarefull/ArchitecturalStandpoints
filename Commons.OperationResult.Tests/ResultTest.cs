using System;
using FluentAssertions;
using Xunit;

namespace Commons.OperationResult.Tests
{
    public class ResultTest
    {
        [Fact]
        public void Success_Invoke_ReturnNewSuccessResult()
        {
            // Act:
            var result = Result.Success();

            // Assert:
            result.Should().BeOfType<SuccessResult>();
        }
        [Fact]
        public void SuccessOfT_Invoke_ReturnNewSuccessResultOfT()
        {
            // Arrange:
            var sample = new SuccessResult<int> { Value = 1 };

            // Act:
            var result = Result.Success(sample.Value);

            // Assert:
            result.Should().BeOfType<SuccessResult<int>>()
                  .And.BeEquivalentTo(sample);
        }

        [Fact]
        public void Failure_Invoke_ReturnNewFailureResult()
        {
            // Arrange:
            var sample = new FailureResult
            {
                ErrorTitle = "Some Title",
                ErrorDescription = "Some Description",
            };

            // Act:
            var result = Result.Failure(sample.ErrorTitle, sample.ErrorDescription);

            // Assert:
            result.Should().BeOfType<FailureResult>()
                  .And.BeEquivalentTo(sample);
        }
        [Fact]
        public void FailureOfT_Invoke_ReturnNewFailureResultOfT()
        {
            // Arrange:
            var sample = new FailureResult<object>
            {
                ErrorTitle = "Some Title",
                ErrorDescription = "Some Description",
            };

            // Act:
            var result = Result.Failure<object>(sample.ErrorTitle, sample.ErrorDescription);

            // Assert:
            result.Should().BeOfType<FailureResult<object>>()
                  .And.BeEquivalentTo(sample);

        }

        [Fact]
        public void Exception_Invoke_ReturnNewExceptionResult()
        {
            // Arrange:
            var sample = new ExceptionResult
            {
                InnerException = new Exception(),
            };

            // Act:
            var result = Result.Exception(sample.InnerException);

            // Assert:
            result.Should().BeOfType<ExceptionResult>()
                  .And.BeEquivalentTo(sample);
        }
        [Fact]
        public void ExceptionOfT_Invoke_ReturnNewExceptionResultOfT()
        {
            // Arrange:
            var sample = new ExceptionResult<object>
            {
                InnerException = new Exception(),
            };

            // Act:
            var result = Result.Exception<object>(sample.InnerException);

            // Assert:
            result.Should().BeOfType<ExceptionResult<object>>()
                  .And.BeEquivalentTo(sample);
        }

        [Fact]
        public void NotFoundOfT_Invoke_ReturnNewNotFoundResultOfT()
        {
            // Act:
            var result = Result.NotFound<object>();

            // Assert:
            result.Should().BeOfType<NotFoundResult<object>>();
        }
    }
}
