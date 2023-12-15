using MassTransit;

namespace GeneralBusMessages.Message
{
    public class Model: IConsumer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
