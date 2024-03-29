﻿using GameForum.Domain.Entities;

namespace GameForum.Application.Contracts.Persistence
{
    public interface IDepartmentRepository : IAsyncRepository<Department>
    {
    }
}