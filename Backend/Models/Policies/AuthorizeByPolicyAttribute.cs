using Microsoft.AspNetCore.Authorization;

public class AuthorizeByPolicyAttribute : AuthorizeAttribute
{
    public AuthorizeByPolicyAttribute(Policy policy)
    {
        Policy = policy.ToString();
    }
}