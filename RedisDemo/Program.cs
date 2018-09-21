using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            // Low-level abstraction interface
            // RedisNativeClientInterface();

            // High-Level abstraction interface
            // RedisClientInterface();

            // High-Level abstraction with Type
            // RedisTypedClientInterface();

            // Using Transaction with Redis
            // RedisTransactions();

            // Publishing 
            //using (IRedisClient client = new RedisClient())
            //{
            //    client.PublishMessage("debug", "Hello from C# app!");
            //}

            // Subcribing
            //using (IRedisClient client = new RedisClient())
            //{
            //    var sub = client.CreateSubscription();
            //    sub.OnMessage = (c, m) => Console.WriteLine("Got message: {0}, from channel {1}.", m, c);
            //    sub.SubscribeToChannels("news");

            //}

            Console.ReadLine();


        }

        private static void RedisTransactions()
        {
            using (IRedisClient client = new RedisClient())
            {
                var transaction = client.CreateTransaction();
                transaction.QueueCommand(c => client.Set("abc", 1));
                transaction.QueueCommand(c => client.Increment("abc", 1));
                transaction.Commit();
                var result = client.Get<int>("abc");
                Console.WriteLine(result);
            }
        }

        
        static void RedisNativeClientInterface()
        {
            using (IRedisNativeClient client = new RedisClient())
            {
                client.Set("urn:messages:1", Encoding.UTF8.GetBytes("Hello C# World!"));
            }

            using (IRedisNativeClient client = new RedisClient())
            {
                var result = Encoding.UTF8.GetString(client.Get("urn:messages:1"));
                Console.WriteLine("Message: {0}", result);
                var delkeys = client.Del("urn:messages:1");
                Console.WriteLine("Key {0} deleted.", delkeys);
            }
        }

        
        static void RedisClientInterface()
        {

            using (IRedisClient client = new RedisClient())
            {
                var customerNames = client.Lists["urn:customernames"];
                customerNames.Clear();
                customerNames.Add("Joe");
                customerNames.Add("Mary");
                customerNames.Add("Bob");
            }

            using (IRedisClient client = new RedisClient())
            {
                var customerNames = client.Lists["urn:customernames"];
                foreach (var customerName in customerNames)
                {
                    Console.WriteLine("Customer: {0} ", customerName);
                }
            }

        }

        static void RedisTypedClientInterface()
        {
            long lastId = 0;
            using (var redisClient = new RedisClient())
            {
                IRedisTypedClient<Customer> customerClient = redisClient.As<Customer>();
                
                var customer = new Customer()
                {
                    Id = customerClient.GetNextSequence(),
                    Name = "Customer 1",
                    Address = "#123, St. 123",
                    Orders = new List<Order> {
                                                new Order { OrderNo = "00001"},
                                                new Order { OrderNo = "00002"}
                                             }

                };

                var storedCustomer = customerClient.Store(customer);
                lastId = storedCustomer.Id;
            }

            using (var redisClient = new RedisClient())
            {
                IRedisTypedClient<Customer> customerClient = redisClient.As<Customer>();
                var customer = customerClient.GetById(lastId);
                Console.WriteLine("Got customer {0}, with name {1}", customer.Id, customer.Name);
            }

        }




    }

    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string Address { get; set; }

        public  List<Order> Orders { get; set; }
    }

    public class Order
    {
        public string OrderNo { get; set; }
    }
}
