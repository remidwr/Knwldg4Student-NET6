using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace Api.Tests
{
    public class ProgramShould
    {
        private readonly Mock<IConfiguration> _mockConfiguration;

        public ProgramShould()
        {
            _mockConfiguration = new Mock<IConfiguration>();
        }

        [Fact]
        public void RegisterDependencyCorrectly()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}