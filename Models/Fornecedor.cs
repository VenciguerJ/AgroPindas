namespace Fornecedores_config
{
    public class Fornecedor
    {
        private double cnpj;
        public double CNPJ
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        private string razaoSocial;
        public string RazaoSocial
        {
            get { return razaoSocial; }
            set { razaoSocial = value; }
        }

        private string endereco;
        public string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        public Fornecedor(double cnpj, string nome, string end)
        {
            CNPJ = cnpj;
            Endereco = end;
            RazaoSocial = nome;
        }

        public Fornecedor(double cnpj2, string nome)
        {
            CNPJ = cnpj2;
            RazaoSocial = nome;
        }

    }
}