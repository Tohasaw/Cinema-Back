namespace Cinema.Data.Features.Movies.Commands.UpdateMovie;
public sealed record UpdateMovieDto
{
    public string Title { get; set; }
    public string Countries { get; set; }
    public string Genres { get; set; }
    public string Director { get; set; }
    public int Length { get; set; }
    public string AgeLimit { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string VideoLink { get; set; }
}