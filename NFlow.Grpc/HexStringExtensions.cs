using System;
using System.Text;

namespace NFlow.Grpc
{
    public static class ProtoStringExtensions
    {
        public static byte[] Pack(this String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static string Unpack(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static Google.Protobuf.ByteString FromHexToByteString(this string hex)
        {
            var addressByte = hex.Pack();
            return Google.Protobuf.ByteString.CopyFrom(addressByte);
        }

        public static string FromByteStringToHex(this Google.Protobuf.ByteString bs)
        {
            return bs.ToByteArray().Unpack();
        }

        public static Google.Protobuf.ByteString FromStringToByteSring(this string str)
        {
            return Google.Protobuf.ByteString.CopyFromUtf8(str);
        }

        public static string FromByteStringToString(this Google.Protobuf.ByteString str)
        {
            return str.ToStringUtf8();
        }

    }



}
