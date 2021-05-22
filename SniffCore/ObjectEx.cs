//
// Copyright (c) David Wendland. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.
//

namespace SniffCore
{
    public static class ObjectEx
    {
        public static bool IsNullOrEmpty(this object element)
        {
            if (element == null)
                return true;
            return string.IsNullOrEmpty(element.ToString());
        }

        public static bool IsNullOrWhiteSpace(this object element)
        {
            if (element == null)
                return true;
            return string.IsNullOrWhiteSpace(element.ToString());
        }
    }
}