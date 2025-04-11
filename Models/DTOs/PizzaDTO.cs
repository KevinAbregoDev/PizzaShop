namespace Models.DTOs
{
    public class PizzaDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public List<ToppingDTO> Toppings { get; set; }
    }
}