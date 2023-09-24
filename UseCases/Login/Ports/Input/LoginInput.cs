using System;
namespace EY_Project.UseCases.Login.Ports.Input
{
	public class LoginInput<T>
    {
		public LoginInput(long? Id, String Tipo, T User)
		{
			this.Id = Id;
			this.Tipo = Tipo;
			this.User = User;
		}

        public long? Id { get; set; }
        public String? Tipo { get; set; }
		public T User { get; set; }
    }
}

