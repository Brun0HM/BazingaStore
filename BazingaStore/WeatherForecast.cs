namespace BazingaStore
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public int teste { get; set; }

        public string? Summary { get; set; }

        public int Teste { get; set; }
    }
}
