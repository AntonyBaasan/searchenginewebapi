using System.Collections.Generic;

namespace SearchEngineDomain
{
    public interface ISearchEngineService
    {
        int Index();

        List<object> Search(string q);
        
        List<object> GetAll();

        int ClearAll();
    }
}
