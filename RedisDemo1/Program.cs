using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IRedisNativeClient redisCient = new RedisClient())
            {
                redisCient.Set("USER:1", Encoding.UTF8.GetBytes("Hello C# World!"));
            }

            using (IRedisNativeClient redisCient = new RedisClient())
            {
                var result = Encoding.UTF8.GetString(redisCient.Get("USER:1"));
                Console.WriteLine(result);
            }

            Console.ReadLine();
        }
    }
}
