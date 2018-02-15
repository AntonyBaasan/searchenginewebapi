using System.Collections.Generic;
using ElasticSearchEngineService;
using ElasticSearchEngineService.Models;
using Xunit;

namespace SearchEngineTests
{
    public class ElasticTests
    {
        private ElasticServiceImpl<ElasticFileInfo> service;

        public ElasticTests()
        {
            service = new ElasticServiceImpl<ElasticFileInfo>(SettingsUtils.GetConnectionString("ElasticSearchEngine"), "test_index");
        }

        [Fact]
        public void IndexTest()
        {
            var indexedAmount = service.Index(GetMockData());

            Assert.Equal(2, indexedAmount);
        }

        [Fact]
        public void QueryByStringTest1()
        {
            service.Index(GetMockData());

            var res = service.Search("what");

            Assert.Single(res);
        }

        [Fact]
        public void QueryByStringTest2()
        {
            service.Index(GetMockData());

            var res = service.Search("hello");

            Assert.Single(res);
        }


        [Fact]
        public void GetAllTest()
        {
            service.Index(GetMockData());

            var all = service.GetAll();
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public void DeleteAllTest()
        {
            service.Index(GetMockData());

            long deletedAmount = service.DeleteAll();

            Assert.Equal(2, deletedAmount);
        }

        [Fact]
        public void DeleteIndexTest()
        {
            service.Index(GetMockData());

            service.DeleteIndex();

            var all = service.GetAll();

            Assert.Empty(all);
        }

        [Fact]
        public void CreateIndexTest()
        {
            service.DeleteIndex();

            service.CreateIndex();

            service.Index(GetMockData());

            var all = service.GetAll();

            Assert.Empty(all);
        }


        private List<ElasticFileInfo> GetMockData()
        {
            var f1 = new ElasticFileInfo { Id = 0, Name = "TestFile1", Description = "What are you looking!", CreatedBy = "Rodney Jones" };
            var f2 = new ElasticFileInfo { Id = 1, Name = "TestFile2", Description = "Hello world my friends", CreatedBy = "Antony Baasandorj" };
            return new List<ElasticFileInfo> { f1, f2 };
        }

    }
}