using System;


namespace NoteLite.Services
{
    public interface IFindService
    {
        bool FindNext(string searchText, bool matchCase = false);
        void Reset();
    }
}
