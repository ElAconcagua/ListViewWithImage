using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Runtime.Kernel.Core
{
   public static class StringExtensions
   {
      public static bool IsUrl(this string text)
      {
         System.Uri uriResult = null;
         return System.Uri.TryCreate(text, UriKind.Absolute, out uriResult) && System.Uri.CheckSchemeName(uriResult.Scheme);
      }

      public static bool IsHex(this string text)
      {
          return System.Text.RegularExpressions.Regex.IsMatch(text, @"\A\b[0-9a-fA-F]+\b\Z");
      }
   }
}
