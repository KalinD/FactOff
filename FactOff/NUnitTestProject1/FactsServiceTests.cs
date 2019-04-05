using FactOff.Models.DB;
using FactOff.Services;
using FactOff.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    [TestFixture]
    public class FactsServiceTests : DbContext
    {
        [Test]
        public void AddsTagToFact()
        {
            var data = new List<Fact>().AsQueryable();
            var mockSet = new Mock<DbSet<Fact>>();
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<FactOffContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            optionsBuilder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var mockContext = new FactOffContext(optionsBuilder.Options);
            var factsService = new FactsService(mockContext);

            Fact fact = new Fact();
            fact.Context = "This is a fact.";
            var factContext = "This is a fact.";
            fact.FactId = Guid.NewGuid();
            Tag tag = new Tag();
            tag.TagId = Guid.NewGuid();
            Guid tagId = tag.TagId;

            factsService.CreateFact(factContext);
            factsService.AddTag(fact, tag);

            Assert.AreEqual(fact.Tags.First().TagId, tagId, "TagId of added tag is not the one it should be.");
        }

        [Test]
        public void CreateFactReturnsFactId()
        {
            var data = new List<Fact>().AsQueryable();
            var mockSet = new Mock<DbSet<Fact>>();
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<FactOffContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            optionsBuilder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var mockContext = new FactOffContext(optionsBuilder.Options);
            var factsService = new FactsService(mockContext);
            string factContext = "This is a fact.";

            factsService.CreateFact(factContext);

            Assert.AreEqual(mockContext.Facts.First().Context, factContext, "Fact not Created.");
        }

        [Test]
        public void DeleteFactFromDB()
        {
            var data = new List<Fact>().AsQueryable();
            var mockSet = new Mock<DbSet<Fact>>();
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<FactOffContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            optionsBuilder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var mockContext = new FactOffContext(optionsBuilder.Options);
            var factsService = new FactsService(mockContext);
            string factContext = "This is a fact.";

            factsService.CreateFact(factContext);
            factsService.DeleteFact(mockContext.Facts.First());

            Assert.IsEmpty(mockContext.Facts);
        }

        [Test]
        public void GetAllFactsReturnsModel()
        {
            var data = new List<Fact>().AsQueryable();
            var mockSet = new Mock<DbSet<Fact>>();
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<FactOffContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            optionsBuilder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var mockContext = new FactOffContext(optionsBuilder.Options);
            var factsService = new FactsService(mockContext);

            
        }

        [Test]
        public void GetFactByIdReturnsFact()
        {
            var data = new List<Fact>().AsQueryable();
            var mockSet = new Mock<DbSet<Fact>>();
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<FactOffContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            optionsBuilder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var mockContext = new FactOffContext(optionsBuilder.Options);
            var factsService = new FactsService(mockContext);
            Fact fact = new Fact();
            fact.Context = "This is a fact.";
            Tag tag = new Tag();
            tag.TagId = Guid.NewGuid();
            Guid tagId = tag.TagId;

            factsService.CreateFact(fact.Context);
            factsService.AddTag(fact, tag);

            Assert.IsNull(factsService.GetFactById(tagId));

        }

        [Test]
        public void RemoveTagFromDB()
        {
            var data = new List<Fact>().AsQueryable();
            var mockSet = new Mock<DbSet<Fact>>();
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<FactOffContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            optionsBuilder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var mockContext = new FactOffContext(optionsBuilder.Options);
            var factsService = new FactsService(mockContext);
            Fact fact = new Fact();
            fact.Context = "This is a fact.";
            Tag tag = new Tag();
            tag.Name = "Test";
            tag.TagId = Guid.NewGuid();

            factsService.CreateFact(fact.Context);
            factsService.AddTag(fact, tag);
            if (mockContext.Facts.First() == fact)
            {
                factsService.RemoveTag(fact, tag);
            }

            Assert.AreEqual(mockContext.Facts.First().Tags.Count, 0, "The fact wasn't removed.");

        }

        [Test]
        public void UpdateFactInDB()
        {
            var data = new List<Fact>().AsQueryable();
            var mockSet = new Mock<DbSet<Fact>>();
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Fact>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            var optionsBuilder = new DbContextOptionsBuilder<FactOffContext>();
#pragma warning disable CS0618 // Type or member is obsolete
            optionsBuilder.UseInMemoryDatabase();
#pragma warning restore CS0618 // Type or member is obsolete
            var mockContext = new FactOffContext(optionsBuilder.Options);
            var factsService = new FactsService(mockContext);
            var factContext = "This is a fact.";
            var updatedContext = "This is an updated fact.";

            factsService.CreateFact(factContext);
            factsService.UpdateFact(mockContext.Facts.First(), updatedContext);

            Assert.AreEqual(mockContext.Facts.First().Context, updatedContext, "Context is not updated.");
        }

    }
}