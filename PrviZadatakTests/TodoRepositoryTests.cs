using DrugiITreciZadatak;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PrviZadatak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrviZadatak.Tests
{
    [TestClass()]
    public class TodoRepositoryTests
    {
        // Get Tests
        [TestMethod()]
        public void GettingNonExistantItemReturnsNull()
        {
            TodoRepository repository = new TodoRepository();
            Assert.IsNull(repository.Get(new Guid()));
        }
        [TestMethod()]
        public void GettingItemReturnsItemFromDatabase()
        {
            TodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            todoItem.Id = new Guid();
            repository.Add(todoItem);
            Assert.AreEqual(todoItem.Id, repository.Get(todoItem.Id).Id);
        }

        // Add Tests
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddingNullToDatabaseThrowsException()
        {
            TodoRepository repository = new TodoRepository();
            repository.Add(null);
        }
        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            TodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }
        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            TodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem("Groceries");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }
        
        // Remove Tests
        [TestMethod]
        public void RemovingNonExistingItemReturnsFalse()
        {
            TodoRepository repository = new TodoRepository();
            Assert.IsFalse(repository.Remove(new Guid()));
        }
        [TestMethod]
        public void RemovingItemWillRemoveFromDatabase()
        {
            TodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("Groceries");
            repository.Add(item);
            repository.Remove(item.Id);
            Assert.AreEqual(0, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(item.Id) == null);
        }

        // Update Tests
        [TestMethod()]
        public void UpdatingNonExistingItemAddsOneToDatabase()
        {
            TodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("Groceries");
            repository.Update(item);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(item.Id) != null);
        }
        [TestMethod()]
        public void UpdatingItemUpdatesItemInDatabase()
        {
            TodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("Groceries");
            repository.Add(item);
            item.Text = "UpdatedGroceries";
            repository.Update(item);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(item.Id) != null);
            Assert.AreEqual(item, repository.Get(item.Id));
            Assert.IsTrue(item.Text == repository.Get(item.Id).Text);
        }

        // MarkAsCompleted Tests
        [TestMethod()]
        public void MarkAsCompletedNonExistantItemReturnsFalse()
        {
            TodoRepository repository = new TodoRepository();
            Assert.IsFalse(repository.MarkAsCompleted(new Guid()));
        }
        [TestMethod()]
        public void MarkAsCompletedMarksAsCompleted()
        {
            TodoRepository repository = new TodoRepository();
            TodoItem item = new TodoItem("Groceries");
            item.IsCompleted = false;
            repository.Add(item);
            repository.MarkAsCompleted(item.Id);
            Assert.IsTrue(repository.Get(item.Id).IsCompleted);
        }

        [TestMethod()]
        public void GetAllGetsAllItems()
        {
            GenericList<TodoItem> todoItems = new GenericList<TodoItem>();
            TodoItem groceries = new TodoItem("Groceries");
            TodoItem keys = new TodoItem("Keys");
            TodoItem shopping = new TodoItem("Shopping");
            TodoRepository repository = new TodoRepository();
            repository.Add(groceries);
            repository.Add(keys);
            repository.Add(shopping);
            Assert.AreEqual(3, repository.GetAll().Count);
        }
        public void GetAllGetsAllSortedItems()
        {
            GenericList<TodoItem> todoItems = new GenericList<TodoItem>();
            TodoItem groceries = new TodoItem("Groceries");
            TodoItem keys = new TodoItem("Keys");
            TodoItem shopping = new TodoItem("Shopping");
            groceries.DateCreated = new DateTime(1999, 4, 4);
            keys.DateCreated = new DateTime(2000, 5, 5);
            shopping.DateCreated = new DateTime(1998, 3, 3);
            TodoRepository repository = new TodoRepository();
            repository.Add(groceries);
            repository.Add(keys);
            repository.Add(shopping);
            var repo = repository.GetAll();
            var previousDate = DateTime.MinValue;
            foreach (TodoItem item in repo)
            {
                Assert.IsTrue(item.DateCreated >= previousDate);
                previousDate = item.DateCreated;
            }
        }
        [TestMethod()]
        public void GetActiveTestGetsAllIncompleted()
        {
            GenericList<TodoItem> todoItems = new GenericList<TodoItem>();
            TodoItem groceries = new TodoItem("Groceries");
            TodoItem keys = new TodoItem("Keys");
            TodoItem shopping = new TodoItem("Shopping");
            groceries.IsCompleted = false;
            keys.IsCompleted = false;
            shopping.IsCompleted = true;
            TodoRepository repository = new TodoRepository();
            repository.Add(groceries);
            repository.Add(keys);
            repository.Add(shopping);
            Assert.AreEqual(2, repository.GetActive().Count);
        }

        [TestMethod()]
        public void GetCompletedTest()
        {
            GenericList<TodoItem> todoItems = new GenericList<TodoItem>();
            TodoItem groceries = new TodoItem("Groceries");
            TodoItem keys = new TodoItem("Keys");
            TodoItem shopping = new TodoItem("Shopping");
            groceries.IsCompleted = false;
            keys.IsCompleted = true;
            shopping.IsCompleted = true;
            TodoRepository repository = new TodoRepository();
            repository.Add(groceries);
            repository.Add(keys);
            repository.Add(shopping);
            Assert.AreEqual(2, repository.GetCompleted().Count);
        }
    }
}