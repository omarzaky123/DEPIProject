namespace DEPIAPI.DTO
{
    public class ProductVartionDTOInsert
    {
        public int Id { get; set; }
        public string VartionValue { get; set; }
        public decimal AddtionalPrice { get; set; }
        public int Quantity_In_Stock { get; set; }
        public int ProductID { get; set; }
        public int? VartionID { get; set; }
    }
}
