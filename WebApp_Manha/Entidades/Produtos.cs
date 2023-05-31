namespace WebApp_Manha.Entidades
{
    public class Produtos
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public bool Ativo { get; set; }
        public string CaminhoImagem {get; set;}
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}
