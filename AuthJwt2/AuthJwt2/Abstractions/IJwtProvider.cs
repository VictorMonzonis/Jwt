using AuthJwt2.Domain.Entities;

namespace AuthJwt2.Abstractions
{
    public interface IJwtProvider
    {
        string Generate(Member member);
    }
}
