﻿using System.Collections.Generic;

namespace CheeseMVC.Models

{
    public class Cheese
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CheeseCategory Category { get; set; }
		public int CategoryID { get; set; }
        public int ID { get; set; }
		public IList<CheeseMenu> CheeseMenu { get; set; }
		
    }
}
