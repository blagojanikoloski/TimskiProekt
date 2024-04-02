﻿using System.Collections.Generic;
using System.Threading.Tasks;
using webapi.Domain.Models;

namespace webapi.Domain.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<Request>> GetAllRequests();
        Task<IEnumerable<Request>> GetRequestsByClientIdOrWorkerId(string id);

        Task<Request> CreateRequest(Request request);
        Task<Request> GetRequestById(int id);
        Task UpdateRequest(Request request);
        Task DeleteRequest(int id);
    }
}