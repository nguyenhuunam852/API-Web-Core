namespace MyWebApp.Shared
{
    public static class ErrorsListDictionary
    {
        public static Dictionary<int, string> Errors = new Dictionary<int, string>()
        {
            { StatusCodes.Status400BadRequest , "BadRequest Errors" },
            { StatusCodes.Status409Conflict , "Conflict Errors" },
            { StatusCodes.Status403Forbidden , "" },
            { StatusCodes.Status404NotFound , "NotFound Errors" },
            { StatusCodes.Status500InternalServerError , "Server Errors" },
            { StatusCodes.Status422UnprocessableEntity , "Validation Errors" },
        };
    }
}
