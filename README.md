# What is this project about

Here you can find my results for performance testing of the Redis BloomFilter and the load tests themself. To get the info about BloomFilter, please visit https://oss.redis.com/redisbloom/

# Performance testing results

```
1. 
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 1 000 000
    threads   - 20

 * Insertion during 3 minutes (warmup), then insertion + existence check during 3 minutes.
 * Filter stats after test:

    size:6944003
    bytes:2576608 bits:20612864 hashes:15 hashwidth:64 capacity:1000000 size:1000000 ratio:5e-05
    bytes:5513880 bits:44111040 hashes:16 hashwidth:64 capacity:2000000 size:2000000 ratio:2.5e-05
    bytes:11749104 bits:93992832 hashes:17 hashwidth:64 capacity:4000000 size:3944003 ratio:1.25e-
    
 * Total inserted items - 6 944 003
 * Total memory usage   - 18.92 MB
 * NBomber results:

    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ insert-guids                                        │
    │      request count │ all = 3082710, ok = 3082710, RPS = 17126.2          │
    │            latency │ min = 0.53, mean = 1.16, max = 39.61, StdDev = 1.46 │
    │ latency percentile │ 50% = 0.93, 75% = 1.01, 95% = 1.33, 99% = 9.2       │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ check-existence                                     │
    │      request count │ all = 3081400, ok = 3081162, RPS = 17117.6          │
    │            latency │ min = 0.54, mean = 1.16, max = 39.75, StdDev = 1.46 │
    │ latency percentile │ 50% = 0.93, 75% = 1.01, 95% = 1.33, 99% = 9.21      │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬────────────────────────────────────────────────────┐
    │               step │ fail stats                                         │
    ├────────────────────┼────────────────────────────────────────────────────┤
    │               name │ check-existence                                    │
    │      request count │ all = 3081400, fail = 238, RPS = 1.3               │
    │            latency │ min = 0.72, mean = 1.07, max = 9.57, StdDev = 0.94 │
    │ latency percentile │ 50% = 0.93, 75% = 1, 95% = 1.22, 99% = 8.68        │
    └────────────────────┴────────────────────────────────────────────────────┘
 
 * Desired error rate - 0.01 %
 * Actual error rate  - 0.008 %

2. 
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 7 000 000
    threads   - 20

 * Insert during 3 minutes (warmup), then insert + check for existence during 3 minutes.
 * Filter stats after test:

    size:6771857
    bytes:18036216 bits:144289728 hashes:15 hashwidth:64 capacity:7000000 size:6771857 ratio:5e-05

 * Total inserted items - 6 771 857
 * Total memory usage   - 17.2 MB
 * NBomber results:

    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ insert-guids                                        │
    │      request count │ all = 2939691, ok = 2939691, RPS = 16331.6          │
    │            latency │ min = 0.54, mean = 1.22, max = 42.46, StdDev = 1.54 │
    │ latency percentile │ 50% = 0.95, 75% = 1.08, 95% = 1.53, 99% = 9.46      │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ check-existence                                     │
    │      request count │ all = 2938190, ok = 2938170, RPS = 16323.2          │
    │            latency │ min = 0.54, mean = 1.22, max = 46.93, StdDev = 1.54 │
    │ latency percentile │ 50% = 0.95, 75% = 1.08, 95% = 1.53, 99% = 9.48      │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬────────────────────────────────────────────────────┐
    │               step │ fail stats                                         │
    ├────────────────────┼────────────────────────────────────────────────────┤
    │               name │ check-existence                                    │
    │      request count │ all = 2938190, fail = 20, RPS = 0.1                │
    │            latency │ min = 0.62, mean = 1.03, max = 1.56, StdDev = 0.22 │
    │ latency percentile │ 50% = 1, 75% = 1.02, 95% = 1.43, 99% = 1.56        │
    └────────────────────┴────────────────────────────────────────────────────┘

 * Desired error rate - 0.01 %
 * Actual error rate  - 0.001 %

3. 
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 15 000 000
    threads   - 40

 * Insert during 3 minutes (warmup), then insert + check for existence during 3 minutes.
 * Filter stats after test:

    size:10059102
    bytes:38649024 bits:309192192 hashes:15 hashwidth:64 capacity:15000000 size:10059102 ratio:5e-05    

 * Total inserted items - 10 059 102
 * Total memory usage   - 36.85 MB
 * NBomber results:

    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ insert-guids                                        │
    │      request count │ all = 4151626, ok = 4151626, RPS = 23064.6          │
    │            latency │ min = 0.59, mean = 1.73, max = 63.35, StdDev = 2.53 │
    │ latency percentile │ 50% = 1.19, 75% = 1.39, 95% = 2.54, 99% = 12.83     │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬────────────────────────────────────────────────────┐
    │               step │ ok stats                                           │
    ├────────────────────┼────────────────────────────────────────────────────┤
    │               name │ check-existence                                    │
    │      request count │ all = 4152039, ok = 4152036, RPS = 23066.9         │
    │            latency │ min = 0.6, mean = 1.73, max = 75.24, StdDev = 2.53 │
    │ latency percentile │ 50% = 1.19, 75% = 1.39, 95% = 2.53, 99% = 12.81    │
    └────────────────────┴────────────────────────────────────────────────────┘
    ┌────────────────────┬────────────────────────────────────────────────────┐
    │               step │ fail stats                                         │
    ├────────────────────┼────────────────────────────────────────────────────┤
    │               name │ check-existence                                    │
    │      request count │ all = 4152039, fail = 3, RPS = 0                   │
    │            latency │ min = 1.11, mean = 1.35, max = 1.48, StdDev = 0.17 │
    │ latency percentile │ 50% = 1.47, 75% = 1.47, 95% = 1.48, 99% = 1.48     │
    └────────────────────┴────────────────────────────────────────────────────┘

 * Desired error rate - 0.01 %
 * Actual error rate  - 0.0001 %

4.
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 30 000 000
    threads   - 150

 * Insert during 3 minutes (warmup), then insert + check for existence during 3 minutes.
 * Filter stats after test:

    size:16626709
    bytes:77298048 bits:618384384 hashes:15 hashwidth:64 capacity:30000000 size:16626709 ratio:5e-05
   
 * Total inserted items - 16 626 709
 * Total memory usage   - 73.7 MB
 * NBomber results:

    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ insert-guids                                        │
    │      request count │ all = 5917453, ok = 5917453, RPS = 32874.7          │
    │            latency │ min = 0.66, mean = 4.55, max = 76.38, StdDev = 5.75 │
    │ latency percentile │ 50% = 2.27, 75% = 3.12, 95% = 16.14, 99% = 28.53    │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬────────────────────────────────────────────────────┐
    │               step │ ok stats                                           │
    ├────────────────────┼────────────────────────────────────────────────────┤
    │               name │ check-existence                                    │
    │      request count │ all = 5917906, ok = 5917906, RPS = 32877.3         │
    │            latency │ min = 0.71, mean = 4.55, max = 80.7, StdDev = 5.75 │
    │ latency percentile │ 50% = 2.27, 75% = 3.12, 95% = 16.14, 99% = 28.53   │
    └────────────────────┴────────────────────────────────────────────────────┘

 * Desired error rate - 0.01 %
 * Actual error rate  - 0 %

5.
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 30 000 000
    threads   - 300

 * Insert during 3 minutes (warmup), then insert + check for existence during 3 minutes.
 * Filter stats after test:

    size:16946629
    bytes:77298048 bits:618384384 hashes:15 hashwidth:64 capacity:30000000 size:16946629 ratio:5e-05
   
 * Total inserted items - 16 946 629
 * Total memory usage   - 73.7 MB
 * NBomber results:

    ┌────────────────────┬──────────────────────────────────────────────────────┐
    │               step │ ok stats                                             │
    ├────────────────────┼──────────────────────────────────────────────────────┤
    │               name │ insert-guids                                         │
    │      request count │ all = 5666271, ok = 5666271, RPS = 31479.3           │
    │            latency │ min = 1.08, mean = 9.51, max = 105.03, StdDev = 8.86 │
    │ latency percentile │ 50% = 4.68, 75% = 16.15, 95% = 29.74, 99% = 36.13    │
    └────────────────────┴──────────────────────────────────────────────────────┘
    ┌────────────────────┬──────────────────────────────────────────────────────┐
    │               step │ ok stats                                             │
    ├────────────────────┼──────────────────────────────────────────────────────┤
    │               name │ check-existence                                      │
    │      request count │ all = 5666300, ok = 5666299, RPS = 31479.4           │
    │            latency │ min = 1.08, mean = 9.51, max = 109.65, StdDev = 8.87 │
    │ latency percentile │ 50% = 4.68, 75% = 16.15, 95% = 29.74, 99% = 36.13    │
    └────────────────────┴──────────────────────────────────────────────────────┘
    ┌────────────────────┬─────────────────────────────────────────────────┐
    │               step │ fail stats                                      │
    ├────────────────────┼─────────────────────────────────────────────────┤
    │               name │ check-existence                                 │
    │      request count │ all = 5666300, fail = 1, RPS = 0                │
    │            latency │ min = 3.62, mean = 3.62, max = 3.62, StdDev = 0 │
    │ latency percentile │ 50% = 3.62, 75% = 3.62, 95% = 3.62, 99% = 3.62  │
    └────────────────────┴─────────────────────────────────────────────────┘

 * Desired error rate - 0.01 %
 * Actual error rate  - ~0 %

6.
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 30 000 000
    threads   - 100

 * Insert during 10 minutes, then check for existence during 5 minutes.
 * Filter stats after test:

    size:27997752
    bytes:77298048 bits:618384384 hashes:15 hashwidth:64 capacity:30000000 size:27997752 ratio:5e-05

 * Total inserted items - 27 997 752
 * Total memory usage   - 73.7 MB
 * NBomber results:

    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ insert-guids                                        │
    │      request count │ all = 27953951, ok = 27953951, RPS = 46589.9        │
    │            latency │ min = 0.55, mean = 2.14, max = 268.1, StdDev = 3.49 │
    │ latency percentile │ 50% = 1.34, 75% = 1.64, 95% = 7.32, 99% = 18.11     │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬────────────────────────────────────────────────────┐
    │               step │ ok stats                                           │
    ├────────────────────┼────────────────────────────────────────────────────┤
    │               name │ check-existence                                    │
    │      request count │ all = 14759760, ok = 14759362, RPS = 49197.9       │
    │            latency │ min = 0.58, mean = 2.02, max = 88.46, StdDev = 3.1 │
    │ latency percentile │ 50% = 1.28, 75% = 1.54, 95% = 8.08, 99% = 15.47    │
    └────────────────────┴────────────────────────────────────────────────────┘
    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ fail stats                                          │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ check-existence                                     │
    │      request count │ all = 14759760, fail = 398, RPS = 1.3               │
    │            latency │ min = 0.75, mean = 2.12, max = 37.39, StdDev = 3.55 │
    │ latency percentile │ 50% = 1.28, 75% = 1.5, 95% = 8.55, 99% = 22.62      │
    └────────────────────┴─────────────────────────────────────────────────────┘

 * Desired error rate - 0.01 %
 * Actual error rate  - 0.003 %

7.
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 60 000 000
    threads   - 200

 * Insert during 10 minutes, then check for existence during 5 minutes.
 * Filter stats after test:

    size:35383009
    bytes:154596096 bits:1236768768 hashes:15 hashwidth:64 capacity:60000000 size:35383009 ratio:5e-05
    
 * Total inserted items - 35 383 009
 * Total memory usage   - 147.43 MB
 * NBomber results:

    ┌────────────────────┬──────────────────────────────────────────────────────┐
    │               step │ ok stats                                             │
    ├────────────────────┼──────────────────────────────────────────────────────┤
    │               name │ insert-guids                                         │
    │      request count │ all = 35382797, ok = 35382797, RPS = 58971.3         │
    │            latency │ min = 0.54, mean = 3.38, max = 135.26, StdDev = 4.99 │
    │ latency percentile │ 50% = 1.9, 75% = 2.36, 95% = 13.74, 99% = 25.74      │
    └────────────────────┴──────────────────────────────────────────────────────┘
    ┌────────────────────┬──────────────────────────────────────────────────────┐
    │               step │ ok stats                                             │
    ├────────────────────┼──────────────────────────────────────────────────────┤
    │               name │ check-existence                                      │
    │      request count │ all = 17251808, ok = 17251803, RPS = 57506           │
    │            latency │ min = 0.57, mean = 3.46, max = 100.33, StdDev = 4.93 │
    │ latency percentile │ 50% = 1.87, 75% = 2.39, 95% = 14.57, 99% = 27.78     │
    └────────────────────┴──────────────────────────────────────────────────────┘
    ┌────────────────────┬────────────────────────────────────────────────┐
    │               step │ fail stats                                     │
    ├────────────────────┼────────────────────────────────────────────────┤
    │               name │ check-existence                                │
    │      request count │ all = 17251808, fail = 5, RPS = 0              │
    │            latency │ min = 1.7, mean = 2, max = 2.27, StdDev = 0.2  │
    │ latency percentile │ 50% = 2.05, 75% = 2.15, 95% = 2.27, 99% = 2.27 │
    └────────────────────┴────────────────────────────────────────────────┘

 * Desired error rate - 0.01 %
 * Actual error rate  - ~0 %

8.
 * Filter configuration:

    errorRate - 0.0001
    capacity  - 60 000 000
    threads   - 300

 * Insert during 15 minutes (200 threads), then insert (5 threads) and check for existence (300 threads) during 20 minutes.
 * Filter stats after test:

    size:55578070
    bytes:154596096 bits:1236768768 hashes:15 hashwidth:64 capacity:60000000 size:55578070 ratio:5e-05
    
 * Total inserted items - 55 578 070
 * Total memory usage   - 147.43 MB
 * NBomber results:

    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ ok stats                                            │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ insert-guids                                        │
    │      request count │ all = 54330237, ok = 54330237, RPS = 60366.9        │
    │            latency │ min = 0.57, mean = 3.3, max = 178.41, StdDev = 5.12 │
    │ latency percentile │ 50% = 1.85, 75% = 2.28, 95% = 13.3, 99% = 24.91     │
    └────────────────────┴─────────────────────────────────────────────────────┘
    ┌────────────────────┬──────────────────────────────────────────────────────┐
    │               step │ ok stats                                             │
    ├────────────────────┼──────────────────────────────────────────────────────┤
    │               name │ insert-guids                                         │
    │      request count │ all = 1179523, ok = 1179523, RPS = 982.9             │
    │            latency │ min = 0.78, mean = 5.07, max = 343.89, StdDev = 7.89 │
    │ latency percentile │ 50% = 2.37, 75% = 3.27, 95% = 18.5, 99% = 32.72      │
    └────────────────────┴──────────────────────────────────────────────────────┘
    ┌────────────────────┬───────────────────────────────────────────────────┐
    │               step │ ok stats                                          │
    ├────────────────────┼───────────────────────────────────────────────────┤
    │               name │ check-existence                                   │
    │      request count │ all = 70830641, ok = 70829203, RPS = 59024.3      │
    │            latency │ min = 0.58, mean = 5.07, max = 350, StdDev = 7.89 │
    │ latency percentile │ 50% = 2.37, 75% = 3.26, 95% = 18.5, 99% = 32.72   │
    └────────────────────┴───────────────────────────────────────────────────┘
    ┌────────────────────┬─────────────────────────────────────────────────────┐
    │               step │ fail stats                                          │
    ├────────────────────┼─────────────────────────────────────────────────────┤
    │               name │ check-existence                                     │
    │      request count │ all = 70830641, fail = 1438, RPS = 1.2              │
    │            latency │ min = 1.4, mean = 5.27, max = 155.48, StdDev = 9.02 │
    │ latency percentile │ 50% = 2.36, 75% = 3.19, 95% = 18.75, 99% = 33.7     │
    └────────────────────┴─────────────────────────────────────────────────────┘

 * Desired error rate - 0.01 %
 * Actual error rate  - 0.002 %
```

