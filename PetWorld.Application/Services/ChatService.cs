using PetWorld.Application.DTOs;
using PetWorld.Application.Interfaces;
using PetWorld.Domain.Entities;
using PetWorld.Domain.IRepository;

namespace PetWorld.Application.Services;

 public class ChatService : IChatService                                                                                                                                                                                          
    {                                                                                                                                                                                                                                
      private readonly IChatRepository _chatRepository;                                                                                                                                                                            
      private readonly IAgentOrchestrator _agentOrchestrator;                                                                                                                                                                      
                                                                                                                                                                                                                                   
        public ChatService(IChatRepository chatRepository, IAgentOrchestrator agentOrchestrator)                                                                                                                                     
        {                                                                                                                                                                                                                            
            _chatRepository = chatRepository;                                                                                                                                                                                        
            _agentOrchestrator = agentOrchestrator;                                                                                                                                                                                  
        }

        public async Task<ChatResponseDTO> ProcessMessageAsync(ChatRequestDTO request)
        {
            if (request.SessionId == null)
            {
              request.SessionId = Guid.NewGuid().ToString();
            }

            var history = await _chatRepository.GetBySessionIdAsync(request.SessionId);
            var result =  await _agentOrchestrator.ProcessAsync(request.Message, history);

            var chatSession = new ChatSession                                                                                                                                                                                                
            {                                                                                                                                                                                                                                
              SessionId = request.SessionId,                                                                                                                                                                                               
              Question = request.Message,                                                                                                                                                                                                  
              Answer = result.Answer,                                                                                                                                                                                                      
              IterationCount = result.IterationCount                                                                                                                                                                                       
            };                                                                                                                                                                                                                               
            await _chatRepository.AddAsync(chatSession);  

            return new ChatResponseDTO
            {
                Answer = result.Answer,
                IterationCount = result.IterationCount,
                SessionId = request.SessionId
            };
        }

        public async Task<IEnumerable<ChatSession>> GetHistoryAsync()
        {
            return await _chatRepository.GetAllAsync();
        }
    }  
