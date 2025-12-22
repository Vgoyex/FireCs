namespace Fire.Communication.Responses
{
    public class LoginResponse
    {
        public bool sucesso { get; set; }
        public string? mensagem { get; set; }
        public string? token { get; set; }
        public string? id { get; set; }
        public string? name { get; set; }
    }
}
