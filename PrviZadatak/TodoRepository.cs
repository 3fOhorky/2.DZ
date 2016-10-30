using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrugiITreciZadatak;

namespace PrviZadatak
{
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly List<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(List<TodoItem> initialDbState = null)
        {
            if (initialDbState != null)
            {
                _inMemoryTodoDatabase = initialDbState;
            }
            else
            {
                _inMemoryTodoDatabase = new List<TodoItem>();
            }// Shorter way to write this in C# using ?? operator :
             // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
             // x ?? y -> if x is not null , expression returns x. Else y.
        }

        public TodoItem Get(Guid todoId)
        {
            return this._inMemoryTodoDatabase.FirstOrDefault(s => s.Id == todoId);
        }

        public void Add(TodoItem todoItem)
        {
            if (this.Get(todoItem.Id) != null) { throw new DuplicateTodoItemException(String.Format("duplicate id: {0}", todoItem.Id)); }
            this._inMemoryTodoDatabase.Add(todoItem);
        }

        public bool Remove(Guid todoId)
        {
            var elementToRemove = this._inMemoryTodoDatabase.Where(s => s.Id == todoId).FirstOrDefault();
            if (elementToRemove == null)
            {
                return false;
            }
            else
            {
                this._inMemoryTodoDatabase.Remove(elementToRemove);
                return true;
            }
        }
        
        public void Update(TodoItem todoItem)
        {
            var selectedItem = this._inMemoryTodoDatabase.Where(s => s == todoItem).FirstOrDefault();
            if (selectedItem == null)
            {
                this._inMemoryTodoDatabase.Add(todoItem);
            } 
            else
            {
                this._inMemoryTodoDatabase.Select(s => { s = todoItem; return true; });
            }
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            var item = this._inMemoryTodoDatabase.FirstOrDefault(s => s.Id == todoId);
            if (item == null)
            {
                return false;
            }
            else
            {
                item.MarkAsCompleted();
                return true;
            }
        }

        public List<TodoItem> GetAll()
        {
            return this._inMemoryTodoDatabase.OrderBy(s => s.DateCreated).ToList();
        }

        public List<TodoItem> GetActive()
        {
            return this._inMemoryTodoDatabase.Where(s => s.IsCompleted == false).ToList();
        }

        public List<TodoItem> GetCompleted()
        {
            return this._inMemoryTodoDatabase.Where(s => s.IsCompleted == true).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            return this._inMemoryTodoDatabase.Where(filterFunction).ToList();
        }
    }
}
