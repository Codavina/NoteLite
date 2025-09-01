using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteLite.Services
{
    public interface IUnsavedChangesService
    {
        bool IsModified { get; }
        void MarkModified();
        void Reset();
        bool ConfirmSaveIfNeeded(Func<bool> onSave);

    }
}
