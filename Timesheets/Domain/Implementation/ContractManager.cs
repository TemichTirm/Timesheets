using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Implementation
{
    public class ContractManager: IContractManager
    {
        private readonly IContractRepo _contractRepo;

        public ContractManager(IContractRepo contractRepo)
        {
            _contractRepo = contractRepo;
        }

        public async Task<bool?> CheckContractIsActive(Guid id)
        {
            return await _contractRepo.CheckContractIsActive(id);
        }

        public async Task<Guid> Create(ContractCreateRequest contractRequest)
        {
            var newContract = new Contract
            {
                Id = Guid.NewGuid(),
                Title = contractRequest.Title,
                DateStart = contractRequest.DateStart,
                DateEnd = contractRequest.DateEnd,
                Description = contractRequest.Description,
                IsDeleted = false
            };
            await _contractRepo.Add(newContract);
            return newContract.Id;
        }

        public async Task<Contract> GetItem(Guid id)
        {
            return await _contractRepo.GetItem(id);
        }

        public async Task<IEnumerable<Contract>> GetItems()
        {
            return await _contractRepo.GetItems();
        }

        public async Task<bool> Update(Guid id, ContractCreateRequest contractRequest)
        {
            var contract = await _contractRepo.GetItem(id);
            if (contract == null)
            {
                return false;
            }
            var newContract = new Contract
            {
                Id = id,
                Title = contractRequest.Title,
                DateStart = contractRequest.DateStart,
                DateEnd = contractRequest.DateEnd,
                Description = contractRequest.Description,
                IsDeleted = contract.IsDeleted
            };

            await _contractRepo.Update(newContract);
            return true;
        }
    }
}