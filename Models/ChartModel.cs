namespace InteractiveTyingGameBlazor.Models
{
    public class ChartModel<T1, T2>(T1 x, T2 y)
    {
        public T1 X { get; set; } = x;

        public T2 Y { get; set; } = y;
    }
}
