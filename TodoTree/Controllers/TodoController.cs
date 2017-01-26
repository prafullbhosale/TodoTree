using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoModels.Models;
using TodoTree.Data;

namespace TodoTree.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoTreeContext _context;

        public TodoController(TodoTreeContext context)
        {
            _context = context;
        }

        // GET: Todo
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoNode.ToListAsync());
        }

        // GET: Todo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoNode = await _context.TodoNode
                .SingleOrDefaultAsync(m => m.TodoNodeId == id);
            if (todoNode == null)
            {
                return NotFound();
            }

            return View(todoNode);
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoNodeId,Title,Description")] TodoNode todoNode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoNode);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(todoNode);
        }

        // GET: Todo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoNode = await _context.TodoNode.SingleOrDefaultAsync(m => m.TodoNodeId == id);
            if (todoNode == null)
            {
                return NotFound();
            }
            return View(todoNode);
        }

        // POST: Todo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TodoNodeId,Title,Description")] TodoNode todoNode)
        {
            if (id != todoNode.TodoNodeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoNode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoNodeExists(todoNode.TodoNodeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(todoNode);
        }

        // GET: Todo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoNode = await _context.TodoNode
                .SingleOrDefaultAsync(m => m.TodoNodeId == id);
            if (todoNode == null)
            {
                return NotFound();
            }

            return View(todoNode);
        }

        // POST: Todo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoNode = await _context.TodoNode.SingleOrDefaultAsync(m => m.TodoNodeId == id);
            _context.TodoNode.Remove(todoNode);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TodoNodeExists(int id)
        {
            return _context.TodoNode.Any(e => e.TodoNodeId == id);
        }
    }
}
