using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteLite.Services
{
    public class UnsavedChangesManager:IUnsavedChangesService
    {
        private bool _isModified = false;
        public bool IsModified => _isModified;
        public void MarkModified() => _isModified = true;
        public void Reset() => _isModified = false;

        ///<summary>
        /// Confirms save if changes were made
        ///</summary>
        ///<param name="onSave">Callback to excute saving logic</param>
        ///<returns>true = continue, false = cancel action</returns>

        public bool ConfirmSaveIfNeeded(Func<bool> onSave)
        {
            if (!_isModified)
                return true;

            var result = MessageBox.Show("Do you want to save changes", "Unsaved Changes",

                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Cancel)
                return false;

            if (result == DialogResult.Yes)
            {
                return onSave();
            }
            return true;
        }

    }
}
