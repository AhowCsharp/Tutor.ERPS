using Swashbuckle.AspNetCore.Annotations;
using TUTOR.Biz.Domain.DTO;

namespace TUTOR.Biz.Models.Responses.Game
{
    public class GameResponse
    {
        public GameResponse(IEnumerable<GameWordsDTO> datasDto)
        {
            Words = datasDto;
        }

        public IEnumerable<GameWordsDTO> Words { get; set; }
    }
}