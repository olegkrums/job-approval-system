using System;
using System.Collections.Generic;

namespace JobApprovalService.DataAccess
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(Guid Id);
    }
}