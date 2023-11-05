using API.Entities;

namespace API;

public interface IPlayerCharacterRepository
{
    Task<ICollection<PlayerCharacter>> GetCharactersForUser(string username);
    PlayerCharacter Create(PlayerCharacter pc);
}
