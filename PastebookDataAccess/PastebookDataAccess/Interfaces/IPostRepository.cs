using PastebookEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PastebookDataAccess.Repositories
{
    public interface IPostRepository:IGenericTransactionDataRepository<PASTEBOOK_POST>
    {
    }
}
