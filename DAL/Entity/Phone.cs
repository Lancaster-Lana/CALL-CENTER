namespace Laneta.DAL.Entity
{
    public class Phone : Entity
    {
        public string Label { get; set; }

        public string Number { get; set; }

        public int CustomerID { get; set; }
    }
}
