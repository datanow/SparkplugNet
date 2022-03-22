// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SparkplugTopic.cs">
// The project is licensed under the MIT license.
// </copyright>
// <summary>
//   The externally used Sparkplug topic class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SparkplugNet.Core.Messages
{
    using System;

    /// <summary>
    /// The externally used Sparkplug topic class.
    /// </summary>
    public class SparkplugTopic
    {
        /// <summary>
        /// The Namespace
        /// </summary>
        public SparkplugNamespace Namespace { get; set; }

        /// <summary>
        /// The Group Id
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// The Message type
        /// </summary>
        public SparkplugMessageType MessageType { get; set; }

        /// <summary>
        /// The Edge Node Id
        /// </summary>
        public string? EdgeNodeId { get; set; }

        /// <summary>
        /// The Device Id
        /// </summary>
        public string? DeviceId { get; set; }

        /// <summary>
        /// Constructs a Topic object from a string
        /// </summary>
        /// <param name="topic">The topic string</param>
        /// <exception cref="ArgumentException">Throws if an invalid topic is specified</exception>
        public SparkplugTopic(string topic)
        {
            var parts = topic.Split('/');

            if(parts.Length < 2)
            {
                throw new ArgumentException("Not a full Topic");
            }

            this.Namespace = EnumExtensions.GetValueFromDescription<SparkplugNamespace>(parts[0]);
            this.GroupId = parts[1];

            if (parts.Length > 2)
            {
                this.MessageType = EnumExtensions.GetValueFromDescription<SparkplugMessageType>(parts[2]);
            }

            if (parts.Length > 3)
            {
                this.EdgeNodeId = parts[3];
            }

            if (parts.Length > 4)
            {
                this.DeviceId = parts[4];
            }
        }
    }
}
