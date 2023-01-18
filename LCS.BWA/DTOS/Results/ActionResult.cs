namespace LCS.BWA.DTOS.Results
{
    public class ActionResult: BaseActionResult
    {
        
    }
    public class ActionResult<T>: BaseActionResult where T : class
    {
        public ActionResult()
        {
            Entities = new();
        }
        public T? Entity { get; set; }
        public List<T> Entities { get; set; }
    }
    public class BaseActionResult
    {
        public BaseActionResult()
        {
            ErrorMessages = new List<string>();
            IsSuccess = true;
        }
        public void AddError(string error)
        {
            ErrorMessages.Add(error);
            IsSuccess = false;
        }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public string FistError => ErrorMessages.FirstOrDefault() ?? "No error message.";
    }
}
