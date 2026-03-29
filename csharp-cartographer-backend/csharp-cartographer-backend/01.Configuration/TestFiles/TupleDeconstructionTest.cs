namespace csharp_cartographer_backend._01.Configuration.TestFiles
{
    public class TupleDeconstructionTest
    {
        public static void RunTupleTest()
        {
            // Tuple example
            (int x, int y) point = (10, 20);
        }

        public static void RunDeconstructionTest()
        {
            // Deconstruction example
            var point = (10, 20);
            var (x, y) = point;
            var (_, z) = point;
        }
    }
}
