using System.Collections.Generic;

namespace SearchEngineDomain.Interfaces
{
    public interface ISearchEngineService<T>
    {
        long Index(List<T> docs);

        void DeleteIndex();

        void CreateIndex();

        List<T> Search(string q);
        
        List<T> GetAll();

        long DeleteAll();

        long DeleteByIds(List<long> ids);
    }
}
