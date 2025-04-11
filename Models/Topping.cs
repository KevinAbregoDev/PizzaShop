namespace Models
{
    public class Topping
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }

        public List<PizzaTopping> PizzaToppings { get; set; } = new List<PizzaTopping>();
    }
}