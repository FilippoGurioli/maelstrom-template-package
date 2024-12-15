using System;

namespace Maelstrom
{
    /// <summary>
    /// Represents an optional value.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class Optional<T>
    {
        private readonly T _value;
        private readonly bool _hasValue;

        /// <summary>
        /// Indicates whether the optional has a value.
        /// </summary>
        public bool HasValue => _hasValue;

        /// <summary>
        /// The value of the optional.
        /// </summary>
        public T Value => GetValueOrThrow();

        /// <summary>
        /// An empty optional.
        /// </summary>
        public static Optional<T> Empty = new();

        /// <summary>
        /// Creates a new optional with the specified value.
        /// </summary>
        /// <param name="value">The value to store.</param>
        private Optional(T value)
        {
            _value = value;
            _hasValue = true;
        }

        /// <summary>
        /// Creates a new empty optional.
        /// </summary>
        private Optional()
        {
            _hasValue = false;
        }

        /// <summary>
        /// Creates a new optional with the specified value.
        /// </summary>
        /// <param name="value">The value to store.</param>
        /// <returns>The optional with the value.</returns>
        public static Optional<T> Of(T value) => new(value);

        /// <summary>
        /// Tries to get the value of the optional.
        /// </summary>
        /// <param name="value">The value of the optional.</param>
        /// <returns>True if the optional has a value, false otherwise.</returns>
        public bool TryGetValue(out T value)
        {
            value = _value;
            return _hasValue;
        }

        /// <summary>
        /// Gets the value of the optional or the specified default value.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the optional is empty.</param>
        /// <returns>The value of the optional or the default value.</returns>
        public T GetValueOrDefault(T defaultValue)
        {
            return _hasValue ? _value : defaultValue;
        }

        /// <summary>
        /// Gets the value of the optional or throws an exception if the optional is empty.
        /// </summary>
        /// <returns>The value of the optional.</returns>
        /// <exception cref="InvalidOperationException">If the optional is empty.</exception>
        public T GetValueOrThrow()
        {
            if (!_hasValue) throw new InvalidOperationException("Optional does not have a value.");
            return _value;
        }
    }
}