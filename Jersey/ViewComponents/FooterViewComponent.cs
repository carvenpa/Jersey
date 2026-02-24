using Jersey.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Jersey.Models;
using Microsoft.EntityFrameworkCore;

namespace Jersey.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public FooterViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    // Retrieve footer data from the database
        //    var footerInfo = await _context.Model.vi..FooterLinks.FirstOrDefaultAsync();

        //    // Pass data to the View
        //    return View("Default", footerInfo);
        //}

        public IViewComponentResult Invoke()
        {
            var footerData = _context.FooterLinks
                .GroupBy(f => f.SectionName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(f => new FooterLinkVM
                    {
                        LinkText = f.LinkText,
                        LinkUrl = f.LinkUrl
                    }).ToList()
                );

            return View(footerData);
        }
    }
}
