namespace LCS.BWA.DTOS
{
    public class NewPasswordCommand
    {
        public string Email { get; set; }
        public string Password { get; }
        public int RecoveryPin { get; set; }
    }
}
