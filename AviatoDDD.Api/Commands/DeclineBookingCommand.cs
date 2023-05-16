namespace AviatoDDD.Commands;

public class DeclineBookingCommand: ICommand
{
    public Guid Id { get; set; }
}