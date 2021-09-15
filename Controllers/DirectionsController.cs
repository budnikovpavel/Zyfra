using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Zyfra.DbModel;
using Zyfra.Repositories;
using Zyfra.Repositories.Interfaces;

namespace Zyfra.Controllers
{
    public class DirectionsController : Controller
    {
        private readonly IDirectionsRepository _repository;

        public DirectionsController(IDirectionsRepository repository)
        {
            _repository = repository;
        }

        // GET: Directions
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAllAsync());
        }

        // GET: Directions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _repository.GetAsync((int)id);
            if (direction == null)
            {
                return NotFound();
            }

            await SetViewBagAsync(direction);
            return View(direction);
        }

        // GET: Directions/Create
        public async Task<ViewResult> Create()
        {
            var direction = new Direction();
            await SetViewBagAsync(direction);
            return View(direction);
        }


        public async Task<IActionResult> FormCreate()
        {
            var direction = new Direction();
            await SetViewBagAsync(direction);
            return PartialView("_FormCreate");
        }

        private async Task SetViewBagAsync(Direction direction)
        {
            var canHasParent = await _repository.CanHasParentDirectionAsync();
            if (canHasParent)
                ViewBag.DirectionId =
                    new SelectList(await ParentsListAsync(), "Id", "Name", direction.ParentDirectionId);
            ViewBag.HasAnyDirections = canHasParent;
        }

        private async Task<IEnumerable<Direction>> ParentsListAsync()
        {
            IEnumerable<Direction> result = new List<Direction>(){new Direction { Name = "Нет" }
                };
            var directions = result.Union(await _repository.GetAllAsync());
            return directions;
        }

        // POST: Directions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,DateCreated,Description,ParentDirectionId,Id")] Direction direction)
        {
            if (ModelState.IsValid)
            {
                await _repository.CreateAsync(direction);
                return RedirectToAction(nameof(Index));
            }

            await SetViewBagAsync(direction);
            return View(direction);
        }

        // GET: Directions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _repository.GetAsync((int)id);
            if (direction == null)
            {
                return NotFound();
            }
            await SetViewBagAsync(direction);
            return View(direction);
        }

        // POST: Directions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,DateCreated,Description,ParentDirectionId,Id")] Direction direction)
        {
            if (id != direction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _repository.EditAsync(direction);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await _repository.GetAsync(direction.Id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            await SetViewBagAsync(direction);
            return View(direction);
        }

        // GET: Directions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direction = await _repository.GetAsync((int)id);
           
            if (direction == null)
            {
                return NotFound();
            }

            await SetViewBagAsync(direction);
            return View(direction);
        }

        // POST: Directions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repository.DeleteAsync(id);
           
           return RedirectToAction(nameof(Index));
        }

       
    }
}
