namespace VictorFrye.MockingMirror.WebApi.Roasting;

public interface IRoastService
{
    Task<Roast> AddRoast(Roast roast, CancellationToken cancellationToken);
}
