# Order genartion 
## Unlock levels for the stages
|             | L1  | L2  | L3  | L4  | L5  | L6  | L7  | L8  | L9  | L10 |
| ----------- | --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| Case        | S1  | S2  |     | S3  |     | S4  |     |     |     |     |
| HDD         | S1  |     | S2  |     | S3  |     | S4  |     |     |     |
| Netzteil    | S1  |     |     | S2  |     | S3  |     | S4  |     |     |
| Motherboard | S1  |     |     |     | S2  |     | S3  |     | S4  |     |
| GPU         | S1  |     |     |     |     | S2  |     | S3  |     | S4  |
| CPU         | S1  |     |     |     |     | S2  |     | S3  |     | S4  |
| RAM         | S1  |     |     |     |     |     | S2  |     | S3  | S4  |



## Initial values for the calculaiton of the stage-probability
Please have a look at the [dataLog.txt](/Assets/Scripts/Order/dataLog.txt) to find the current set values. 

|FIELD1|FIELD2                                                                                    ||Component                                                                  |Stage            |unlockLevel         |currentLevel|distance |scaledDistance    |currentLevel|distance |scaledDistance  |currentLevel|distance |scaledDistance  |currentLevel|distance |scaledDistance|currentLevel|distance |scaledDistance  |currentLevel|distance |scaledDistance|currentLevel|distance |scaledDistance  |currentLevel|distance |scaledDistance  |currentLevel|distance |scaledDistance |currentLevel|distance |scaledDistance  |
|------|------------------------------------------------------------------------------------------|------|------------------------------------------------------------------------|------------------|---------------|------|-------|----------|-------|-------|----------|-------|-------|----------|-------|-------|----------|-------|-------|----------|-------|-------|----------|-------|-------|----------|-------|-------|----------|-------|-------|----------|-------|-------|----------|
|                                                                                          |      |                                                                    | Case            || |       |          |  |       |          |  |       |          |  |       |          |  |       |          |  |       |          |  |       |          |  |       |          |  |       |          |  |       |          |
|      |                                                                                          |      |                                                                        |                  |               |1     |       |          |2      |       |          |3      |       |          |4      |       |          |5      |       |          |6      |       |          |7      |       |          |8      |       |          |9      |       |          |10     |       |          |
|      |                                                                                          |      |                                                                        |                  |               |P     |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||
|      |                                                                                          |      |                                                                        |1                 |1              |100%  |       |          |100%   |1      |1         |50%    |2      |2         |43%    |3      |3         |31%    |4      |4         |26%    |5      |5         |21%    |6      |6         |18%    |7      |7         |16%    |8      |8         |15%    |9      |9         |
|      |                                                                                          |      |                                                                        |2                 |2              |      |       |          |0%     |       |0         |50%    |1      |2         |57%    |2      |4         |46%    |3      |6         |42%    |4      |8         |34%    |5      |10        |31%    |6      |12        |29%    |7      |14        |27%    |8      |16        |
|      |                                                                                          |      |                                                                        |3                 |4              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |23%    |1      |3         |32%    |2      |6         |31%    |3      |9         |31%    |4      |12        |31%    |5      |15        |31%    |6      |18        |
|      |                                                                                          |      |                                                                        |4                 |6              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |14%    |1      |4         |21%    |2      |8         |24%    |3      |12        |27%    |4      |16        |
|      |                                                                                          |      |                                                                        |TotalScaledDistances|               |      |       |          |       |       |1         |       |       |4         |       |       |7         |       |       |13        |       |       |19        |       |       |29        |       |       |39        |       |       |49        |       |       |59        |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |HDD                                                                     |                  |               | |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |             ||1     |       |          |2      |       |          |3      |       |          |4      |       |          |5      |       |          |6      |       |          |7      |       |          |8      |       |          |9      |       |          |10     |       |          |
|      |                                                                                          |      |                                                                        |                  |               |P     |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||
|      |                                                                                          |      |                                                                        |1                 |1              |100%  |       |          |100%   |1      |1         |100%   |2      |2         |60%    |3      |3         |50%    |4      |4         |36%    |5      |5         |30%    |6      |6         |23%    |7      |7         |20%    |8      |8         |18%    |9      |9         |
|      |                                                                                          |      |                                                                        |2                 |3              |      |       |          |0%     |       |0         |0%     |0      |0         |40%    |1      |2         |50%    |2      |4         |43%    |3      |6         |40%    |4      |8         |33%    |5      |10        |30%    |6      |12        |28%    |7      |14        |
|      |                                                                                          |      |                                                                        |3                 |5              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |21%    |1      |3         |30%    |2      |6         |30%    |3      |9         |30%    |4      |12        |30%    |5      |15        |
|      |                                                                                          |      |                                                                        |4                 |7              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |13%    |1      |4         |20%    |2      |8         |24%    |3      |12        |
|      |                                                                                          |      |                                                                        |TotalScaledDistances|               |      |       |          |       |       |1         |       |       |2         |       |       |5         |       |       |8         |       |       |14        |       |       |20        |       |       |30        |       |       |40        |       |       |50        |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |Netzteil                                                                |             || |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |1     |       |          |2      |       |          |3      |       |          |4      |       |          |5      |       |          |6      |       |          |7      |       |          |8      |       |          |9      |       |          |10     |       |          |
|      |                                                                                          |      |                                                                        |                  |               |P     |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||
|      |                                                                                          |      |                                                                        |1                 |1              |100%  |       |          |100%   |1      |1         |100%   |2      |2         |100%   |3      |3         |67%    |4      |4         |56%    |5      |5         |40%    |6      |6         |33%    |7      |7         |26%    |8      |8         |22%    |9      |9         |
|      |                                                                                          |      |                                                                        |2                 |4              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |33%    |1      |2         |44%    |2      |4         |40%    |3      |6         |38%    |4      |8         |32%    |5      |10        |29%    |6      |12        |
|      |                                                                                          |      |                                                                        |3                 |6              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |20%    |1      |3         |29%    |2      |6         |29%    |3      |9         |29%    |4      |12        |
|      |                                                                                          |      |                                                                        |4                 |8              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |13%    |1      |4         |20%    |2      |8         |
|      |                                                                                          |      |                                                                        |TotalScaledDistances|               |      |       |          |       |       |1         |       |       |2         |       |       |3         |       |       |6         |       |       |9         |       |       |15        |       |       |21        |       |       |31        |       |       |41        |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |Motherboard                                                             |             || |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |1     |       |          |2      |       |          |3      |       |          |4      |       |          |5      |       |          |6      |       |          |7      |       |          |8      |       |          |9      |       |          |10     |       |          |
|      |                                                                                          |      |                                                                        |                  |               |P     |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||
|      |                                                                                          |      |                                                                        |1                 |1              |100%  |       |          |100%   |1      |1         |100%   |2      |2         |100%   |3      |3         |100%   |4      |4         |71%    |5      |5         |60%    |6      |6         |44%    |7      |7         |36%    |8      |8         |28%    |9      |9         |
|      |                                                                                          |      |                                                                        |2                 |5              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |29%    |1      |2         |40%    |2      |4         |38%    |3      |6         |36%    |4      |8         |31%    |5      |10        |
|      |                                                                                          |      |                                                                        |3                 |7              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |19%    |1      |3         |27%    |2      |6         |28%    |3      |9         |
|      |                                                                                          |      |                                                                        |4                 |9              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |13%    |1      |4         |
|      |                                                                                          |      |                                                                        |TotalScaledDistances|               |      |       |          |       |       |1         |       |       |2         |       |       |3         |       |       |4         |       |       |7         |       |       |10        |       |       |16        |       |       |22        |       |       |32        |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |GPU                                                                     |             || |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |1     |       |          |2      |       |          |3      |       |          |4      |       |          |5      |       |          |6      |       |          |7      |       |          |8      |       |          |9      |       |          |10     |       |          |
|      |                                                                                          |      |                                                                        |                  |               |P     |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||
|      |                                                                                          |      |                                                                        |1                 |1              |100%  |       |          |100%   |1      |1         |100%   |2      |2         |100%   |3      |3         |100%   |4      |4         |100%   |5      |5         |75%    |6      |6         |64%    |7      |7         |47%    |8      |8         |39%    |9      |9         |
|      |                                                                                          |      |                                                                        |2                 |6              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |25%    |1      |2         |36%    |2      |4         |35%    |3      |6         |35%    |4      |8         |
|      |                                                                                          |      |                                                                        |3                 |8              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |18%    |1      |3         |26%    |2      |6         |
|      |                                                                                          |      |                                                                        |4                 |10             |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |
|      |                                                                                          |      |                                                                        |TotalScaledDistances|               |      |       |          |       |       |1         |       |       |2         |       |       |3         |       |       |4         |       |       |5         |       |       |8         |       |       |11        |       |       |17        |       |       |23        |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |CPU                                                                     |             || |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |1     |       |          |2      |       |          |3      |       |          |4      |       |          |5      |       |          |6      |       |          |7      |       |          |8      |       |          |9      |       |          |10     |       |          |
|      |                                                                                          |      |                                                                        |                  |               |P     |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||
|      |                                                                                          |      |                                                                        |1                 |1              |100%  |       |          |100%   |1      |1         |100%   |2      |2         |100%   |3      |3         |100%   |4      |4         |100%   |5      |5         |75%    |6      |6         |64%    |7      |7         |47%    |8      |8         |39%    |9      |9         |
|      |                                                                                          |      |                                                                        |2                 |6              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |25%    |1      |2         |36%    |2      |4         |35%    |3      |6         |35%    |4      |8         |
|      |                                                                                          |      |                                                                        |3                 |8              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |18%    |1      |3         |26%    |2      |6         |
|      |                                                                                          |      |                                                                        |4                 |10             |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |
|      |                                                                                          |      |                                                                        |TotalScaledDistances|               |      |       |          |       |       |1         |       |       |2         |       |       |3         |       |       |4         |       |       |5         |       |       |8         |       |       |11        |       |       |17        |       |       |23        |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |      |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |RAM                                                                     |             || |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |       |       |          |
|      |                                                                                          |      |                                                                        |                  |               |1     |       |          |2      |       |          |3      |       |          |4      |       |          |5      |       |          |6      |       |          |7      |       |          |8      |       |          |9      |       |          |10     |       |          |
|      |                                                                                          |      |                                                                        |                  |               |P     |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||P      |||
|      |                                                                                          |      |                                                                        |1                 |1              |100%  |       |          |100%   |1      |1         |100%   |2      |2         |100%   |3      |3         |100%   |4      |4         |100%   |5      |5         |75%    |6      |6         |64%    |7      |7         |47%    |8      |8         |39%    |9      |9         |
|      |                                                                                          |      |                                                                        |2                 |7              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |25%    |0      |2         |36%    |2      |4         |35%    |3      |6         |35%    |4      |8         |
|      |                                                                                          |      |                                                                        |3                 |9              |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |18%    |1      |3         |26%    |2      |6         |
|      |                                                                                          |      |                                                                        |4                 |10             |      |       |          |0%     |       |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |0%     |0      |0         |
|      |                                                                                          |      |                                                                        |TotalScaledDistances|               |      |       |          |       |       |1         |       |       |2         |       |       |3         |       |       |4         |       |       |5         |       |       |8         |       |       |11        |       |       |17        |       |       |23        |