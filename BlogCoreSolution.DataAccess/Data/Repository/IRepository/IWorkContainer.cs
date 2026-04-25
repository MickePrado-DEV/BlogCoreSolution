using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCoreSolution.DataAccess.Data.Repository.IRepository
{
    public interface IWorkContainer: IDisposable
    {
        ICategoryRepository CategoryRepository { get; }

        void Save();
    }
}
