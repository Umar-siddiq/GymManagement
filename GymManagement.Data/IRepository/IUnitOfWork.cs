﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Data.IRepository
{
    public interface IUnitOfWork
    {
        IGymRepository Gym{ get; }
        IGymUserRepository GymUser {  get; }
        void Save();
    }
}
