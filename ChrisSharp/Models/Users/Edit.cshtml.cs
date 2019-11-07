﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ChrisSharp.Data;
using ChrisSharp.Models;

namespace ChrisSharp.Models.Users
{
    public class EditModel : PageModel
    {
        private readonly ChrisSharp.Data.ChrisSharpContext _context;

        public EditModel(ChrisSharp.Data.ChrisSharpContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //User = await _context.User.FirstOrDefaultAsync(m => m.ID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                /*if (!UserExists(User.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }*/
            }

            return RedirectToPage("./Index");
        }

        //private bool UserExists(int id)
        //{
        //    return _context.User.Any(e => e.ID == id);
        //}
    }
}
