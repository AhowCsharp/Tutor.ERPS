using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.Game
{
    public class GameResponse
    {
        public GameResponse(IEnumerable<GameWordsDTO> datasDto)
        {
            words = datasDto;
        }

        public IEnumerable<GameWordsDTO> words { get; set; }
    }
}