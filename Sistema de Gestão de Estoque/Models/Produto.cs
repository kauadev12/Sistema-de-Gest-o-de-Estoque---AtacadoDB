namespace Sistema_de_Gestão_de_Estoque.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int FornecedorId { get; set; }
        public decimal Preco {  get; set; }
        public int Estoque {  get; set; }
        public int EstoqueMinimo { get; set; }  

        public Fornecedor? Fornecedor { get; set; }
    }
}
