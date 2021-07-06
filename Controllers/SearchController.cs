using iaccess_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iaccess_test.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SearchController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchString, string currentFilter, int? pageNumber)
        {
            var searchStringList = new List<SearchStringDTO>();

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            if (searchString != null)
            {
                searchStringList = (from m in _context.SearchString.ToList()
                                    where m.StringContent.Contains(searchString, StringComparison.Ordinal)
                                    select new SearchStringDTO
                                    {
                                        StringId = m.StringId.ToString(),
                                        StringContent = m.StringContent,
                                        MatchTimes = CountMatchString(m.StringContent, searchString)
                                    })
                                    .ToList();
            }

            var searchStringDTO = PaginatedList<SearchStringDTO>.Create(searchStringList, pageNumber ?? 1, 5);

            ViewData["PageIndex"] = searchStringDTO.PageIndex;
            ViewData["TotalPages"] = searchStringDTO.TotalPages;
            ViewData["CurrentFilter"] = searchString;

            return View(searchStringDTO);
        }

        public IActionResult Insert()
        {
            List<SearchString> searchStringDummy = new List<SearchString>();

            for(var i = 0; i < 100000; i++)
            {
                int randomLength = new Random().Next(1000,2000);

                searchStringDummy.Add(new SearchString 
                {
                    StringContent = RandomString(randomLength)
                });
            }

            _context.SearchString.AddRange(searchStringDummy);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        private static string RandomString(int length)
        {
            Random random = new Random();
            string upperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string lowerCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();
            string numbers = "0123456789";
            string blankSpace = " ";
            string chars = upperCaseLetters + lowerCaseLetters + numbers + blankSpace;
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static string CountMatchString(string stringContent, string searchString)
        {
            int totalCount = 0;

            for (var i = 0; i <= stringContent.Length - searchString.Length; i++)
            {
                var subs = stringContent.Substring(i, searchString.Length);

                if (subs == searchString)
                {
                    totalCount++;
                } 
            }

            return totalCount.ToString();
        }
    }
}
