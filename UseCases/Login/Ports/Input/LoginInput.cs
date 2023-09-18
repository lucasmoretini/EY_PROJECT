using System;
namespace EY_Project.UseCases.Login.Ports.Input
{
	public class LoginInput
	{
		public LoginInput(long? Id, String Tipo)
		{
			this.Id = Id;
			this.Tipo = Tipo;
		}

        public long? Id { get; set; }
        public String? Tipo { get; set; }
    }
}

