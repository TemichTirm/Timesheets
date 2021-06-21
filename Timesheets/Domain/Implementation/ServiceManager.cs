using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Timesheets.Data.Interfaces;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Implementation
{
    public class ServiceManager : IServiceManager
    {
        private readonly IServiceRepo _serviceRepo;
        public ServiceManager(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }
        public async Task<Guid?> Create(ServiceCreateRequest request)
        {
            // Проверка, используется ли уже имя сервиса.
            if (await _serviceRepo.CheckServiceExist(request.Name))
            {
                return null;
            }

            // Если имя сервиса уникальное - регистрация нового сервиса.
            var newService = new Service
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            await _serviceRepo.Add(newService);
            return newService.Id;
        }

        public async Task<Service> GetItem(Guid id)
        {
            return await _serviceRepo.GetItem(id);
        }

        public async Task<IEnumerable<Service>> GetItems()
        {
            return await _serviceRepo.GetItems();
        }

        public async Task<bool> Update(Guid id, ServiceCreateRequest serviceRequest)
        {
            var service = await _serviceRepo.GetItem(id);
            if (service == null)
            {
                return false;
            }
            service.Name = serviceRequest.Name;
            await _serviceRepo.Update(service);
            return true;
        }
    }
}
