namespace Zadanie9.DTOs;

public class UserRDTO
{
    public int IdUser { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}