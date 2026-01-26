using csharp_cartographer_backend._03.Models.Artifacts;
using csharp_cartographer_backend._08.Controllers.Artifacts.Dtos;

namespace csharp_cartographer_backend._06.Workflows.Artifacts
{
    public interface IGenerateArtifactWorkflow
    {
        /// <summary>
        /// Executes the generate-artifact workflow for demo files.
        /// </summary>
        /// <param name="fileName">The name of the demo file.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A fully mapped artifact.</returns>
        Task<Artifact> ExecGenerateDemoArtifact(string fileName, CancellationToken cancellationToken);

        /// <summary>
        /// Executes the generate-artifact workflow for user uploaded files.
        /// </summary>
        /// <param name="requestDto">The DTO containing the uploaded file data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A fully mapped artifact.</returns>
        Task<Artifact> ExecGenerateUserArtifact(GenerateArtifactDto requestDto, CancellationToken cancellationToken);
    }
}
