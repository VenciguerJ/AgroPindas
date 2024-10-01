namespace Client_Config
{
    public class Client
    {
        public int ID { get; set; }
        
        private string name; 
        public string Name
        {
            get { return name; }
            set {
                name = value; 
            }
        }

        private double cpf;
        public double CPF
        {
            get { return cpf; }
            set { cpf = value; }
        }

        private double phoneNumber;
        public double PhoneNumber { get; set; }

        public Boolean Assinante { get; set; }

        public Client(string nome, double cpf, double Telefone)
        {
            Name = nome;
            CPF = cpf;
            PhoneNumber = Telefone;
            Assinante = false;
        }
    }
}
