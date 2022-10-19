﻿using currus.Models;

namespace currus.Repository
{
    public interface IUserDbRepository: IDbRepository<User>
    {
        void DeleteById(int id);
    }
}