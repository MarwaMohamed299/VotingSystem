using VotingSystem.Domain.Entities;

namespace VotingSystem.Infrastructure.Repositories.Polls
{
    public interface IPollRepository
    {
        Task CreatePollAsync(Poll poll);
        Task DeletePollAsync(Poll poll);
        Task GetPollByIdAsync(int id);
        Task<Poll> GetPollWithOptionsAsync(int pollId);
        Task UpdatePollAsync(Poll poll);
    }
}