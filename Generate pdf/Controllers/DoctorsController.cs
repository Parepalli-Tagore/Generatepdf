﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Generate_pdf.Models;
using Aspose.Pdf;
using System.Data;
using System.Security.Policy;

namespace Generate_pdf.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly HospitalDbContext _context;

        public DoctorsController(HospitalDbContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
              return _context.Doctors != null ? 
                          View(await _context.Doctors.ToListAsync()) :
                          Problem("Entity set 'HospitalDbContext.Doctors'  is null.");
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Specialised,Qualification,StartTime,EndTime,Experience")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Specialised,Qualification,StartTime,EndTime,Experience")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Doctors == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Doctors == null)
            {
                return Problem("Entity set 'HospitalDbContext.Doctors'  is null.");
            }
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                _context.Doctors.Remove(doctor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(long id)
        {
          return (_context.Doctors?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        DbClass DbObject = new DbClass();


        public IActionResult GeneratePDF()

        {

            var document = new Document

            {

                PageInfo = new PageInfo { Margin = new MarginInfo(28, 28, 28, 40) }

            };

            var pdfpage = document.Pages.Add();


            Table template = new Table

            {

                ColumnWidths = "25% 25% 25% 25%",

                DefaultCellPadding = new MarginInfo(10, 5, 10, 5),

                Border = new BorderInfo(BorderSide.All, .5f, Color.Black),

                DefaultCellBorder = new BorderInfo(BorderSide.All, .2f, Color.Black),


            };


            DataTable dt = DbObject.GetRecord();

            template.ImportDataTable(dt, true, 0, 0);

            document.Pages[1].Paragraphs.Add(template);


            using (var streamout = new MemoryStream())

            {

                document.Save(streamout);

                return new FileContentResult(streamout.ToArray(), "application/pdf")

                {

                    FileDownloadName = "DoctorsData.pdf"
                };

            }

        }
    }
}
