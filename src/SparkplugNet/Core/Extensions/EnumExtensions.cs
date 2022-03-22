// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Hämmer Electronics">
// The project is licensed under the MIT license.
// </copyright>
// <summary>
//   A class that contains extension method for all <see cref="Enum"/> data types.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SparkplugNet.Core.Extensions;

/// <summary>
/// A class that contains extension method for all <see cref="Enum"/> data types.
/// </summary>
internal static class EnumExtensions
{
    /// <summary>
    /// Gets the <see cref="DescriptionAttribute" /> text of the <see cref="Enum"/>.
    /// </summary>
    /// <param name="enum">The <see cref="Enum"/> value.</param>
    /// <returns>The <see cref="DescriptionAttribute" /> text of the <see cref="Enum"/>.</returns>
    internal static string GetDescription(this Enum @enum)
    {
        var genericEnumType = @enum.GetType();
        var memberInfo = genericEnumType.GetMember(@enum.ToString());

        if (memberInfo.Length <= 0)
        {
            return @enum.ToString();
        }

        var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? ((DescriptionAttribute)attributes.ElementAt(0)).Description : @enum.ToString();
    }

    /// <summary>
    /// Gets an Enum value from a string description
    /// </summary>
    /// <typeparam name="T">The Enum Type</typeparam>
    /// <param name="description">A Description decorator value</param>
    /// <returns>The Enum value</returns>
    /// <exception cref="ArgumentException">Throws if the description value was not found on the target Enum</exception>
    public static T GetValueFromDescription<T>(string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
            typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }

        throw new ArgumentException("Not found.", nameof(description));
        // Or return default(T);
    }
}
