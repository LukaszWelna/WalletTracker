using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WalletTracker.Application.Income;
using WalletTracker.Domain.Entities;
using WalletTracker.Domain.Interfaces;

namespace WalletTracker.Application.Services
{
    public interface IIncomeService
    {
        public Task Create(IncomeDto incomeDto);
        public Task SeedDefaultCategoriesToUser(string userId); 
    }

    public class IncomeService : IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        private readonly IMapper _mapper;

        public IncomeService(IIncomeRepository incomeRepository, IMapper mapper)
        {
            _incomeRepository = incomeRepository;
            _mapper = mapper;
        }
        public async Task Create(IncomeDto incomeDto)
        {
            var income = _mapper.Map<Domain.Entities.Income>(incomeDto);

            await _incomeRepository.Create(income);
        }

        public async Task SeedDefaultCategoriesToUser(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new InvalidOperationException("User Id cannot be null or empty");
            }

            var incomeCategoriesAssignedToUserId = new List<IncomeCategoryAssignedToUser>();
            var incomeCategoriesDefault = await _incomeRepository.GetDefaultCategories();

            foreach(var category in incomeCategoriesDefault)
            {
                incomeCategoriesAssignedToUserId.Add(
                    new IncomeCategoryAssignedToUser()
                    {
                        UserId = userId,
                        Name = category.Name
                    });
            }

            await _incomeRepository.SeedDefaultCategoriesToUser(incomeCategoriesAssignedToUserId);
        }
    }
}
