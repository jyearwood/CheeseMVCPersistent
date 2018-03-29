using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CheeseMVC.Models;


namespace CheeseMVC.ViewModels
{
	public class AddMenuItemViewModel
	{
		public int CheeseID { get; set; }
		public int MenuID { get; set; }

		public Menu Menu { get; set; }
		public List<SelectListItem> Cheeses{get; set;}
		 
		public AddMenuItemViewModel() : base() { }

		public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
		{
			Menu = menu;
			Cheeses = new List<SelectListItem>();

			foreach (Cheese cheese in cheeses)
			{
				Cheeses.Add(new SelectListItem
				{
					Value = cheese.ID.ToString(),
					Text = cheese.Name
				});
			}
		}
        
    }
}
