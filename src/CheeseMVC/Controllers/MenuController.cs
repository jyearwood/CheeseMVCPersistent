using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;
using CheeseMVC.ViewModels;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
		private readonly CheeseDbContext context;

		public MenuController(CheeseDbContext dbContext)
		{
			context = dbContext;
		}

        
        public IActionResult Index()
        {
			IList<Menu> menus = context.Menus.Include(c => c.CheeseMenus).ToList(); 
            return View(menus);
        }

		public IActionResult Add()
		{
			AddMenuViewModel AddMenuViewModel = new AddMenuViewModel();
			return View(AddMenuViewModel);
		}


		[HttpPost]

		public IActionResult Add(AddMenuViewModel addMenuViewModel)
		{
			if (ModelState.IsValid)
			{
				Menu menu = new Menu
				{
					Name = addMenuViewModel.Name
				};

				context.Menus.Add(menu);
				context.SaveChanges();

				return Redirect("/Menu/ViewMenu/" + menu.ID);
			}

			return View(addMenuViewModel);
		}

		public IActionResult ViewMenu(int id)
		{
		
			Menu menu = context.Menus.Single(c => c.ID == id);

			List<CheeseMenu> items = context
				.CheeseMenus
				.Include(item => item.Cheese)
				.Where(cm => cm.MenuID == id)
				.ToList();

			ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel(menu, items);
			return View(viewMenuViewModel);
		}

		public IActionResult AddItem(int id)
		{
			Menu menu = context.Menus.Single(c => c.ID == id);
			IList<Cheese> cheeses = context
				.Cheeses
				.Include(c => c.Category).ToList();

			AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, cheeses);
			return View(addMenuItemViewModel);
		}

		[HttpPost]
		public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
		{
			if (ModelState.IsValid)
			{
				IList<CheeseMenu> existingItems = context.CheeseMenus

				.Where(c => c.CheeseID == addMenuItemViewModel.CheeseID)
				.Where(c => c.MenuID == addMenuItemViewModel.MenuID).ToList();

				if (existingItems.Count < 1)
				{
					CheeseMenu newCheeseMenu = new CheeseMenu
					{
						MenuID = addMenuItemViewModel.MenuID,
						Menu = context.Menus.Single(c => c.ID == addMenuItemViewModel.MenuID),
						CheeseID = addMenuItemViewModel.CheeseID,
						Cheese = context.Cheeses.Single(c => c.ID == addMenuItemViewModel.CheeseID)

					};

					context.CheeseMenus.Add(newCheeseMenu);
					context.SaveChanges();

					return Redirect("/Menu/ViewMenu/" + newCheeseMenu.MenuID);

				}

				return Redirect("/Menu/ViewMenu/" + addMenuItemViewModel.MenuID);

			}

			return View(addMenuItemViewModel);

		}
	

    }
}
