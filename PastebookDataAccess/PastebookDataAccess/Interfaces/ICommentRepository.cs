using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PastebookEF;

namespace PastebookDataAccess.Repositories
{
    public interface ICommentRepository: IGenericTransactionDataRepository <PASTEBOOK_COMMENT>
    {
    }
}
