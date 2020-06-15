// <copyright file="BasePool.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace obServer.Network.NetworkElements
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// BasePool class.
    /// </summary>
    /// <typeparam name="T">Generic parameter.</typeparam>
    internal abstract class BasePool<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasePool{T}"/> class.
        /// </summary>
        public BasePool()
        {
            this.Pool = new Queue<T>();
        }

        private Queue<T> Pool { get; set; }

        /// <summary>
        /// False if 0, true if more than 0. Based on Pool count.
        /// </summary>
        /// <returns>Boolean.</returns>
        public bool NotNullElement()
        {
            return this.Pool.Count > 0;
        }

        /// <summary>
        /// Add an element to the pool.
        /// </summary>
        /// <param name="element">T type element.</param>
        public void AddPoolElement(T element)
        {
            this.Pool.Enqueue(element);
        }

        /// <summary>
        /// Give an element from the pool.
        /// </summary>
        /// <returns>Pool element.</returns>
        protected virtual T GetPoolElement()
        {
            return this.Pool.Dequeue();
        }
    }
}
