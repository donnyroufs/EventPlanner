using System.Threading.Tasks;
using EventPlanner.Domain;

namespace EventPlanner.Application
{
    public class CreateOcassionUseCase
    {
        private IOcassionRepository _ocassionRepository{ get; }

        public CreateOcassionUseCase(IOcassionRepository ocassionRepository)
        {
            _ocassionRepository = ocassionRepository;
        }

        public async Task<OcassionDTO> Execute(Ocassion ocassion)
        {
            await _ocassionRepository.Save(ocassion);


            return new OcassionDTO(ocassion.Description);
        }
    }
}