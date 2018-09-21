
# Redis Training

# 1. What's NoSql database?

# 2. NoSql Database Type and When to use:
```
+ Key/Value Based/Store:
	- Definition:
	- Vendor: Redis, MemcacheDB, etc.
	- When To Use:
+ Column Based/Stored:
	- Definition	:
	- Vendor		: Cassandra, HBase, etc.
	- When To Use	:
+ Document Based/Stored:
	- Definition	:
	- Vendor		: MongoDB, Couchbase, etc
	- When To Use	:
+ Graph Based/Stored:
	- Definition	:
	- Vendor		: OrientDB, Neo4J, etc.
	- When To Use	:
```

# 3. Redis NoSql
## 3.1. Redis Basic Commands
* Redis Full Documentatin: https://redis.io/documentation
* Redis Command References: https://redis.io/commands
* Redis 5 Data Types:
  - References: 
    https://redis.io/topics/data-types-intro
    https://redis.io/topics/data-types
    
  - Strings Data Type: String, Images, Serialize Objects( XML, JSON), Others ...
  	```
	-> Commands: 
		- SET: Set value with a key.
		- GET: Get value by a key.
		- APPEND: append string value.
		- INCR AND DECR: increment or decrement key's value.
		- GETRANGE: Get substring
		- MGET: Get multiple keys and values 
		- MSET: Set multiple keys and values 
		- STRLEN: Length of strings
		
	-> Examples: 
		** GET and SET **
		> SET USER "name: Test1"
		> GET USER
		> DEL USER
				
		** SET With Expiration **
		> SET USER "name: Test1" EX 5 
		> GET USER
		> DEL USER 
				
		** SET With Json data **
		> SET USER:1 "{'name': 'user1', 'email': 'user1@email.com'}"
		> GET USER:1
				
		** Incrementing: INCR AND DECR **
		> SET USER:ID 1
		> GET USER:ID
		> INCR USER:ID
		> GET USER:ID
		> APPEND USER:1 "extra data"
		> GET USER:1
		
		** GETRANGE: **
		> SET customer:1 "abcde000123"
		> getrange customer:1 5 9
				
		** MSET , MGET AND STRLEN: **
		> mset order:1 "order 1 data" order:2 "order 2 data"
		> mget order:1 order:2
		> strlen order:1
  	
	```
  - List Data Type: Store the list of strings type.
  	```
	-> Commands: 
		- LPUSH: 
		- RPUSH: 
		- LREM: 
		- LSET: 
		- LINDEX: 
		- LRANGE: 
		- LLEN: 
		- LPOP: 
		- RPOP:
		- LTRIM:
		
	-> Examples: 	
	
		** Adding to list: LPUSH AND RPUSH **
		> LPUSH COMMENTS "Comment 1"
		> lrange 0 1
		> LPUSH COMMENTS "Comment 2"
		> lrange 0 2
		> RPUSH COMMENTS "Comment 3"
		> lrange 0 3
		> RPUSH COMMENTS "Comment 4"
		> RPUSH COMMENTS "Comment 5"
		> lrange 0 5
				
		** LTRIM: Trimming list item **
		> RPUSH COMMENTS "Comment 6"
		> lrange 0 6
		> ltrim connents 0 5
		> lrange 0 5

		** Others commands: lindex, lpop and rpop **
		> lindex connents 2
		> lpop connents 
		> lrange 0 5
		> lpop connents 
		> lrange 0 5 
	
  	```
	
   - SETS Data Type: Sets contain a collection of unique strings, not repeat or duplicate value. 
	```
	-> Commands: 
	- SADD:
	- SCARD: 
	- SDIFF SINTER AND SUNION:
	- SISMEMBER:
	- SMEMBERS:
	- SMOVE:
	- SREM:
	-> Examples:
	** Adding to sets **
	> sadd post:1:likes "bob" "joe" "mary"
	> scard post:1:likes
	> smembers post:1:likes
	> sadd post:2:likes "bob" "tom"
	> sdiff post:1:likes post:2:likes
	> sinter post:1:likes post:2:likes
	> smembers post:1:likes "bob"

	```
   - Hashes: hashes are maps between string fields and string values
	```
	-> Commands: 
		- HSET: 
		- HMSET: 
		- HGET: 
		- HMGET:
		- HGETALL:
		- HDEL:
		- HEXISTS:
		- HINCRBY:
		- HKEYS:
		- HVALS:
	-> Examples:
		> hset user:1:h name "joe"
		> hget user:1:h name 
		> hmset user:1:h email "joe@test1.com" id 1
		> hmset user:1:h name email id
		> hmgetall user:1:h
		> hkeys user:1:h
		> hvals user:1:h
	```		
    - Sorted sets: the same to sets but it's sorted. Sorted sets have excellent performance charateristics for adding, removing and updating
	```		
	-> Commands: 
		- ZADD:
		- ZCARD:
		- ZCOUNT:
		- ZINCRBY:
		- ZRANGE:
		- ZRANK: get index of field or item.
		- ZREM:
		- ZSCORE:

	-> Examples:
		> zadd heightscores 120 "joe" 100 "bob" 150 "mary" 90 "tom"
		> zrange heightscores 0 4
		> zrange heightscores 0 4 WITHSCORES 
		> zadd heightscores 155 "joe"
		> zrank heightscores bob
		> zrank heightscores tom
		> zscore mary
	``` 
## 3.2. Redis Pub and Sub: 

** Redis can be used as a message bus.

** References: 
https://redis.io/topics/pubsub

** Commands and Examples:
```
** Using Pub and Sub **
- Terminal 1:
> subscriber geetings 
	
- Terminal 2:
> publish greetings "hello redis"
> publish greet "hello"
	
- Terminal 3: 
> subscriber errors
	
- Terminal 2:
> publish greetings "h1"
> publish errors "oops"
	
- Terminal 4: subscribe with pattern
> publish greet*

- Terminal 2:
> publish greetings "h1"
> publish greet "h1"
```

## 3.3. Redis Transactions:

** Redis has limited support for transactions.
** References: https://redis.io/topics/transactions
** Using Transactions:
** Commands and Examples:
```
	-> Terminal 1:
	> multi 

	-> Terminal 2:
	> set account-a 100
	> set account-b 200

	-> Terminal 1:
	> incrby account-a -50
	> incrby account-b 50

	-> Terminal 2:
	> get account-a
	> incrby account-a 300

	-> Terminal 1:
	> exec 

	-> Terminal 2:
	> set account-a 0

	-> Terminal 1:
	> watch account-a
	> multi
	> incrby account-a -50
	> incrby account-b +50

	-> Terminal 2:
	> set account-a 25

	-> Terminal 1:
	> exec
```
## 3.4. Redis Client Demo with C# Console Application
