namespace Models
{
    public class Pizza
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<Topping> Toppings { get; set; }

        public List<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
    }
}