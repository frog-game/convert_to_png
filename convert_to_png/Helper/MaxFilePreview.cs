namespace convert_to_png
{
    internal class MaxFilePreview
    {
        public MaxFilePreview(byte[] thumbbytes)
        {
            ConvertBytes(thumbbytes);
        }

        private void ConvertBytes(byte[] thumbbytes)
        {
            int num = thumbbytes[14];
            int num2 = thumbbytes[0x10];
            int num3 = 3;
            int num4 = 0x6a;
            int num5 = 30;
            int num6 = 0x80;
            int num7 = 6;
            int num8 = thumbbytes.Length - num4 - num7;
            bool flag = num == 0;
            bool flag2 = num2 == 0;
            if (num == 0 || num2 == 0)
            {
                for (int j = 0x40; j >= 2; j /= 2)
                {
                    if (num8 / (num6 * j) / num3 < num5 * j)
                    {
                        if (flag)
                        {
                            num = num6 * (j / 2);
                            num2 = num8 / num / num3;
                        }
                        else if (flag2)
                        {
                            num2 = num6 * (j / 2);
                            num = num8 / num2 / num3;
                        }
                    }
                }
            }
            int num10 = num * num3;
            if (num10 % 4 != 0)
            {
                num10 += 4 - num10 % 4;
            }
            int num11 = num10 - num * 3;
            int length = thumbbytes.Length;
            int num13 = num * num2 * 4;
            byte[] buffer = new byte[num13];
            int num14 = num4;
            for (int i = 0; i < num2; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (num14 >= length)
                    {
                        IsEmpty = true;
                        return;
                    }
                    int num17 = num2 - i - 1;
                    int num18 = (j + num17 * num) * 4;
                    buffer[num18++] = thumbbytes[num14++];
                    buffer[num18++] = thumbbytes[num14++];
                    buffer[num18++] = thumbbytes[num14++];
                    buffer[num18++] = 0xff;
                }
                num14 += num11;
            }
            Width = num;
            Height = num2;
            Stride = num * 4;
            Pixels = buffer;
            IsEmpty = false;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public int Stride { get; private set; }

        public byte[]? Pixels { get; private set; }

        public bool IsEmpty { get; private set; }
    }
}

