using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;

namespace csharp_cartographer_backend._06.Workflows.Artifacts
{
    public interface IGenerateArtifactWorkflow
    {
        Task<Artifact> ExecGenerateDemoArtifact(string fileName);

        Task<Artifact> ExecGenerateUserArtifact(GenerateArtifactDto requestDto);
    }
}
