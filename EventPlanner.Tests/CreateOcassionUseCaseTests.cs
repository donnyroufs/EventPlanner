using System;
using System.Threading.Tasks;
using EventPlanner.Application;
using EventPlanner.Domain;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace EventPlanner.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public async Task CreatesAnOcassion()
        {
            const string description = "my ocassion";
            var ocassionRepository = new Mock<IOcassionRepository>();
            CreateOcassionUseCase createOcassionUseCase = new CreateOcassionUseCase(ocassionRepository.Object);
            Ocassion ocassion = new(description);
            OcassionDTO ocassionDTO = new(description);

            var result = await createOcassionUseCase.Execute(ocassion);

            ocassionRepository.Verify(o => o.Save(ocassion), Times.Once);
            result.Should().BeEquivalentTo(ocassionDTO);
        }
    }
}