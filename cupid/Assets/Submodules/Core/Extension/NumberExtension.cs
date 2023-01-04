namespace Wayway.Engine
{
    public static class NumberExtension
    {
        public static string ToKmbt(this int number)
        {
            return number switch
            {
                >= 100000000 => (number / 1000000D).ToString("0.#M"),
                >= 10000000 => (number / 1000000D).ToString("0.##M"),
                >= 1000000 => (number / 1000000D).ToString("0.###M"),
                >= 100000 => (number / 1000D).ToString("0.#K"),
                >= 10000 => (number / 1000D).ToString("0.##K"),
                _ => number.ToString("#,0")
            };
        }
        
        public static string ToKmbt(this long number)
        {
            return number switch
            {
                >= 100000000000000000 => (number / 1000000000000000D).ToString("0.#Q"),
                >= 10000000000000000 => (number / 1000000000000000D).ToString("0.##Q"),
                >= 1000000000000000 => (number / 1000000000000000D).ToString("0.###Q"),
                >= 100000000000000 => (number / 1000000000000D).ToString("0.#T"),
                >= 10000000000000 => (number / 1000000000000D).ToString("0.##T"),
                >= 1000000000000 => (number / 1000000000000D).ToString("0.###T"),
                >= 100000000000 => (number / 1000000000D).ToString("0.#B"),
                >= 10000000000 => (number / 1000000000D).ToString("0.##B"),
                >= 1000000000 => (number / 1000000000D).ToString("0.###B"),
                >= 100000000 => (number / 1000000D).ToString("0.#M"),
                >= 10000000 => (number / 1000000D).ToString("0.##M"),
                >= 1000000 => (number / 1000000D).ToString("0.###M"),
                >= 100000 => (number / 1000D).ToString("0.#K"),
                >= 10000 => (number / 1000D).ToString("0.##K"),
                _ => number.ToString("#,0")
            };
        }
    }
}
