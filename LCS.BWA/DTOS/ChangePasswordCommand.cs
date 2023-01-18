namespace LCS.BWA.DTOS
{
    public class ChangePasswordCommand
    {
        public Guid Id { get; }
        
        public string OldPassword { get; }
        public string NewPassword { get; }
    }
}
