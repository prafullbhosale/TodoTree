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
    public class TodoNodeParentMapsController : Controller
    {
        private readonly TodoTreeContext _context;

        public TodoNodeParentMapsController(TodoTreeContext context)
        {
            _context = context;
        }

        // GET: TodoNodeParentMaps
        public async Task<IActionResult> Index()
        {
            var todoTreeContext = _context.TodoNodeParentMap.Include(t => t.ParentNode).Include(t => t.TodoNode);
            return View(await todoTreeContext.ToListAsync());
        }

        // GET: TodoNodeParentMaps/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoNodeParentMap = await _context.TodoNodeParentMap
                .Include(t => t.ParentNode)
                .Include(t => t.TodoNode)
                .SingleOrDefaultAsync(m => m.TodoNodeParentMapId == id);
            if (todoNodeParentMap == null)
            {
                return NotFound();
            }

            return View(todoNodeParentMap);
        }

        // GET: TodoNodeParentMaps/Create
        public IActionResult Create()
        {
            ViewData["ParentNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title");
            ViewData["TodoNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title");
            return View();
        }

        // POST: TodoNodeParentMaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TodoNodeParentMapId,TodoNodeId,ParentNodeId")] TodoNodeParentMap todoNodeParentMap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoNodeParentMap);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ParentNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title", todoNodeParentMap.ParentNodeId);
            ViewData["TodoNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title", todoNodeParentMap.TodoNodeId);
            return View(todoNodeParentMap);
        }

        // GET: TodoNodeParentMaps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoNodeParentMap = await _context.TodoNodeParentMap.SingleOrDefaultAsync(m => m.TodoNodeParentMapId == id);
            if (todoNodeParentMap == null)
            {
                return NotFound();
            }
            ViewData["ParentNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title", todoNodeParentMap.ParentNodeId);
            ViewData["TodoNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title", todoNodeParentMap.TodoNodeId);
            return View(todoNodeParentMap);
        }

        // POST: TodoNodeParentMaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TodoNodeParentMapId,TodoNodeId,ParentNodeId")] TodoNodeParentMap todoNodeParentMap)
        {
            if (id != todoNodeParentMap.TodoNodeParentMapId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(todoNodeParentMap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TodoNodeParentMapExists(todoNodeParentMap.TodoNodeParentMapId))
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
            ViewData["ParentNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title", todoNodeParentMap.ParentNodeId);
            ViewData["TodoNodeId"] = new SelectList(_context.TodoNode, "TodoNodeId", "Title", todoNodeParentMap.TodoNodeId);
            return View(todoNodeParentMap);
        }

        // GET: TodoNodeParentMaps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var todoNodeParentMap = await _context.TodoNodeParentMap
                .Include(t => t.ParentNode)
                .Include(t => t.TodoNode)
                .SingleOrDefaultAsync(m => m.TodoNodeParentMapId == id);
            if (todoNodeParentMap == null)
            {
                return NotFound();
            }

            return View(todoNodeParentMap);
        }

        // POST: TodoNodeParentMaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var todoNodeParentMap = await _context.TodoNodeParentMap.SingleOrDefaultAsync(m => m.TodoNodeParentMapId == id);
            _context.TodoNodeParentMap.Remove(todoNodeParentMap);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TodoNodeParentMapExists(int id)
        {
            return _context.TodoNodeParentMap.Any(e => e.TodoNodeParentMapId == id);
        }
    }
}
