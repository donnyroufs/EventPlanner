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

            var presenter = new Presenter();
            var ocassionRepository = new Mock<IOcassionRepository>();
            var createOcassionUseCase = new CreateOcassionUseCase<OcassionViewModel>(ocassionRepository.Object, presenter);
            var ocassionViewModel = new OcassionViewModel(description);

            ICreateOcassionDTO input = new CreateOcassionDTO(description);

            var result = await createOcassionUseCase.Execute(input);

            ocassionRepository.Verify(o => o.Save(It.IsAny<Ocassion>()), Times.Once);
            result.Should().BeOfType<OcassionViewModel>();
            result.Should().BeEquivalentTo(ocassionViewModel);
        }
    }

    internal class CreateOcassionDTO : ICreateOcassionDTO
    {
        public string Description { get; init; }

        public CreateOcassionDTO(string description)
        {
            Description = description;
        }
    }

    internal class OcassionViewModel
    {
        public string Description{ get; init; }

        public OcassionViewModel(string description)
        {
            Description = description;
        }
    }

    internal class Presenter : IPresenter<OcassionDTO, OcassionViewModel>
    {
        public OcassionViewModel Present(OcassionDTO data)
        {
            return new OcassionViewModel(data.Description);
        }
    }
}