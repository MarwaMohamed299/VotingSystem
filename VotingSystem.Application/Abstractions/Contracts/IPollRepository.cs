using VotingSystem.Domain.Entities;

namespace VotingSystem.Infrastructure.Repositories.Polls
{
    public interface IPollRepository
    {
        Task CreatePollAsync(Poll poll);
        Task DeletePollAsync(int id);
        Task<Poll?> GetPollByIdAsync(int id);
        Task<Poll> GetPollWithQuestionsAsync(int pollId);
        Task UpdatePollAsync(Poll poll);
        Task<Poll> GetVotesForEachPullAsync(int pollId);
    }
}