## Getting Started

You can perform the load testing by yourself using tests from this repository. About how to run them locally you will learn from the sections below.

### Prerequisites

What things you need to install

```
1. .Net 5.0 SDK - use the official Microsoft website.
2. Docker Desktop (any version) - use the official Docker website.
```

### Build and run

Firstly, clone the repository to any folder you wish:

```
git clone https://github.com/Expeth/redis-bloom-performance.git
```

Then, adjust appsettings.json file with the needed configuration of the BloomFilter:

``` java
"bloom": {
    "name": "test",        // The key under which the filter is found
    "errorRate": "0.0001", // The desired probability for false positives. The rate is a decimal value between 0 and 1.
    "capacity": "10000000" // The number of entries intended to be added to the filter. 
}
```

Then, simply build and run the needed tests from Rider or VisualStudio. The results you can check in the test output or in the "reports" folder, generated by NBomber.

## Built With

* [.Net 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) - Platform
* [NBomber](https://github.com/PragmaticFlow/NBomber) - Load testing
* [FluentDocker](https://github.com/mariotoffia/FluentDocker) - Used to run Docker container with Redis Bloom
* [NUnit](https://github.com/nunit/nunit) - Unit-testing framework

## Authors

* **Kovetskiy Valeriy** - *Initial work* - [telegram](https://t.me/kovetskiy)
